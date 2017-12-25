using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SNSBot.Cookies;
using SNSBot.Error;
using SNSBot.Instagram.JSON;

namespace SNSBot.Instagram
{
	public class InstagramPage
	{
		private InstagramListener _listener;
		private UInt32 _interval = 300000;
		private readonly String _username;
		private readonly UInt64 _userId;
		private readonly int _cookieIndex;

		//List has Limitations to Int.MaxValue
		//Use Int. I think no one will upload it than Int.MaxValue.
		private Int32 _count;

		private UInt64 _lastPostId;

		public InstagramPage(int cookieIndex, String username, UInt64 userId)
		{
			Timer timer = new Timer(TimerCallback);
			timer.Change(0, _interval);
			_username = username;
			_userId = userId;
			_cookieIndex = cookieIndex;
			_count = GetCount().Result;
			_lastPostId = GetLastPost().Result.Id;
		}
		
		public async Task<Boolean> Reload()
		{
			Int32 count = await GetCount();
			if (_count != count)
			{
				int newCount = count - _count;

				HttpClientHandler handler = new HttpClientHandler();
				handler.CookieContainer = CookieHandler.GetCookieContainer(_cookieIndex);

				HttpClient client = new HttpClient(handler);
				client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
				client.DefaultRequestHeaders.Referrer = new Uri("https://www.instagram.com/" + _username + "/");

				HttpResponseMessage response = await client.GetAsync(new Uri(
					"https://www.instagram.com/graphql/query/?query_id=17888483320059182&variables=" +
					Uri.EscapeDataString("{\"id\":\"" + _userId + "\",\"first\":" + (newCount + 1) + "}")));

				if (!response.IsSuccessStatusCode)
					throw new HTTPError(response.StatusCode.ToString());

				Query query = JsonConvert.DeserializeObject<Query>(response.Content.ReadAsStringAsync().Result);

				if (query.Status != "ok")
					throw new RequestError();

				if(query.Data.User.EdgeOwnerToTimelineMedia.Edges[newCount].Node.Id != _lastPostId)
					throw new RequestError("Post Update was too fast to manage.");

				for(int i = newCount; i > 0; ++i)
					_listener.onNewArticle(new InstagramPost(query.Data.User.EdgeOwnerToTimelineMedia.Edges[i - 1].Node));

				_lastPostId = query.Data.User.EdgeOwnerToTimelineMedia.Edges[0].Node.Id;

				_count = count;

				return true;
			}
			return false;
		}

		public void SetInstagramListener(InstagramListener listener)
		{
			_listener = listener;
		}

		public void SetInterval(UInt32 interval)
		{
			_interval = interval;
		}

		private async Task<Int32> GetCount()
		{
			HttpClientHandler handler = new HttpClientHandler();
			handler.CookieContainer = CookieHandler.GetCookieContainer(_cookieIndex);

			HttpClient client = new HttpClient(handler);
			client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
			client.DefaultRequestHeaders.Referrer = new Uri("https://www.instagram.com/" + _username + "/");

			HttpResponseMessage response = await client.GetAsync(new Uri("https://www.instagram.com/graphql/query/?query_id=17888483320059182&variables=" + Uri.EscapeDataString("{\"id\":\"" + _userId + "\",\"first\":0}")));

			if(!response.IsSuccessStatusCode)
				throw new HTTPError(response.StatusCode.ToString());

			Query query = JsonConvert.DeserializeObject<Query>(response.Content.ReadAsStringAsync().Result);

			if(query.Status != "ok")
				throw new RequestError();

			return query.Data.User.EdgeOwnerToTimelineMedia.Count;
		}

		private async Task<InstagramPost> GetLastPost()
		{
			HttpClientHandler handler = new HttpClientHandler();
			handler.CookieContainer = CookieHandler.GetCookieContainer(_cookieIndex);

			HttpClient client = new HttpClient(handler);
			client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
			client.DefaultRequestHeaders.Referrer = new Uri("https://www.instagram.com/" + _username + "/");

			HttpResponseMessage response = await client.GetAsync(new Uri("https://www.instagram.com/graphql/query/?query_id=17888483320059182&variables=" + Uri.EscapeDataString("{\"id\":\"" + _userId + "\",\"first\":1}")));

			if (!response.IsSuccessStatusCode)
				throw new HTTPError(response.StatusCode.ToString());

			Query query = JsonConvert.DeserializeObject<Query>(response.Content.ReadAsStringAsync().Result);

			if (query.Status != "ok")
				throw new RequestError();

			if (query.Data.User.EdgeOwnerToTimelineMedia.Count == 0)
				return null;

			return new InstagramPost(query.Data.User.EdgeOwnerToTimelineMedia.Edges[0].Node);
		}

		private void TimerCallback(Object state)
		{
			Reload();
		}

	}
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SNSBot.Error;
using SNSBot.Cookies;
using SNSBot.Instagram.JSON;

namespace SNSBot.Instagram
{
	public class Instagram
	{

		private readonly int _cookieIndex;
	
		/*
		 * Instagram()
		 * Initalize Cookies. Connect to Instagram for get essential cookies.
		 */
		public Instagram()
		{
			_cookieIndex = CookieHandler.AddCookieContainer();

			HttpClientHandler handler = new HttpClientHandler {CookieContainer = CookieHandler.GetCookieContainer(_cookieIndex) };

			HttpClient client  = new HttpClient(handler);

			HttpResponseMessage response = client.GetAsync("https://www.instagram.com/").Result;

			if (!response.IsSuccessStatusCode)
				throw new HTTPError(response.StatusCode.ToString());

		}

		~Instagram()
		{
			CookieHandler.DeleteCookieContainer(_cookieIndex);
		}

		/*
		 * Boolean Login()
		 * Login to Instagram. Load Page and Posts which are locked.
		 */
		public Boolean Login(String id, String pass)
		{
			HttpClientHandler handler = new HttpClientHandler();
			handler.CookieContainer = CookieHandler.GetCookieContainer(_cookieIndex);

			HttpClient client = new HttpClient(handler);
			client.DefaultRequestHeaders.Add("X-Instagram-AJAX","1");
			client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
			client.DefaultRequestHeaders.Add("X-CSRFToken", CookieHandler.GetCookieContainer(_cookieIndex).GetCookies(new Uri("https://www.instagram.com/"))["csrftoken"]?.Value);
			client.DefaultRequestHeaders.Referrer = new Uri("https://www.instagram.com/");

			HttpResponseMessage response = client.PostAsync("https://www.instagram.com/accounts/login/ajax/", new FormUrlEncodedContent(
				new List<KeyValuePair<string, string>>
				{
					new KeyValuePair<string, string>("username", id),
					new KeyValuePair<string, string>("password", pass)
				})).Result;

			if (!response.IsSuccessStatusCode)
				throw new HTTPError(response.StatusCode.ToString());

			JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
			if ((String) json["status"] != "ok")
				throw new RequestError();
			return (Boolean) json["authenticated"];
		}

		/*
		 * InstagramPage GetPage(String username)
		 * Returns InstagramPage Instance
		 */
		public InstagramPage GetPage(String username)
		{
			return new InstagramPage(_cookieIndex, username, GetUserId(username));
		}

		private UInt64 GetUserId(String username)
		{
			HttpClientHandler handler = new HttpClientHandler();
			handler.CookieContainer = CookieHandler.GetCookieContainer(_cookieIndex);

			HttpClient client = new HttpClient(handler);
			client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
			client.DefaultRequestHeaders.Referrer = new Uri("https://www.instagram.com/" + username + "/");

			HttpResponseMessage response = client.GetAsync(new Uri("https://www.instagram.com/" + username + "/?__a=1")).Result;

			if (!response.IsSuccessStatusCode)
				throw new HTTPError(response.StatusCode.ToString());

			UserData userData = JsonConvert.DeserializeObject<UserData>(response.Content.ReadAsStringAsync().Result);

			return userData.User.Id;
		}
		
	}
}

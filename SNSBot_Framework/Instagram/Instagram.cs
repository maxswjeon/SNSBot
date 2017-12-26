using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SNSBot.Error;
using SNSBot.Instagram.JSON;

namespace SNSBot.Instagram
{
	public class Instagram
	{

		private readonly HttpClient _client;
		private readonly CookieContainer _cookie;
	
		/*
		 * Instagram()
		 * Initalize Cookies. Connect to Instagram for get essential cookies.
		 */
		public Instagram()
		{
			_client  = new HttpClient(new HttpClientHandler{CookieContainer = _cookie});

			HttpResponseMessage response = _client.GetAsync("https://www.instagram.com/").Result;

			if (!response.IsSuccessStatusCode)
				throw new HTTPError(response.StatusCode.ToString());

		}

		/*
		 * Boolean Login()
		 * Login to Instagram. Load Page and Posts which are locked.
		 */
		public Boolean Login(String id, String pass)
		{
			HttpRequestMessage request = new HttpRequestMessage
			{
				RequestUri = new Uri("https://www.instagram.com/accounts/login/ajax/"),
				Method = HttpMethod.Post,
				Headers =
				{
					{"X-Instagram-AJAX", "1"},
					{"X-Requested_With", "XMLHttpRequest"},
					{"X-CSRFToken", _cookie.GetCookies(new Uri("https://www.instagram.com/"))["csrfToken"]?.Value},
					{HttpRequestHeader.Referer.ToString(), "https://www.instagram.com/"}
				},
				Content = new FormUrlEncodedContent(new []
				{
					new KeyValuePair<string, string>("username", id),
					new KeyValuePair<string, string>("password", pass)
				})
			};

			HttpResponseMessage response = _client.SendAsync(request).Result;

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
			return new InstagramPage(_client, _cookie, username, GetUserId(username));
		}

		private UInt64 GetUserId(String username)
		{
			HttpRequestMessage request = new HttpRequestMessage
			{
				RequestUri = new Uri("https://www.instagram.com/" + username + "/?__a=1"),
				Method = HttpMethod.Get,
				Headers =
				{
					{"X-Requested_With", "XMLHttpRequest"},
					{HttpRequestHeader.Referer.ToString(), "https://www.instagram.com/" + username + "/"}
				}
			};

			HttpResponseMessage response = _client.SendAsync(request).Result;

			if (!response.IsSuccessStatusCode)
				throw new HTTPError(response.StatusCode.ToString());

			UserData userData = JsonConvert.DeserializeObject<UserData>(response.Content.ReadAsStringAsync().Result);

			return userData.User.Id;
		}
		
	}
}

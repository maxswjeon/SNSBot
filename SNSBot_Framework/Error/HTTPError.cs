using System;
using System.Net;

namespace SNSBot.Error
{
	public class HTTPError : Exception
	{
		public HTTPError() : base() { }
		public HTTPError(String message) : base(message) { }
		public HTTPError(String message, Exception inner) : base(message, inner) { }
	}
}

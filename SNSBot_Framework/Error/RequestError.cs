using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SNSBot.Error
{
	class RequestError : Exception
	{
		public RequestError() { }

		public RequestError(string message) : base(message) { }

		public RequestError(string message, Exception innerException) : base(message, innerException) { }

		protected RequestError(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}

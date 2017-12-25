using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SNSBot.Cookies
{
	internal static class CookieHandler
	{
		private static object _instagramSync = new object();
		private static List<CookieContainer> Instagram = new List<CookieContainer>();

		internal static int AddCookieContainer()
		{
			lock (_instagramSync)
			{
				Instagram.Add(new CookieContainer());
				return Instagram.Count;
			}
		}

		internal static CookieContainer GetCookieContainer(int index)
		{
			return Instagram[index - 1];
		}

		internal static void PutCookieContainer(int index, CookieContainer container)
		{
			lock (_instagramSync)
			{
				Instagram[index] = container;
			}
		}

		internal static void DeleteCookieContainer(int index)
		{
			Instagram[index] = null;
		}
	}
}

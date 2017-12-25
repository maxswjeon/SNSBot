using System;
using System.Collections.Generic;
using System.Text;

namespace SNSBot.Instagram
{
	public interface InstagramListener
	{
		void onNewArticle(InstagramPost post);
	}
}

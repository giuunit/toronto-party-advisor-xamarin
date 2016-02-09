using System;
using System.Threading.Tasks;

namespace TorontoPartyAdvisor
{
	public static class FacebookHelper
	{
		public async static Task<FacebookUser> GetAsync(string token)
		{
			var url = string.Format("https://graph.facebook.com/v2.3/me?access_token={0}", token);

			FacebookUser toReturn = await HttpHelper.Get<FacebookUser> (url);

			return toReturn;
		}

		public static string GetPicture(string facebookId)
		{
			return string.Format ("http://graph.facebook.com/{0}/picture?type=square", facebookId);
		}

		public static string GetPicture(string facebookId, int size)
		{
			return string.Format ("http://graph.facebook.com/{0}/picture?height={1}&width={1}", facebookId, size);
		}

		public static string GetPictureLarge(string facebookId)
		{
			return string.Format ("http://graph.facebook.com/{0}/picture?type=large", facebookId);
		}
	}
}



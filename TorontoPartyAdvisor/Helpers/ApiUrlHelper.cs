using System.Collections.Generic;
using System.Linq;

namespace TorontoPartyAdvisor
{
	public static class ApiUrlHelper
	{
		private static string GetUrl(string obj, string action, string queryString = null)
		{
			var finalUrl = string.Format ("{0}/{1}/{2}{3}", Const.PROJECT_URL, obj, action, queryString);
			return finalUrl;
		}

		public static string GetLoginUrl()
		{
			return GetUrl ("users", "login");
		}

		public static string GetPlacesGetUrl(string accessToken)
		{
			return GetUrl ("places", "get", 
				GetQueryString(new Dictionary<string, string> () {{"accesstoken", accessToken}}));
		}

		public static string GetGenderStatsUrl(string accessToken, int placeId)
		{
			return GetUrl ("places", "genderstats", 
				GetQueryString(new Dictionary<string, string> () {
					{"accesstoken", accessToken},
					{"placeId", placeId.ToString()}
				}));
		}

		public static string GetUserInfoUrl(string accessToken)
		{
			return GetUrl ("users", "getinfo", 
				GetQueryString(new Dictionary<string, string> () {
					{"accesstoken", accessToken}
				}));
		}

		public static string GetRankingUrl(string accessToken)
		{
			return GetUrl("ranking","get", 
				GetQueryString(new Dictionary<string, string> {
				{"accesstoken", accessToken}
			}));
		}

		public static string GetAddPositionUrl()
		{
			return GetUrl ("positions", "add");
		}

		public static string GetPlacesPingUrl()
		{
			return GetUrl ("places", "ping");
		}

		public static string GetPlaceFbEvent(string accessToken, string facebookPlaceId)
		{
			return GetUrl ("places", "getevent", GetQueryString(new Dictionary<string, string> {
				{"accesstoken", accessToken},
				{"facebookId", facebookPlaceId}
			}));
		}

		public static string GetFbHours(string accessToken, string facebookPlaceId)
		{
			return GetUrl ("places", "gethours", GetQueryString(new Dictionary<string, string> {
				{"accesstoken", accessToken},
				{"facebookId", facebookPlaceId}
			}));
		}

		private static string GetQueryString(Dictionary<string, string> values)
		{
			string toReturn = string.Empty;

			if (values.Count == 0) 
			{
				return toReturn;
			}

			toReturn = "?";

			for (int i = 0; i < values.Count; i++) 
			{
				var element = values.ElementAt(i);
				var key = System.Net.WebUtility.UrlEncode (element.Key);
				var value = System.Net.WebUtility.UrlEncode (element.Value);
				var stringArgument = string.Format ("{0}={1}", key, value);

				//not the last
				if(i != values.Count-1)
				{
					stringArgument += "&";
				}

				toReturn += stringArgument;
			}

			return toReturn;
		}
	}
}


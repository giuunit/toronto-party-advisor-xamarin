using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TorontoPartyAdvisor
{
	public static class PartyAdvisorApiHelper
	{
		public static async Task Login(FacebookUser user)
		{
			var url = ApiUrlHelper.GetLoginUrl();
			await HttpHelper.Post (url, user);
		}

		public static async Task<List<Place>> GetPlaces(string token)
		{
			var url = ApiUrlHelper.GetPlacesGetUrl (token);
			var places =  await HttpHelper.Get<List<Place>> (url);
			return places;
		}

		public static async Task<PlaceDetails> GetGenderStats(string token, int placeId)
		{
			var url = ApiUrlHelper.GetGenderStatsUrl (token, placeId);
			var placeDetails = await HttpHelper.Get<PlaceDetails> (url);
			return placeDetails;
		}

		public static async Task<UserInfo> GetUserInfo(string token)
		{
			var url = ApiUrlHelper.GetUserInfoUrl (token);
			var userInfo = await HttpHelper.Get<UserInfo> (url);
			return userInfo;
		}

		public static async Task<List<RankingItem>> GetRanking(string token)
		{
			var url = ApiUrlHelper.GetRankingUrl (token);
			var rankingItems = await HttpHelper.Get<List<RankingItem>> (url);
			return rankingItems;	
		}

		public static async Task<BonusBox> PostPosition (PostPositionViewModel obj, TimeSpan? timeOut = null)
		{
			var url = ApiUrlHelper.GetAddPositionUrl ();
			var bonus = await HttpHelper.Post<BonusBox> (url, obj, timeOut);
			return bonus;
		}

		public static async Task<DateTimeOffset> GetPlacePing ()
		{
			var url = ApiUrlHelper.GetPlacesPingUrl ();
			var date = await HttpHelper.Get<DateTimeOffset> (url);
			return date;
		}

		public static async Task<FacebookEvent> GetNextFacebookEvent(string token, string facebookPlaceId)
		{
			var url = ApiUrlHelper.GetPlaceFbEvent (token, facebookPlaceId);
			var fbEvent = await HttpHelper.Get<FacebookEvent> (url);
			return fbEvent;
		}

		public static async Task<FacebookPlaceDayHours> GetHours(string accessToken, string facebookPlaceId)
		{
			var url = ApiUrlHelper.GetFbHours (accessToken, facebookPlaceId);
			var hours = await HttpHelper.Get<FacebookPlaceDayHours> (url);
			return hours;
		}
	}
}


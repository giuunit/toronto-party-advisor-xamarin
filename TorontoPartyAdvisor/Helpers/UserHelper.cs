using System.Threading.Tasks;

namespace TorontoPartyAdvisor
{
	public static class UserHelper
	{
		public static async Task SaveUserAsync(FacebookUser user)
		{
			await new AkavacheEntity ().InsertObjectAsync<FacebookUser> (Const.USER_AKAVACHE_KEY, user);
		}
	}
}


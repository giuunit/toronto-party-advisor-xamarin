using System.Threading.Tasks;

namespace TorontoPartyAdvisor
{
	public sealed class CacheHelper
	{
		private static volatile User _user;
		private static object syncRoot = new object();
		internal const string USR_KEY = "user";

		private CacheHelper () { }

		public async static Task<User> GetUserAsync() 
		{
			if (_user == null) 
			{
				_user = await new AkavacheEntity().GetObjectAsync<User> (USR_KEY);
			}

			return _user;
		}

		public async static Task SaveUserAsync(User user)
		{
			await new AkavacheEntity().InsertObjectAsync(USR_KEY, user);

			_user = user;
		}

		public async static Task ClearUser()
		{
			_user = null;
			await new AkavacheEntity().RemoveObjectAsync<User>(USR_KEY);
		}
	}
}

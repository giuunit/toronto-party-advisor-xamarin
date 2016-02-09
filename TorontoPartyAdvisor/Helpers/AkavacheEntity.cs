using System.Threading.Tasks;
//dont delete this using
using System.Reactive.Linq;
using Akavache;
using System;

namespace TorontoPartyAdvisor
{
	/// <summary>
	/// Akavache entity.
	/// </summary>
	public class AkavacheEntity : IEntity
	{
		public AkavacheEntity()
		{
			BlobCache.ApplicationName = "PartyAdvisor";
		}

		#region IEntity implementation

		public async Task<T> GetObjectAsync<T> (string key)
		{
			try
			{
				var res = await BlobCache.UserAccount.GetObject<T> (key);
				return res;
			}
			catch(Exception e)
			{
				return default(T);
			}
		}

		public async Task InsertObjectAsync<T> (string key, T value)
		{
			await BlobCache.UserAccount.InsertObject<T>(key, value);
		}

		public async Task RemoveObjectAsync<T> (string key)
		{
			await BlobCache.UserAccount.InvalidateObject<T> (key);
		}

		public async Task DeleteAll()
		{
			await BlobCache.UserAccount.InvalidateAll();
		}
			
		#endregion
	}
}


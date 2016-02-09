using System;
using System.Threading.Tasks;

namespace TorontoPartyAdvisor
{
	public interface IEntity
	{
		Task<T> GetObjectAsync<T>(string key);
		Task InsertObjectAsync<T>(string key, T value);
		Task RemoveObjectAsync<T>(string key);

	}
}


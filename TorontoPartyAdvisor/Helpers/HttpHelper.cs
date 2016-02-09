using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TorontoPartyAdvisor
{
	public static class HttpHelper
	{
		public async static Task<bool> IsNetworkAvailable()
		{
			try 
			{
				using (var httpClient = new HttpClient())
				{
					var content = await httpClient.GetStringAsync("https://www.google.com");
					return true;
				}
			}

			catch(Exception) 
			{
				return false;
			}
		}

		public async static Task<T> Get<T>(string url)
		{
			using (var httpClient = new HttpClient())
			{
				var content = await httpClient.GetStringAsync(url); 

				//todo: make it async
				return  Newtonsoft.Json.JsonConvert.DeserializeObject<T> (content);
			}
		}
			
		public async static Task Post(string url, object data)
		{
			//todo: make it async
			string json =  Newtonsoft.Json.JsonConvert.SerializeObject(data);
			StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

			using (var httpClient = new HttpClient())
			{
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
				var response = await httpClient.PostAsync(url, content);
				response.EnsureSuccessStatusCode();
			}
		}

		public async static Task<T> Post<T>(string url, object data, TimeSpan? timeOut = null) where T : class
		{
			//todo: make it async
			string json =  Newtonsoft.Json.JsonConvert.SerializeObject(data);
			StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

			using (var httpClient = new HttpClient())
			{
				if (timeOut.HasValue)
					httpClient.Timeout = timeOut.Value;

				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
				var response = await httpClient.PostAsync(url, content);
				response.EnsureSuccessStatusCode();

				string responseContent = await response.Content.ReadAsStringAsync();
				return Newtonsoft.Json.JsonConvert.DeserializeObject<T> (responseContent);
			}
		}
	}
}


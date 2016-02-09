using Newtonsoft.Json;

namespace TorontoPartyAdvisor
{
	public class FacebookUser
	{
		[JsonProperty(PropertyName = "id")]
		public string FacebookId { get; set; }

		[JsonProperty(PropertyName = "first_name")]
		public string FirstName { get; set; }

		[JsonProperty(PropertyName = "last_name")]
		public string LastName { get; set; }

		[JsonProperty(PropertyName = "gender")]
		public string Gender { get; set; }

		[JsonProperty(PropertyName = "email")]
		public string Email { get; set; }

		[JsonProperty(PropertyName = "timezone")]
		public int TimeZone { get; set; }

		[JsonProperty(PropertyName = "locale")]
		public string Locale { get; set; }

		[JsonProperty(PropertyName = "access_token")]
		public string AccessToken { get; set; }
	}
}


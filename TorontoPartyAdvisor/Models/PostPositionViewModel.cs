using Newtonsoft.Json;

namespace TorontoPartyAdvisor
{
	public class PostPositionViewModel
	{
		[JsonProperty(PropertyName="accessToken")]
		public string AccessToken { get; set; }

		[JsonProperty(PropertyName="latitude")]
		public double Latitude { get; set; }

		[JsonProperty(PropertyName="longitude")]
		public double Longitude { get; set; }
	}
}


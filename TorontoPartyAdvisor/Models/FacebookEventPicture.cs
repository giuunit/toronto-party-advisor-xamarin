using System;
using Newtonsoft.Json;

namespace TorontoPartyAdvisor
{
	public class FacebookEventPictureData
	{
		[JsonProperty(PropertyName = "is_silhouette")]
		public bool IsSilhouette { get; set; }

		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }
	}

	public class FacebookEventPicture
	{
		[JsonProperty(PropertyName = "data")]
		public FacebookEventPictureData Data { get; set; }
	}
}


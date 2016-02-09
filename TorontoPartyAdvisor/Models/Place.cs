using Newtonsoft.Json;


namespace TorontoPartyAdvisor
{
	public class Place
	{
		[JsonProperty(PropertyName = "Id")]
		public int Id { get;set; }

		[JsonProperty(PropertyName = "Name")]
		public string Name { get;set; }

		[JsonProperty(PropertyName = "Latitude")]
		public float Latitude { get;set; }

		[JsonProperty(PropertyName = "Longitude")]
		public float Longitude { get;set; }

		[JsonProperty(PropertyName = "Address")]
		public string Address { get;set; }

		[JsonProperty(PropertyName = "ImagePath")]
		public string ImagePath {get;set;}

		[JsonProperty(PropertyName = "FacebookId")]
		public string FacebookId{get;set;}

	}
}


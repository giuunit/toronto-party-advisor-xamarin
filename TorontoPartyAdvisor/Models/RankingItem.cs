using Newtonsoft.Json;

namespace TorontoPartyAdvisor
{
	public class RankingItem
	{
		[JsonProperty(PropertyName = "FirstName")]
		public string FirstName{ get; set; }

		[JsonProperty(PropertyName = "LastName")]
		public string LastName { get; set; }

		[JsonProperty(PropertyName = "Score")]
		public int Score { get;set; } 

		[JsonProperty(PropertyName = "FacebookId")]
		public string FacebookId { get;set; }

		[JsonIgnore]
		public string Name 
		{ 
			get 
			{
				//if there is not enough users we fill the ranking
				return 
					string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) ? 
					"XXX" : 
					string.Format ("{0} {1}.", FirstName, LastName[0]);
			} 
		}

		[JsonIgnore]
		public string FacebookPictureUrl
		{
			get
			{
				//image is an embedded resource
				return string.IsNullOrWhiteSpace(FacebookId) ?
					"facebookdefault.png" :
					FacebookHelper.GetPicture (FacebookId, Const.FACEBOOK_PIC_SIZE_RANKING);
			}
		}
	}
}


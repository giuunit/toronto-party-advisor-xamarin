namespace TorontoPartyAdvisor
{
	public class UserInfo
	{
		public int Points {	get;set;}
		public string FirstName { get;set;}
		public string LastName { get;set;}
		public string FacebookId { get; set; }
		public string Gender { get; set; }

		public string FullName
		{ 
			get 
			{
				return string.Format ("{0} {1}.", FirstName, LastName[0]);
			} 
		}

		public string FacebookPictureUrl
		{
			get
			{
				return FacebookHelper.GetPicture(FacebookId, Const.FACEBOOK_PIC_SIZE_PROFILE);
			}
		}
	}
}


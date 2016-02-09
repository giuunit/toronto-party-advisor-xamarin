using System;

namespace TorontoPartyAdvisor
{
	public class PlaceDetails
	{
		public int PlaceId { get;set; }
		public int MaleUsers { get; set; }
		public int FemaleUsers { get; set; }

		public int Users 
		{
			get 
			{
				return MaleUsers + FemaleUsers;
			}
		}

		public float MalePercentage 
		{
			get 
			{
				if (Users == 0)
					return 0;

				return (float)MaleUsers / (float)Users;
			}
		}

		public float FemalePercentage 
		{
			get 
			{
				if (Users == 0)
					return 0;

				return (float)FemaleUsers / (float)Users;
			}
		}
			
		public string FemalePercentageString {
			get 
			{
				return (Math.Round(FemalePercentage, 2)*100) + " %";
			}
		}

		public string MalePercentageString 
		{
			get {
				return (Math.Round(MalePercentage, 2)*100) + " %";
			}
		}
	}
}


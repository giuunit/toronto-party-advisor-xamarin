using System;

namespace TorontoPartyAdvisor
{
	public class PlaceDistance
	{
		public readonly Place Place;
		public readonly Position Position;

		public PlaceDistance (Place place, Position position)
		{
			Place = place;
			Position = position;
		}

		public string Name
		{
			get{
				return Place.Name;
			}
		}

		public string ImagePath
		{ 
			get { 
				return Place.ImagePath; 
			} 
		}

		public double? Distance
		{
			get
			{
				if (Position == null)
					return null;

				return MathPosition.Distance (Place.Latitude, Place.Longitude, Position.Latitude, Position.Longitude);
			}
		}

		public string FormattedDistance
		{
			get
			{
				if(!Distance.HasValue)
					return "?";
				
				return FormatHelper.FormatDistance(Distance.Value);
			}
		}
	}
}


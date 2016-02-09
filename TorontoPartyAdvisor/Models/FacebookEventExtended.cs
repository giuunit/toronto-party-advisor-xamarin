using System;

namespace TorontoPartyAdvisor
{
	public class FacebookEventExtended : FacebookEvent
	{
		public string PlaceName {
			get;
			set;
		}
		public int PlaceId {
			get;
			set;
		}
	}
}


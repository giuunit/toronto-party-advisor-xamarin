using System;

namespace TorontoPartyAdvisor
{
	public class GPSNotEnabledException : Exception
	{
		public GPSNotEnabledException ()
		{
		}

		public GPSNotEnabledException (string message) : base (message)
		{
		}
	}
}


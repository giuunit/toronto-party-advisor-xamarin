using System;

namespace TorontoPartyAdvisor
{
	public static class FormatHelper
	{
		public static string GetFormattedDay(DateTimeOffset date)
		{
			string toReturn = null;

			var days = (date.Day - DateTimeOffset.Now.Day);

			if (days == 0) {
				toReturn = "today";	
			} else if (days == 1) {
				toReturn = "tomorrow";
			} else {
				toReturn = string.Format ("{0:MM/dd}", date);
			}

			return toReturn;
		}

		/// <summary>
		/// Formats the distance.
		/// </summary>
		/// <returns>The distance in meters</returns>
		/// <param name="distance">Distance.</param>
		public static string FormatDistance(double distance)
		{
			var km = distance / 1000;
			var roundedKm = Math.Round (km, 1);
			return string.Format ("{0}km", roundedKm);
		}
	}
}


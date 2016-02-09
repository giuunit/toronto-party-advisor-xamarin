using System;

namespace TorontoPartyAdvisor
{
	public class MathPosition
	{
		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public MathPosition()
		{

		}

		public MathPosition(double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		public bool HasPositionInItsRadius(MathPosition obj, int radiusMeters)
		{
			return this.Distance(obj) <= radiusMeters;
		}

		/// <summary>
		/// http://www.geodatasource.com/developers/c-sharp
		/// </summary>
		/// <param name="lat"></param>
		/// <param name="lon"></param>
		/// <returns>Distance in meters</returns>
		public double Distance(double lat, double lon)
		{
			double theta = Longitude - lon;
			double dist = Math.Sin(GeometryHelper.DegreeToRadian(Latitude)) * Math.Sin(GeometryHelper.DegreeToRadian(lat)) + Math.Cos(GeometryHelper.DegreeToRadian(Latitude)) * Math.Cos(GeometryHelper.DegreeToRadian(lat)) * Math.Cos(GeometryHelper.DegreeToRadian(theta));
			dist = Math.Acos(dist);
			dist = GeometryHelper.RadianToDegree(dist);
			dist = dist * 60 * 1.1515;

			//meters by default
			dist = dist * 1.609344 * 1000;

			return (dist);
		}

		public double Distance(MathPosition obj)
		{
			return Distance(obj.Latitude, obj.Longitude);
		}

		public static double Distance(double lat1, double lon1, double lat2, double lon2)
		{
			var pos1 = new MathPosition (lat1, lon1);
			var pos2 = new MathPosition (lat2, lon2);

			return pos1.Distance (pos2);
		}
	}
}


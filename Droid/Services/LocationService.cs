using TorontoPartyAdvisor.Droid;
using Android.Locations;
using Android.App;
using Android.Content;
using System.Collections.Generic;

[assembly: Xamarin.Forms.Dependency (typeof (LocationService))]
namespace TorontoPartyAdvisor.Droid
{
	public class LocationService : ILocationService
	{
		private LocationManager _locMan;
		private PendingIntent _pi;
		private List<Position> _positions = new List<Position>();

		#region ILocationService implementation

		public void StartService ()
		{
			_locMan = Application.Context.GetSystemService (Context.LocationService) as LocationManager;

			if (_locMan.GetProvider (LocationManager.GpsProvider) != null && _locMan.IsProviderEnabled (LocationManager.GpsProvider)) 
			{
				_pi = PendingIntent.GetBroadcast(Application.Context, 0, new Intent(), PendingIntentFlags.UpdateCurrent);
				_locMan.RequestLocationUpdates(LocationManager.GpsProvider, 2000, 5, _pi);
				_locMan.GetLastKnownLocation (LocationManager.GpsProvider);
			}

			else 
				throw new GPSNotEnabledException (Const.NO_GPS_ERROR_MESSAGE);
		}

		public void OnPause ()
		{
			_locMan.RemoveUpdates(_pi);
		}

		public void OnResume ()
		{
			_locMan.RequestLocationUpdates(LocationManager.GpsProvider, 2000, 5, _pi);
		}

		public Position GetPosition ()
		{
			Position toReturn = null;

			var location = _locMan.GetLastKnownLocation (LocationManager.GpsProvider);

			if (location != null) {
				toReturn = new Position {
					Latitude = location.Latitude,
					Longitude = location.Longitude
				};

				return toReturn;
			}

			return new Position () {
				Latitude = 43.645406,
				Longitude = -79.394101
			};

			//throw new PositionNotFoundException (Const.NO_GPS_POSITION_ERROR_MESSAGE);
		}

		#endregion
	}
}


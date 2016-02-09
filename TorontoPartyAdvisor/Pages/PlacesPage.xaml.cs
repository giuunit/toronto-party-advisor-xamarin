using Xamarin.Forms;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Net;
using System.Linq;

namespace TorontoPartyAdvisor
{
	public partial class PlacesPage : ContentPage
	{
		private AkavacheEntity _db;

		public PlacesPage ()
		{
			InitializeComponent ();
			_db = new AkavacheEntity ();
		}

		public void DisableLoader()
		{
			aiLoader.IsRunning = false;
			aiLoader.IsEnabled = false;
			aiLoader.IsVisible = false;
		}

		public void EnableLoader()
		{
			aiLoader.IsRunning = true;
			aiLoader.IsEnabled = true;
			aiLoader.IsVisible = true;
		}

		protected async override void OnAppearing ()
		{
			List<Place> places; 

			EnableLoader ();

			bool exceptionFlag = false;
			string exceptionMsg = string.Empty;

			try {

				var service = DependencyService.Get<ILocationService> ();
				var position = service.GetPosition();

				//optimisation thing: this can be done in parallel
				DateTimeOffset datePingServer = await PartyAdvisorApiHelper.GetPlacePing ();

				long? datePingTicksLocal = await _db.GetObjectAsync<long?>(Const.PLACES_AKAVACHE_PING_KEY);

				//outdated places info
				//comparison of ticks bc it looks like the serialization of datetimeoffset is not as precise as comparison of ticks
				if (datePingTicksLocal == null || datePingTicksLocal.Value < datePingServer.Ticks) 
				{
					places = await GetServerPlaces ();

					//we save the new places
					await _db.InsertObjectAsync<List<Place>> (Const.PLACES_AKAVACHE_KEY, places);

					//we save the ping value
					await _db.InsertObjectAsync<long?> (Const.PLACES_AKAVACHE_PING_KEY, datePingServer.Ticks);
				} 

				//updated infos
				else 
				{
					//test if places already saved in database
					places = await _db.GetObjectAsync<List<Place>>(Const.PLACES_AKAVACHE_KEY);

					if (places == null) 
					{
						places = await GetServerPlaces();

						await _db.InsertObjectAsync<List<Place>> (Const.PLACES_AKAVACHE_KEY, places);
					}
				}

				List<PlaceDistance> pdList = new List<PlaceDistance>();

				foreach(var place in places)
				{
					var pd = new PlaceDistance(place, position);
					pdList.Add(pd);
				}
					
				lvPlaces.ItemsSource = pdList.OrderBy(x=>x.Distance).ToList();
			}

			catch(WebException ex)
			{
				int status = (int)ex.Status;
				exceptionFlag = true;

				//name resolution failure, aka no internet
				exceptionMsg = status == 1 ? Const.APP_REQUIRES_INTERNET : Const.GLOBAL_ERROR_MSG;
			}

			catch(PositionNotFoundException ex) 
			{
				exceptionFlag = true;
				exceptionMsg = ex.Message;
			}

			catch(Exception ex) 
			{
				exceptionFlag = true;
				exceptionMsg = Const.GLOBAL_ERROR_MSG;
			}

			finally
			{
				DisableLoader ();
			}
				
			if (exceptionFlag) 
			{
				await DisplayAlert ("Error", exceptionMsg, "Ok");
				await App.Navigation.PopModalAsync ();
			}
		}

		public async void OnItemClickedPlace(object sender, EventArgs e)
		{
			var listView = sender as ListView;

			var placeId = Convert.ToInt32((listView.SelectedItem as PlaceDistance).Place.Id);

			await this.Navigation.PushModalAsync (new PlacePage (placeId));
		}

		private async Task<List<Place>> GetServerPlaces()
		{
			var user = await _db.GetObjectAsync<FacebookUser> (Const.USER_AKAVACHE_KEY);

			//no token ? redirect to login page
			if (user == null || string.IsNullOrWhiteSpace (user.AccessToken)) 
			{
				await Navigation.PushModalAsync (new LoginPage ());
			}

			var places = await PartyAdvisorApiHelper.GetPlaces (user.AccessToken);

			return places;
		}
	}
}


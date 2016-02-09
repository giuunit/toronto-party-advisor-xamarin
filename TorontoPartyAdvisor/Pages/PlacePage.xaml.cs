using System.Collections.Generic;

using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace TorontoPartyAdvisor
{
	public partial class PlacePage : ContentPage
	{
		public readonly int PlaceId;
		private AkavacheEntity _db;
		private string _accessToken;
		private FacebookEvent _facebookEvent;

		public PlacePage (int id)
		{
			InitializeComponent ();
			PlaceId = id;
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
			EnableLoader ();

			//if we reach this point; places must be in database
			var places = await _db.GetObjectAsync<List<Place>>(Const.PLACES_AKAVACHE_KEY);
			var user = await _db.GetObjectAsync<FacebookUser> (Const.USER_AKAVACHE_KEY);

			_accessToken = user.AccessToken;

			var place = places.SingleOrDefault (x=>x.Id == PlaceId);
			lblPlaceName.Text = place.Name;
			lblPlaceAddress.Text = place.Address;

			await FillPage (user.AccessToken);

			DisableLoader ();

			FacebookEvent lastFbEvent = null;

			try
			{
				lastFbEvent = await PartyAdvisorApiHelper.GetNextFacebookEvent (user.AccessToken, place.FacebookId);
				var facebookHours = await PartyAdvisorApiHelper.GetHours(user.AccessToken, place.FacebookId);

				if(facebookHours != null && !string.IsNullOrWhiteSpace(facebookHours.StartTime))
				{
					lblHours.Text = string.Format("[{0}-{1}]",facebookHours.StartTime, string.IsNullOrWhiteSpace(facebookHours.EndTime) ? "?" : facebookHours.EndTime);
				}
			}

			catch(Exception ex) 
			{
				//TODO log error
			}

			if (lastFbEvent != null && lastFbEvent.EndTime > DateTimeOffset.Now) {
				lblFacebookLink.IsVisible = true;
				_facebookEvent = lastFbEvent;
				lblFacebookLink.IsUnderline = true;
			}
		}

		private async Task<PlaceDetails> GetDetails(string accessToken)
		{
			PlaceDetails details = null;

			try
			{
				details = await PartyAdvisorApiHelper.GetGenderStats (accessToken, PlaceId);
				lblLastUpdate.Text = string.Format("Last Update\n [{0:hh:mm tt}]", DateTime.Now);
				return details;
			}

			catch(Exception  ex)
			{
				return null;
			}
		}

		private PlaceDetails GetFakeDetails ()
		{
			var details = new PlaceDetails ();

			details.FemaleUsers = 20;
			details.MaleUsers = 30;
			details.PlaceId = this.PlaceId;

			lblLastUpdate.Text = string.Format("Last Update\n [{0:hh:mm tt}]", DateTime.Now);

			return details;
		}

		private async Task FillPage(string accessToken)
		{
			//var details = GetFakeDetails ();
			var details = await GetDetails(accessToken);

			//error while getting datas
			if (details == null) {
				await DisplayAlert ("Error", Const.GLOBAL_ERROR_MSG, "Ok");
				// We go back to the old menu
				await App.Navigation.PopModalAsync ();
			} 

			//no error
			else 
			{
				if (details.Users > 0) {
					lblNoUser.IsVisible = false;

					lblNbrUsers.Text = details.Users + " Users detected";

					lblNbrMaleUsers.IsVisible = true;
					lblNbrFemaleUsers.IsVisible = true;

					lblNbrMaleUsers.Text = details.MalePercentageString;
					lblNbrFemaleUsers.Text = details.FemalePercentageString;
				} 

				else 
				{
					lblNoUser.IsVisible = true;

					lblNbrUsers.Text = "No user detected";
					lblNoUser.Text = Const.NO_USER_MSG_DETAILS;

					lblNbrMaleUsers.IsVisible = false;
					lblNbrFemaleUsers.IsVisible = false;
				}
			}
		}

		private async Task Refresh()
		{
			EnableLoader ();

			await FillPage (_accessToken);

			DisableLoader ();
		}

		public async void OnClickRefreshButton(object sender, EventArgs e)
		{
			await Refresh ();
		}

		public async void OnLinkEventClickedProfile(object sender, EventArgs e)
		{
			if (_facebookEvent != null) 
			{
				var fbEvent = new FacebookEventExtended {
					AttendingCount = _facebookEvent.AttendingCount,
					Description = _facebookEvent.Description,
					EndTime = _facebookEvent.EndTime,
					Id = _facebookEvent.Id,
					MaybeCount = _facebookEvent.MaybeCount,
					Name = _facebookEvent.Name,
					Picture = _facebookEvent.Picture,
					StartTime = _facebookEvent.StartTime,
					PlaceId = PlaceId,
					PlaceName = lblPlaceName.Text
				};

				var page = new FacebookEventPage (fbEvent);

				await App.Navigation.PushModalAsync (page);
			}
		}
	}
}


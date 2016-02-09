using Xamarin.Forms;
using System;

namespace TorontoPartyAdvisor
{
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}

		public async void OnButtonClickedPlaces(object sender, EventArgs e)
		{
			await this.Navigation.PushModalAsync (new PlacesPage ());
		}

		public async void OnButtonClickedProfile(object sender, EventArgs e)
		{
			await this.Navigation.PushModalAsync (new UserPage ());
		}

		public async void OnButtonClickedRanking(object sender, EventArgs e)
		{
			await this.Navigation.PushModalAsync (new RankingPage ());
		}
	}
}


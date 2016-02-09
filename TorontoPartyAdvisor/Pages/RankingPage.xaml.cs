using Xamarin.Forms;
using System.Net;
using System;

namespace TorontoPartyAdvisor
{
	public partial class RankingPage : ContentPage
	{
		private AkavacheEntity _db;

		public RankingPage ()
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
			EnableLoader ();

			var user = await _db.GetObjectAsync<FacebookUser> (Const.USER_AKAVACHE_KEY);
			bool exceptionFlag = false;
			string exceptionMsg = string.Empty;

			try 
			{
				var rankingItems = await PartyAdvisorApiHelper.GetRanking (user.AccessToken);
				lvRanking.ItemsSource = rankingItems;
			}

			catch(WebException e) 
			{
				int status = (int)e.Status;

				exceptionFlag = true;

				//name resolution failure, aka no internet
				if(status == 1)
				{
					exceptionMsg = Const.APP_REQUIRES_INTERNET;
				} 
				else 
				{
					exceptionMsg = Const.GLOBAL_ERROR_MSG;
				}
			}
			catch(Exception ex) 
			{
				exceptionFlag = true;
				exceptionMsg = Const.GLOBAL_ERROR_MSG;
			}

			DisableLoader ();

			if (exceptionFlag) 
			{
				await DisplayAlert ("Error", exceptionMsg, "Ok");
				await App.Navigation.PopModalAsync ();
			}
		}
	}
}


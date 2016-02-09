using Xamarin.Forms;
using System;
using System.Net;

namespace TorontoPartyAdvisor
{
	public partial class UserPage : ContentPage
	{
		private AkavacheEntity _db;

		public UserPage ()
		{
			InitializeComponent ();
			_db = new AkavacheEntity ();
		}

		protected async override void OnAppearing ()
		{
			EnableLoader ();

			var user = await _db.GetObjectAsync<FacebookUser> (Const.USER_AKAVACHE_KEY);
			bool exceptionFlag = false;
			string exceptionMsg = string.Empty;

			try
			{
				var info = await PartyAdvisorApiHelper.GetUserInfo (user.AccessToken);

				lblUserName.Text = info.FullName;
				imgUser.Source = info.FacebookPictureUrl;
				lblUserPoints.Text = string.Format("{0} pts", info.Points);
				lblGender.Text = string.Format("{0}{1}", (char.ToUpper(info.Gender[0])),info.Gender.Substring(1));
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

		public async void OnButtonClickedLogOff(object sender, EventArgs e)
		{
			var db = new AkavacheEntity ();
			await db.RemoveObjectAsync<FacebookUser> (Const.USER_AKAVACHE_KEY);

			await Navigation.PushModalAsync (new LoginPage ());
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
	}
}


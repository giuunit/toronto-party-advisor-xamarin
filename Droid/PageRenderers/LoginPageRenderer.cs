using System; 
using Android.App;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using TorontoPartyAdvisor;
using TorontoPartyAdvisor.Droid;
using Xamarin.Auth;
using Android.Net;

[assembly: ExportRenderer (typeof (LoginPage), typeof (LoginPageRenderer))]

namespace TorontoPartyAdvisor.Droid
{
	public class LoginPageRenderer : PageRenderer
	{
		protected async override void OnElementChanged (ElementChangedEventArgs<Page> e)
		{
			var activity = this.Context as Activity;

			var connectivityManager = (ConnectivityManager)activity.GetSystemService(Activity.ConnectivityService);
			var activeConnection = connectivityManager.ActiveNetworkInfo;

			if (activeConnection == null || !activeConnection.IsConnected) {
				var dlgAlert = (new AlertDialog.Builder (activity)).Create ();
				dlgAlert.SetMessage ("No active connection is available, please try to access to an active connection");
				dlgAlert.SetTitle ("Error");
				dlgAlert.SetButton ("OK", (s, ev) => {
					dlgAlert.Dismiss ();
					Environment.Exit (0);
				});
				dlgAlert.Show ();
			} 

			else 
			{
				var auth = new OAuth2Authenticator (
					clientId: Const.FACEBOOK_APP_ID, // your OAuth2 client id
					scope: "email", // the scopes for the particular API you're accessing, delimited by "+" symbols
					authorizeUrl: new System.Uri ("https://m.facebook.com/dialog/oauth/"), // the auth URL for the service
					redirectUrl: new System.Uri ("http://www.facebook.com/connect/login_success.html")); // the redirect URL for the service

				auth.Completed += async (sender, eventArgs) => 
				{
					if (eventArgs.IsAuthenticated) 
					{
						//we get the access token
						var token = eventArgs.Account.Properties["access_token"];

						try
						{
							var fbUser = await FacebookHelper.GetAsync(token);
							fbUser.AccessToken = token;
							await PartyAdvisorApiHelper.Login(fbUser);
							await UserHelper.SaveUserAsync(fbUser);
							await App.Navigation.PopModalAsync();
						}

						catch(Exception ex)
						{
							//todo: log what happened here

							var dlgAlert = (new AlertDialog.Builder (activity)).Create ();
							dlgAlert.SetMessage ("An error occured");
							dlgAlert.SetTitle ("Error");
							dlgAlert.SetButton ("OK", (s, ev) => dlgAlert.Dismiss ());
							dlgAlert.Show ();
							Environment.Exit (0);
						}
					} 

					else 
					{
						// The user cancelled

						var dlgAlert = (new AlertDialog.Builder (activity)).Create ();
						dlgAlert.SetMessage ("This app needs you to be connected using Facebook");
						dlgAlert.SetTitle ("Error");
						dlgAlert.SetButton ("OK", (s, ev) => dlgAlert.Dismiss ());
						dlgAlert.Show ();
						Environment.Exit (0);

					}
				};

				activity.StartActivity (auth.GetUI(activity));
			}
		}
	}
}


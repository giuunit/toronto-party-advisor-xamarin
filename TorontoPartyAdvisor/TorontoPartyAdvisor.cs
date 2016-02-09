using Xamarin.Forms;
using System.Threading.Tasks;

namespace TorontoPartyAdvisor
{
	public class App : Application
	{
		public static INavigation Navigation;

		public App()
		{
			MainPage = new MainPage ();
		}

		protected async override void OnStart ()
		{
			Navigation = MainPage.Navigation;

			var isAuthenticated = await IsAuthenticated ();
			if (!isAuthenticated) 
			{
				await Navigation.PushModalAsync (new LoginPage ());
			}

			var service = DependencyService.Get<ILocationService> ();
			service.StartService ();
		}

		private static async Task<bool> IsAuthenticated()  
		{
			var user = await new AkavacheEntity ().GetObjectAsync<FacebookUser> (Const.USER_AKAVACHE_KEY);
			return user != null && !string.IsNullOrWhiteSpace (user.AccessToken);
		}
	}
}


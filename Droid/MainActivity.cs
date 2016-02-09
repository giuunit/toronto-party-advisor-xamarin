using Android.App;
using Android.Content.PM;
using Android.OS;
using XLabs.Forms;
using ImageCircle.Forms.Plugin.Droid;

namespace TorontoPartyAdvisor.Droid
{
	[Activity (ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : XFormsApplicationDroid
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			ImageCircleRenderer.Init ();

			LoadApplication (new App ());
		}
	}
}


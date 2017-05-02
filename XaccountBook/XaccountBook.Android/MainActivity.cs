using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace XaccountBook.Droid
{
	[Activity(Label = "Xaccount Book", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			Xamarin.Forms.Forms.Init(this, bundle);
			LoadApplication(new App());

			// start sms hooking service
			StartService(new Intent(this, typeof(Communicator)));
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}
	}
}


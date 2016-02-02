using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Content;

namespace com.tarabel.bluetoothnotify
{
	[Activity (Label = "BluetoothNotify", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			button.Click += delegate {
				onButtonClick ();
			};
			;
			StartService (new Intent (this, typeof(BluetoothLowEnergySearchService)));
		}

		protected void onButtonClick ()
		{
			
			this.Finish ();
		}
	}
}

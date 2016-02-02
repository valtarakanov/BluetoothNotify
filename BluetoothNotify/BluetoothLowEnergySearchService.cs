using System;
using Android.App;
using Android.Widget;
using Android.Content;
using Android.OS;

namespace com.tarabel.bluetoothnotify
{
	[Service]
	public class BluetoothLowEnergySearchService: Service
	{
		bool isStarted = false;

		public override Android.OS.IBinder OnBind (Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
		{
			if (!isStarted) {
				CreateNotifications ("starting");
				isStarted = true;
			} else {
				CreateNotifications("resuming");
			}
			return StartCommandResult.RedeliverIntent;
		}

		void CreateNotifications(string status)
		{
			#if DEBUG
				Notification notification = new Notification.Builder (this)
				.SetContentTitle ("BluetoothLowEnergySearchService " + status)
				.SetContentText ("message sent at" + System.DateTime.Now.ToLongTimeString ())
				.SetSmallIcon (Resource.Drawable.icon)
				.Build ();

			NotificationManager nMgr = (NotificationManager)GetSystemService (NotificationService);
			nMgr.Notify (0, notification);

			var myHandler = new Handler ();
			myHandler.Post (() =>  {
				Toast.MakeText (this, "BluetoothLowEnergySearchService "+ status, ToastLength.Long).Show ();
			});
			#endif
		}




	}
}


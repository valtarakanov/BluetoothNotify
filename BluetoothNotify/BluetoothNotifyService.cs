/*using System;
using Android.App;
using Android.Widget;
using Android.Content;
using Android.OS;

namespace BluetoothNotify
{
	[Service]
	public class BluetoothNotifyService: Service
	{
		BTConnectedStateRecevier btReceiver;



		public override Android.OS.IBinder OnBind (Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
		{
			if (btReceiver == null) {
				CreateNotifications ("Creating");
				btReceiver = new BTConnectedStateRecevier ();
			} else {
				CreateNotifications("Resuming");
			}
			return StartCommandResult.RedeliverIntent;
		}

		void CreateNotifications(string status)
		{
			Notification notification = new Notification.Builder (this)
				.SetContentTitle ("BluetoothNotification " + status)
				.SetContentText ("message sent at" + System.DateTime.Now.ToLongTimeString ())
				.SetSmallIcon (Resource.Drawable.icon)
				//.SetLargeIcon (Resource.Drawable.icon)
				.Build ();

			NotificationManager nMgr = (NotificationManager)GetSystemService (NotificationService);
			//var notification = new Notification (Resource.Drawable.icon, "Message from BluetoothNotifyService service");
			//var pendingIntent = PendingIntent.GetActivity (this, 0, new Intent (this, typeof(MainActivity)), 0);
			//notification.SetLatestEventInfo (this, "BluetoothNotifyService Service Notification", "Message from BluetoothNotifyService service", pendingIntent);
			nMgr.Notify (0, notification);

			var myHandler = new Handler ();
			myHandler.Post (() =>  {
				Toast.MakeText (this, "BluetoothNotifyService "+ status, ToastLength.Long).Show ();
			});
		}




	}
}

*/
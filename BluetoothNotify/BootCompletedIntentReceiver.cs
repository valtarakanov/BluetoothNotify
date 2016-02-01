using System;
using Android.Content;
using Android.App;
using Android.Widget;
using Android.OS;

namespace BluetoothNotify
{
	[BroadcastReceiver]
	[IntentFilter (new[] {Intent.ActionBootCompleted})]
	public class BootCompletedIntentReceiver : BroadcastReceiver 
	{
		public override void OnReceive (Context context, Intent intent)
		{
			//Intent pushIntent = new Intent(context, typeof(BluetoothNotifyService));
			//pushIntent.AddFlags (ActivityFlags.NewTask);
			//context.StartService(pushIntent);
			if ("android.intent.action.BOOT_COMPLETED".ToLower().Equals (intent.Action.ToLower())) {
				CreateNotifications (context, "boot_completed");
			}
			else if ("android.intent.action.QUICKBOOT_POWERON".ToLower().Equals (intent.Action.ToLower())) {
				CreateNotifications (context, "quickboot");
			} else {
				CreateNotifications (context, intent.Action);
			}

		}

		public void CreateNotifications(Context context, string action)
		{
			Notification notification = new Notification.Builder(context)
				.SetContentTitle ("Startup notification " + action)
				.SetContentText ("message sent at" + System.DateTime.Now.ToLongTimeString ())
				.SetSmallIcon (Resource.Drawable.icon)
				.Build ();

			NotificationManager nMgr = (NotificationManager)context.GetSystemService (Android.Content.ContextWrapper.NotificationService);
			nMgr.Notify (0, notification);

			var myHandler = new Handler ();
			myHandler.Post (() =>  {
				Toast.MakeText (context, "Startup notification " + action, ToastLength.Long).Show ();
			});
		}
	}
}


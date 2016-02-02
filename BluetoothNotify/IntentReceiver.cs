using System;
using Android.Content;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Bluetooth;

namespace com.tarabel.bluetoothnotify
{
	[BroadcastReceiver]
	[IntentFilter (new[] {Intent.ActionBootCompleted, "android.intent.action.QUICKBOOT_POWERON", BluetoothDevice.ActionAclConnected, BluetoothDevice.ActionAclDisconnectRequested, BluetoothDevice.ActionAclDisconnected})]
	public class IntentReceiver : BroadcastReceiver 
	{
		NotificationProcessor _notificationProcessor;

		public override void OnReceive (Context context, Intent intent)
		{
			Log.Info ("com.tarabel.bluetoothnotify", "received " + intent.Action);
			try {
				if ("android.intent.action.BOOT_COMPLETED".ToLower().Equals (intent.Action.ToLower())) {
					CreateNotifications (context, "boot_completed");
					StartService (context);
				}else if ("android.intent.action.QUICKBOOT_POWERON".ToLower().Equals (intent.Action.ToLower())) {
					CreateNotifications (context, "quickboot");
					StartService (context);
				}else if (BluetoothDevice.ActionAclConnected.ToLower().Equals (intent.Action.ToLower())) {
					CreateNotifications (context, "bluetooth connect");
					ProcessBluetoothIntent (context, intent);
				}else if (BluetoothDevice.ActionAclDisconnectRequested.ToLower().Equals (intent.Action.ToLower()) ||
					BluetoothDevice.ActionAclDisconnected.ToLower().Equals (intent.Action.ToLower())) {
					CreateNotifications (context, "bluetooth disconnect");
					ProcessBluetoothIntent (context, intent);
				} else {
					CreateNotifications (context, intent.Action);
				}
			} catch (Exception ex) {
				Log.Info ("com.tarabel.bluetoothnotify", "error processing intents " + ex.ToString());
			}
		}

		private void StartService(Context context)
		{
			try {
				Intent serviceStartIntent = new Intent (context, typeof(com.tarabel.bluetoothnotify.BluetoothLowEnergySearchService));
				serviceStartIntent.AddFlags (ActivityFlags.NewTask);
				context.StartService (serviceStartIntent);
			} catch (Exception ex) {
				Log.Info ("com.tarabel.bluetoothnotify", "error while starting service " + ex.ToString());
			}
		}

		void ProcessBluetoothIntent (Context context, Intent intent)
		{
			try {
				if (_notificationProcessor == null) {
					_notificationProcessor = new NotificationProcessor ();
				}
				_notificationProcessor.ProcessIntent (context, intent);
			} catch (Exception ex) {
				Log.Info ("com.tarabel.bluetoothnotify", "error while passing intent to NotificationProcessor " + ex.ToString());
			}
		}

		private void CreateNotifications(Context context, string action, bool isMessageNeeded = true, bool isToastNeeded = true)
		{
			#if DEBUG
			try {
				if (isMessageNeeded)
				{
					Notification notification = new Notification.Builder(context)
						.SetContentTitle ("BluetoothNotify intent received " + action)
						.SetContentText ("message sent at" + System.DateTime.Now.ToLongTimeString ())
						.SetSmallIcon (Resource.Drawable.icon)
						.Build ();

					NotificationManager nMgr = (NotificationManager)context.GetSystemService (Android.Content.ContextWrapper.NotificationService);
					nMgr.Notify (0, notification);
				}

				if (isToastNeeded)
				{
					var myHandler = new Handler ();
					myHandler.Post (() =>  {
						Toast.MakeText (context, "BluetoothNotify intent received " + action, ToastLength.Long).Show ();
					});
				}
				
			} catch (Exception ex) {
				Log.Info ("com.tarabel.bluetoothnotify", "CreateNotification error in IntentReceiver " + ex.ToString());
			}
			#endif
		}
	}
}


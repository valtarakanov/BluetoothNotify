using System;
using Android.Bluetooth;
using Android.Content;
using System.Collections.Generic;
using Android.Widget;
using Android.OS;



namespace com.tarabel.bluetoothnotify
{
	public class BluetoothProcessor
	{
		NotificationProcessor _notificationProcessor = null;

		public BluetoothProcessor ()
		{
		}

		public List<string> GetDeviceList(Context context)
		{
			
			List<string> result = new List<string> ();

			// Get local Bluetooth adapter
			BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;

			// If the adapter is null, then Bluetooth is not supported
			if (bluetoothAdapter == null) {
				var myHandler = new Handler ();
				myHandler.Post (() =>  {
					Toast.MakeText (context, "Bluetooth is not available", ToastLength.Long).Show ();
				});
				return result;
			}

			// If bluetooth is not enabled, ask to enable it
			if (!bluetoothAdapter.IsEnabled)
			{
				var myHandler = new Handler ();
				myHandler.Post (() =>  {
					Toast.MakeText (context, "Bluetooth is not enabled, please enable it", ToastLength.Long).Show ();
				});
				var enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
			}

			// Get connected devices
			var listOfDevices = bluetoothAdapter.BondedDevices;
			if (listOfDevices.Count > 0)
			{
				foreach (var bluetoothDevice in listOfDevices)
				{
					if (!result.Contains(bluetoothDevice.Name + ":" + bluetoothDevice.Address))
						result.Add(bluetoothDevice.Name + ":" + bluetoothDevice.Address);
				}
			}

			return result;
		}


		public void ProcessIntent(Context context, Intent intent)
		{
			if (intent.Action == BluetoothDevice.ActionAclConnected)
			{
				BluetoothDevice bd = (BluetoothDevice)intent.Extras.Get ("android.bluetooth.device.extra.DEVICE");
				//TODO: update with better wording
				if (SettingsProcessor.IsDeviceSelectedForNotifications (bd.Name, bd.Address, context)) {
					string foo = bd.Name + " " + bd.Address;
					SendMessage (" connected " + foo);
				}
			}

			if (intent.Action == BluetoothDevice.ActionAclDisconnectRequested ||
				intent.Action == BluetoothDevice.ActionAclDisconnected )
			{
				BluetoothDevice bd = (BluetoothDevice)intent.Extras.Get ("android.bluetooth.device.extra.DEVICE");
				//TODO: update with better wording
				if (SettingsProcessor.IsDeviceSelectedForNotifications (bd.Name, bd.Address, context)) {
					string foo = bd.Name + " " + bd.Address;
					SendMessage (" disconnected " + foo);
				}
			}
		}

		private void SendMessage(string message)
		{
			if (_notificationProcessor == null) {
				_notificationProcessor = new NotificationProcessor ();
			}

			_notificationProcessor.SendMessage (message);
		}
	}
}


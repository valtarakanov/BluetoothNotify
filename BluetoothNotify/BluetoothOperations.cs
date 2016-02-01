/*
using System;
using Android.Locations;
using System.Collections.Generic;

using Android.App;
using Android.Widget;
using Android.OS;
using System.Linq;
using System.Text;
using Android.Bluetooth;
using System.Collections;
using Android.Content;


namespace BluetoothNotify
{
	public class BluetoothOperations
	{
		//LocationManager _locationManager;
		//string _locationProvider;
		BTConnectedStateRecevier btReceiver = null;

		public BluetoothOperations ()
		{
			//InitializeLocationManager ();

			btReceiver = new BTConnectedStateRecevier ();
		}

		
	}


	[BroadcastReceiver]
	[IntentFilter(new[] {BluetoothDevice.ActionAclConnected, BluetoothDevice.ActionAclDisconnectRequested, BluetoothDevice.ActionAclDisconnected})]
	public class BTConnectedStateRecevier : BroadcastReceiver
	{

		public MainActivity Bar;
		string _locationProvider;
		LocationManager _locationManager;

		public BTConnectedStateRecevier () : base ()
		{
			InitializeLocationManager ();

		}

		void InitializeLocationManager()
		{
			_locationManager = (LocationManager) Application.Context.GetSystemService(Application.LocationService);
			Criteria criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Fine
			};
			IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Any())
			{
				_locationProvider = acceptableLocationProviders.First();
			}
			else
			{
				_locationProvider = string.Empty;
			}

		}


		public override void OnReceive(Context context, Intent intent)
		{
			if (intent.Action == BluetoothDevice.ActionAclConnected)
			{
				BluetoothDevice bd = (BluetoothDevice)intent.Extras.Get ("android.bluetooth.device.extra.DEVICE");
				string 	foo = bd.Name + " " + bd.Address;
				SendMessage ("connected " + foo);
			}

			if (intent.Action == BluetoothDevice.ActionAclDisconnectRequested ||
				intent.Action == BluetoothDevice.ActionAclDisconnected )
			{
				BluetoothDevice bd = (BluetoothDevice)intent.Extras.Get ("android.bluetooth.device.extra.DEVICE");
				string 	foo = bd.Name + " " + bd.Address;
				SendMessage ("disconnected " + foo);

			}
		}


		private void SendMessage(string foo)
		{
			Location currentLocation;
			string addressText = "Unable to determine the address.";
			string locationText = "Unable to determine your location.";

			Address address = null;

			try
			{
				currentLocation = _locationManager.GetLastKnownLocation(_locationProvider);
				if (currentLocation != null)
				{
					locationText = string.Format("{0:f6},{1:f6}", currentLocation.Latitude, currentLocation.Longitude);
					Geocoder geocoder = new Geocoder(Application.Context);
					IList<Address> addressList = geocoder.GetFromLocation(currentLocation.Latitude, currentLocation.Longitude, 10);
					address  = addressList.FirstOrDefault();

					if (address != null)
					{
						StringBuilder deviceAddress = new StringBuilder();
						for (int i = 0; i < address.MaxAddressLineIndex; i++)
						{
							deviceAddress.AppendLine(address.GetAddressLine(i));
						}
						// Remove the last comma from the end of the address.
						addressText = deviceAddress.ToString();
					}
				}
			}
			catch
			{
			}

			

			//bt.GetProfileProxy(null,null,ProfileType.GattServer

			Android.Telephony.SmsManager.Default.SendTextMessage ("2623092186", null, "Message from " + locationText + " address: " + addressText + " device " + foo, null, null);

		}

	}
}

*/
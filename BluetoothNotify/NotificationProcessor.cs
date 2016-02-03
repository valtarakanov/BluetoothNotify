
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


namespace com.tarabel.bluetoothnotify
{
	public class NotificationProcessor
	{
		public NotificationProcessor ()
		{
			InitializeLocationManager ();

		}

		public MainActivity Bar;
		string _locationProvider;
		LocationManager _locationManager;


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





		public void SendMessage(string foo)
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

			Android.Telephony.SmsManager.Default.SendTextMessage ("2623092186", null, "Message from " + locationText + " address: " + addressText + " device " + foo, null, null);

		}

	}
}


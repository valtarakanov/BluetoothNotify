using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Content;
using System.Collections.Generic;
using System;

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
			Button button = FindViewById<Button> (Resource.Id.saveButton);
			button.Click += delegate {
				onButtonClick ();
			};

			StartService (new Intent (this, typeof(BluetoothLowEnergySearchService)));

			List<string> availableDevices = (new BluetoothProcessor ()).GetDeviceList (this);
			List<DeviceInfo> bluetoothDevices = SettingsProcessor.RetrievePreviousSelections (availableDevices);

			ListView deviceView = FindViewById<ListView> (Resource.Id.deviceListView);
			deviceView.Adapter = new DeviceListAdapter (this, bluetoothDevices);


		}

		protected void onButtonClick ()
		{
			ListView deviceView = FindViewById<ListView> (Resource.Id.deviceListView);
			DeviceListAdapter deviceListAdapter = deviceView.Adapter as DeviceListAdapter;

			SettingsProcessor.StoreSelections (deviceListAdapter.GetSelectedDevices ());

			this.Finish ();
		}
	}




}

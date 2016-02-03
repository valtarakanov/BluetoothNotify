using System;
using System.Collections.Generic;

namespace com.tarabel.bluetoothnotify
{
	internal static class SettingsProcessor
	{

		internal static List<DeviceInfo> RetrievePreviousSelections(List<string> availableDevices)
		{

			#if DEBUG
			availableDevices.Add ("My test device with a very long name:12342342134134");
			availableDevices.Add ("My second device:123123123");
			#endif

			//TODO: retrieve previously saved devices
				
			List<DeviceInfo> resultList = new List<DeviceInfo> ();
			foreach (var availableDevice in availableDevices) {
				DeviceInfo device = new DeviceInfo ();
				device.DeviceName = availableDevice;
				//TODO: check next line's value against previously saved devices
				device.IsSelected = false;
				resultList.Add (device);
			}

			//TODO: add devices that were previously saved, but not on current available list to the available list

			return resultList;
		}

		internal static void StoreSelections(List<DeviceInfo> bluetoothDevices)
		{
			//TODO: clear all previously saved devices

			List<DeviceInfo> resultList = new List<DeviceInfo> ();
			foreach (var device in bluetoothDevices) {
				if (device.IsSelected)
				{
					//TODO: store device as saved
				}
			}
		}
	}
}


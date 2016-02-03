using System;
using System.Collections.Generic;
using Android.Preferences;
using Android.Content;

namespace com.tarabel.bluetoothnotify
{
	internal static class SettingsProcessor
	{
		private const string _savedDevicesSettingName = "SavedDevices";

		internal static List<DeviceInfo> RetrievePreviousSelections(List<string> availableDevices, Context context)
		{

			#if DEBUG
			availableDevices.Add ("My test device with a very long name:12342342134134");
			availableDevices.Add ("My second device:123123123");
			#endif

			var prefs = context.ApplicationContext.GetSharedPreferences (_savedDevicesSettingName, FileCreationMode.WorldReadable);
			var savedDevices = prefs.GetStringSet (_savedDevicesSettingName,null);
			if (savedDevices == null)
				savedDevices = new List<string> ();
			List<DeviceInfo> resultList = new List<DeviceInfo> ();
			foreach (var availableDevice in availableDevices) {
				DeviceInfo device = new DeviceInfo ();
				device.DeviceName = availableDevice;
				device.IsSelected = savedDevices.Contains (device.DeviceName);
				if (device.IsSelected) {
					savedDevices.Remove (device.DeviceName);
				}
				resultList.Add (device);
			}

			//add what is left in the saved devices to the top
			foreach (var savedDevice in savedDevices) {
				DeviceInfo missingDevice = new DeviceInfo ();
				missingDevice.DeviceName = savedDevice;
				missingDevice.IsSelected = true;
				resultList.Add (missingDevice);
			}

			return resultList;
		}

		internal static void StoreSelections(List<DeviceInfo> bluetoothDevices, Context context)
		{
			List<string> devicesToSave = new List<string> ();
			foreach (var device in bluetoothDevices) {
				if (device.IsSelected)
				{
					devicesToSave.Add (device.DeviceName);
				}
			}
			var prefs = context.ApplicationContext.GetSharedPreferences (_savedDevicesSettingName, FileCreationMode.WorldReadable);
			var editor = prefs.Edit ();
			editor.PutStringSet (_savedDevicesSettingName, devicesToSave);
			editor.Apply ();

		}

		internal static bool IsDeviceSelectedForNotifications(string devicename, string deviceaddress, Context context)
		{
			var prefs = context.ApplicationContext.GetSharedPreferences (_savedDevicesSettingName, FileCreationMode.WorldReadable);
			var savedDevices = prefs.GetStringSet (_savedDevicesSettingName,null);
			if (savedDevices != null) {
				return savedDevices.Contains (devicename + ":" + deviceaddress);
			}
			return false;
		}
	}
}


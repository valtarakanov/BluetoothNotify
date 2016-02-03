using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;

namespace com.tarabel.bluetoothnotify
{

	internal class DeviceListAdapter : BaseAdapter<DeviceInfo>
	{

		public override long GetItemId (int position)
		{
			return position;
		}

		public override DeviceInfo this[int index] {
			get {
				return _deviceList[index];
			}
		}

		public List<DeviceInfo> GetSelectedDevices()
		{
			List<DeviceInfo> selectedDevices = new List<DeviceInfo> ();
			foreach (var device in _deviceList) {
				if (device.IsSelected) {
					selectedDevices.Add (device);
				}
			}

			return selectedDevices;
		}

		private List<DeviceInfo> _deviceList;
		private Activity _context;

		public DeviceListAdapter(Activity context, List<DeviceInfo> deviceList) : base()
		{
			_context = context;
			_deviceList = deviceList;
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			Android.Views.View view = convertView;

			if (view == null) 
				view = _context.LayoutInflater.Inflate (Resource.Layout.DeviceRow, null);

			view.FindViewById<TextView> (Resource.Id.deviceName).Text = _deviceList [position].DeviceName;
			CheckBox deviceCheckbox = view.FindViewById<CheckBox> (Resource.Id.deviceChecked);

			deviceCheckbox.Checked = _deviceList [position].IsSelected;
			deviceCheckbox.Tag = _deviceList [position].DeviceName;

			deviceCheckbox.Click += delegate (object sender, EventArgs args){
				CheckedChange (sender, args);
			};


			return view;

		}

		void CheckedChange(object sender, EventArgs e)
		{
			CheckBox checkbox = sender as CheckBox;
			if (checkbox != null) {
				string devicechecked = checkbox.Tag.ToString();
				foreach (var device in _deviceList) {
					if (device.DeviceName == devicechecked) {
						device.IsSelected = checkbox.Checked;
					}
				}
			}
		}

		public override int Count {
			get {
				return _deviceList.Count; 
			}
		}
	}
}


using System;
using System.Drawing;
using MonoTouch;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinSecurityTests.iOS;
using MonoTouch.CoreBluetooth;
using System;
using MonoTouch.CoreBluetooth;
using System.Collections.Generic;
using System.Threading.Tasks;


// This ExportRenderer command tells Xamarin.Forms to use this renderer
// instead of the built-in one for this page
[assembly:ExportRenderer(typeof(XamarinSecurityTests.BTConnectPage), typeof(XamarinSecurityTests.BTConnectPageRenderer))]

namespace XamarinSecurityTests
{
	/// <summary>
	/// Render this page using platform-specific UIKit controls
	/// </summary>
	public class BTConnectPageRenderer : PageRenderer
	{

		// event declarations
		public event EventHandler<CBDiscoveredPeripheralEventArgs> DeviceDiscovered = delegate {};
		public event EventHandler<CBPeripheralEventArgs> DeviceConnected = delegate {};
		public event EventHandler<CBPeripheralErrorEventArgs> DeviceDisconnected = delegate {};
		public event EventHandler ScanTimeoutElapsed = delegate {};


		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);

			BluetoothLEManager ();
		}

		public List<CBPeripheral> DiscoveredDevices
		{
			get { return this._discoveredDevices; }
		}
		List<CBPeripheral> _discoveredDevices = new List<CBPeripheral>();

		public List<CBPeripheral> ConnectedDevices
		{
			get { return this._connectedDevices; }
		}
		List<CBPeripheral> _connectedDevices = new List<CBPeripheral>();

		public CBCentralManager CentralBleManager
		{
			get { return this._central; }
		}
		CBCentralManager _central;


		protected void BluetoothLEManager ()
		{
			_central = new CBCentralManager (MonoTouch.CoreFoundation.DispatchQueue.CurrentQueue);
			_central.DiscoveredPeripheral += (object sender, CBDiscoveredPeripheralEventArgs e) => {
				Console.WriteLine ("DiscoveredPeripheral: " + e.Peripheral.Name);
				this._discoveredDevices.Add (e.Peripheral);
				this.DeviceDiscovered(this, e);
			};

			_central.UpdatedState += (object sender, EventArgs e) => {
				Console.WriteLine ("UpdatedState: " + _central.State);
			};


			_central.ConnectedPeripheral += (object sender, CBPeripheralEventArgs e) => {
				Console.WriteLine ("ConnectedPeripheral: " + e.Peripheral.Name);

				// when a peripheral gets connected, add that peripheral to our running list of connected peripherals
				if(!this._connectedDevices.Contains(e.Peripheral) ) {
					this._connectedDevices.Add (e.Peripheral );
				}			

				// raise our connected event
				this.DeviceConnected ( sender, e);

			};

			_central.DisconnectedPeripheral += (object sender, CBPeripheralErrorEventArgs e) => {
				Console.WriteLine ("DisconnectedPeripheral: " + e.Peripheral.Name);

				// when a peripheral disconnects, remove it from our running list.
				if ( this._connectedDevices.Contains (e.Peripheral) ) {
					this._connectedDevices.Remove ( e.Peripheral);
				}

				// raise our disconnected event
				this.DeviceDisconnected (sender, e);

			};
		}


	}
}


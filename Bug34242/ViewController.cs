using System;
using System.Collections.Generic;

using UIKit;
using CoreBluetooth;
using Foundation;

namespace Bug34242
{
	public partial class ViewController : UIViewController
	{
		private CBCentralManager _manager;
		private bool _isPendingStartScan;

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			_manager = new CBCentralManager();
			_manager.UpdatedState += OnUpdatedState;
			_manager.DiscoveredPeripheral += OnDiscoveredPeripheral;

			if (_manager.State != CBCentralManagerState.PoweredOn)
			{
				_isPendingStartScan = true;
			}
			else
			{
				_isPendingStartScan = false;
				_manager.ScanForPeripherals(null, ScanningOptions);
			}
		}
			
		private void OnUpdatedState(object sender, EventArgs eventArgs)
		{
			if (_isPendingStartScan)
			{
				if (_manager.State != CBCentralManagerState.PoweredOn)
				{
					_isPendingStartScan = true;
				}
				else
				{
					_isPendingStartScan = false;
					_manager.ScanForPeripherals(null, ScanningOptions);
				}
			}
		}

		private void OnDiscoveredPeripheral(object sender, CBDiscoveredPeripheralEventArgs e)
		{			
			try {
				Console.WriteLine ("Device Found 0x{0}: {1}", e.Peripheral.Handle.ToString ("x"), e.Peripheral.Name);
				e.Peripheral.DiscoveredService += OnDeviceDiscoveredService;
				Console.WriteLine ("Device Found 0x{0}: {1} WeakDelegate.Handle: 0x{2}", e.Peripheral.Handle.ToString ("x"), e.Peripheral.Name, e.Peripheral.WeakDelegate.Handle.ToString ("x"));
			} catch (Exception ex) {
				Console.WriteLine (ex);
				throw;
			}
			GC.Collect ();
		}

		private void OnDeviceDiscoveredService(object sender, NSErrorEventArgs nsErrorEventArgs)
		{
		}

		private PeripheralScanningOptions ScanningOptions
		{
			get
			{
				return new PeripheralScanningOptions
				{
					AllowDuplicatesKey = true
				};
			}
		}
	}
}


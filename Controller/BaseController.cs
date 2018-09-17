using System.Linq;
using HidLibrary;
using System.Threading;
using System;
using System.Diagnostics;

namespace ProxyGuitar {
    public abstract class BaseController {

        protected int VendorID, ProductID;
        protected HidDevice usbDevice;
        protected bool _attached;
        protected byte[] raw;

        public BaseController() {
        }

        public void Open() {
            raw = new byte[27];
            raw[3] = 0x80; //Left Stick X Static
            raw[4] = 0x80; //Left Stick Y Static
            raw[19] = 0X02;
            raw[21] = 0X02;
            raw[23] = 0X02;
            raw[25] = 0X02;

            usbDevice = HidDevices.Enumerate(VendorID, ProductID).FirstOrDefault();
            if (usbDevice == null)
                return;

            usbDevice.OpenDevice();

            usbDevice.Inserted += DeviceAttachedHandler;
            usbDevice.Removed += DeviceRemovedHandler;

            usbDevice.MonitorDeviceEvents = true;

            usbDevice.ReadReport(OnReport);
        }

        public bool CanUse() {
            return (usbDevice != null);
        }

        public virtual byte[] Read() {
            return raw;
        }

        public void Close() {
            if (usbDevice != null) {
                usbDevice.CloseDevice();
                usbDevice = null;
            }
        }

        public virtual string GetName() {
            return "Base";
        }

        //------

        protected virtual void OnReport(HidReport report) {
            if (_attached == false) { return; }

            if (report.Data.Length >= 4) {
                //Modify raw
            }

            usbDevice.ReadReport(OnReport);
        }

        private void DeviceAttachedHandler() {
            _attached = true;
            Console.WriteLine(GetName() + " attached.");
            usbDevice.ConnectionCheckOverride = usbDevice.IsConnected;
            usbDevice.ReadReport(OnReport);
        }

        private void DeviceRemovedHandler() {
            _attached = false;
            usbDevice.ConnectionCheckOverride = false;
            Console.WriteLine(GetName() + " removed.");
        }

    }
}

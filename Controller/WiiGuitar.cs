
using HidLibrary;
using System;

namespace ProxyGuitar {
    class WiiGuitar : BaseController {

        public WiiGuitar() : base() {
            VendorID = 0x1BAD;
            ProductID = 0x0004;
            Open();
        }

        protected override void OnReport(HidReport report) {
            if (_attached == false) { return; }

            if (report.Data.Length >= 4) {
                raw[0] = report.Data[0];
                raw[1] = report.Data[1];
                raw[2] = report.Data[2];
                raw[5] = report.Data[5];//Right Stick X Whammy
                raw[6] = report.Data[6];//Right Stick Y Effects Switch
            }

            usbDevice.ReadReport(OnReport);
        }

        public override string GetName() {
            return "Wii Guitar";
        }

    }
}

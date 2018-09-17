using System.Collections;
using System.Collections.Generic;

namespace ProxyGuitar {
    class XboxGHWT : BaseController {

        private static Dictionary<byte, byte> DPadTranslate = new Dictionary<byte, byte>() {
            {0x10, 0x08 }, {0x11, 0x00 }, {0x13, 0x02 }, {0x15, 0x04 }, {0x17, 0x06 }
        };

        public XboxGHWT() : base() {
            VendorID = 0x1439;
            ProductID = 0x0719;
            Open();
        }

        public override byte[] Read() {
            byte[] readBuffer = usbDevice.Read().Data;
            byte[] raw = new byte[27];

            BitArray buttonsBA = new BitArray(new bool[] {
                (readBuffer[6] & 0x04) == 0x04, //Blue
                (readBuffer[6] & 0x01) == 0x01, //Green
                (readBuffer[6] & 0x02) == 0x02, //Red
                (readBuffer[6] & 0x08) == 0x08, //Yellow
                (readBuffer[6] & 0x10) == 0x10, //Orange
                (readBuffer[1] == 0xFF), //Tilt,
                false //Solo, which is missing
            });
            buttonsBA.CopyTo(raw, 0);

            BitArray buttons2BA = new BitArray(new bool[] {
                (readBuffer[6] & 0x80) == 0x80, //Start
                (readBuffer[6] & 0x40) == 0x40  //Back/Starpower
            });
            buttons2BA.CopyTo(raw, 1);

            raw[2] = DPadTranslate[readBuffer[2]]; //DPad

            raw[5] = readBuffer[3]; //Whammy
            //if (data.raw[3] > 0xF0) data.raw[3] = 0xFF;

            if (readBuffer[1] == 0xFF)//Overdrive
                raw[0] = (byte)(raw[0] | 0x20);

            return raw;
        }

        public override string GetName() {
            return "Xbox 360 Guitar Hero World Tour Guitar";
        }

    }
}

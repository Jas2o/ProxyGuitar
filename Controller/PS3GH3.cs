
namespace ProxyGuitar {
    class PS3GH3 : BaseController {

        public PS3GH3() : base() {
            VendorID = 0x12BA;
            ProductID = 0x0100;
            Open();
        }

        public override byte[] Read() {
            byte[] readBuffer = usbDevice.Read().Data;

            byte[] raw = new byte[27];

            byte buttons = (byte)(readBuffer[0] & 0xF6);
            //Need to swap yellow and blue
            if ((byte)(readBuffer[0] & 0x08) == 0x08)
                buttons = (byte)(buttons | 0x01);
            if ((byte)(readBuffer[0] & 0x01) == 0x01)
                buttons = (byte)(buttons | 0x08);
            raw[0] = buttons;

            raw[1] = readBuffer[1]; //Buttons (Start/Select/PS)

            raw[2] = readBuffer[2]; //Dpad
            if (raw[2] == 0x0F)
                raw[2] = 0x08; //Idle

            raw[5] = readBuffer[5]; //Whammy
            if (raw[5] > 0xF0)
                raw[5] = 0xFF;

            if (readBuffer[20] == 0x01 && (readBuffer[19] > 0x70 && readBuffer[19] < 0x90))//Overdrive
                raw[0] = (byte)(raw[0] | 0x20);

            return raw;
        }

        public override string GetName() {
            return "PS3 GH3 Guitar";
        }

    }
}

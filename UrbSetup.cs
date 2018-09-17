using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyGuitar {
    public class UrbSetup {

        public byte bmRequestType, bRequest;
        public int wValue, wIndex, wLength;

        public byte wValue0, wValue1, wIndex0, wIndex1;

        public UrbSetup() {
            bmRequestType = 0;
            bRequest = 0;
            wValue = 0;
            wIndex = 0;
            wLength = 0;

            wValue0 = 0;
            wValue1 = 0;
            wIndex0 = 0;
            wIndex1 = 0;
        }

        public UrbSetup(byte[] input, int offset = 0) {
            bool flip = false;

            bmRequestType = input[offset];
            bRequest = input[offset + 1];
            wValue = (ushort) GTL.ReadInt(input, offset + 2, 2, flip);
            wIndex = (ushort) GTL.ReadInt(input, offset + 4, 2, flip);
            wLength = (ushort) GTL.ReadInt(input, offset + 6, 2, flip);

            wValue0 = input[offset + 2];
            wValue1 = input[offset + 3];
            wIndex0 = input[offset + 4];
            wIndex1 = input[offset + 5];
        }

        public override string ToString() {
            return String.Format("bmRequestType: {0:X}, bRequest: {1:X}, wValue: {2:X}, wIndex: {3:X}, wLength: {4:X}", bmRequestType, bRequest, wValue, wIndex, wLength);
        }

        public byte[] ToBytes() {
            byte[] bytes = new byte[8];
            bytes[0] = bmRequestType;
            bytes[1] = bRequest;

            byte[] _wValue = BitConverter.GetBytes((ushort)wValue);
            byte[] _wIndex = BitConverter.GetBytes((ushort)wIndex);
            byte[] _wLength = BitConverter.GetBytes((ushort)wLength);

            Array.Reverse(_wValue);
            Array.Reverse(_wIndex);
            Array.Reverse(_wLength);

            Array.Copy(_wValue, 0, bytes, 2, 2);
            Array.Copy(_wIndex, 0, bytes, 4, 2);
            Array.Copy(_wLength, 0, bytes, 6, 2);
            return bytes;
        }
    }
}

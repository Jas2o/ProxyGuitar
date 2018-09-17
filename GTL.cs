using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyGuitar {

    //GameTools Lite
    public static class GTL {

        public static byte[] SubArray(byte[] source, int bytesRec) {
            byte[] dest = new byte[bytesRec];
            Array.Copy(source, dest, bytesRec);
            return dest;
        }

        public static string ByteArrayToString(byte[] ba, string join = "") {
            //http://stackoverflow.com/questions/311165/how-do-you-convert-byte-array-to-hexadecimal-string-and-vice-versa
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", join);
        }

        public static int ReadInt(byte[] source, int sourceIndex, int length, bool flip) {
            byte[] dest = new byte[length];
            Array.Copy(source, sourceIndex, dest, 0, length);
            Array.Reverse(dest);

            if (length == 2)
                return BitConverter.ToUInt16(dest, 0);
            else if (length == 4)
                return BitConverter.ToInt32(dest, 0);
            else
                return -1;
        }

    }
}

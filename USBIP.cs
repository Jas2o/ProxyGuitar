using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyGuitar {
    public static class USBIP {

        private static bool sentHIDreport = false;
        private static UrbSetup lastURB = null;

        public static USBIP_RET_SUBMIT HandleRequest(ref USBIP_CMD_SUBMIT cmd) {
            USBIP_RET_SUBMIT submit = new USBIP_RET_SUBMIT(cmd);

            if(lastURB != null && lastURB.ToBytes().SequenceEqual(cmd.urb.ToBytes())) {
                //Send interrupt data
            } else {
                if (cmd.urb.bmRequestType == 0x80) { // Host Request
                    if (cmd.urb.bRequest == 0x06)
                        HandleGetDescriptor(ref cmd, ref submit);
                    else if (cmd.urb.bRequest == 0x00) { // Get STATUS

                    }
                } else if (cmd.urb.bmRequestType == 0x81) {
                    if (cmd.urb.wValue == 0x22) {
                        //if (!sentHIDreport) {
                            Console.WriteLine("sentHIDreport");
                            sentHIDreport = true;
                            //Get Descriptor Request HID Report
                            //bmRequestType: 81, bRequest: 6, wValue: 22, wIndex: 0, wLength: C900
                            if (cmd.urb.wLength == 0xC900) {
                                submit._data = new byte[] { 0x05, 0x01, 0x09, 0x05, 0xa1, 0x01, 0x15, 0x00, 0x25, 0x01, 0x35, 0x00, 0x45, 0x01, 0x75, 0x01, 0x95, 0x0d, 0x05, 0x09, 0x19, 0x01, 0x29, 0x0d, 0x81, 0x02, 0x95, 0x03, 0x81, 0x01, 0x05, 0x01, 0x25, 0x07, 0x46, 0x3b, 0x01, 0x75, 0x04, 0x95, 0x01, 0x65, 0x14, 0x09, 0x39, 0x81, 0x42, 0x65, 0x00, 0x95, 0x01, 0x81, 0x01, 0x26, 0xff, 0x00, 0x46, 0xff, 0x00, 0x09, 0x30, 0x09, 0x31, 0x09, 0x32, 0x09, 0x35, 0x75, 0x08, 0x95, 0x04, 0x81, 0x02, 0x06, 0x00, 0xff, 0x09, 0x20, 0x09, 0x21, 0x09, 0x22, 0x09, 0x23, 0x09, 0x24, 0x09, 0x25, 0x09, 0x26, 0x09, 0x27, 0x09, 0x28, 0x09, 0x29, 0x09, 0x2a, 0x09, 0x2b, 0x95, 0x0c, 0x81, 0x02, 0x0a, 0x21, 0x26, 0x95, 0x08, 0xb1, 0x02, 0x0a, 0x21, 0x26, 0x91, 0x02, 0x26, 0xff, 0x03, 0x46, 0xff, 0x03, 0x09, 0x2c, 0x09, 0x2d, 0x09, 0x2e, 0x09, 0x2f, 0x75, 0x10, 0x95, 0x04, 0x81, 0x02, 0xc0 };
                                submit.ReadyToSend = true;
                            }
                        //}
                    }
                } else if (cmd.urb.bmRequestType == 0xA1) {
                    submit.status = -75;
                    submit.ReadyToSend = true;
                } else if (cmd.urb.bmRequestType == 0x00) {
                    //Data?
                } else if (cmd.urb.bmRequestType == 0x01) {
                } else if (cmd.urb.bmRequestType == 0x21) {
                    //SET_IDLE Request
                    submit.ReadyToSend = true;
                }
            }
            lastURB = cmd.urb;

            if (!submit.ReadyToSend) {
                if (sentHIDreport) {
                    submit._data = Server.guitar.Read();

                    //Console.WriteLine(GTL.ByteArrayToString(submit._data, " "));

                    submit.ReadyToSend = true;
                } else {
                    Console.WriteLine("bmRequestType: {0:X2} | bRequest: {1:X2}", cmd.urb.bmRequestType, cmd.urb.bRequest);
                    Console.WriteLine();
                }
            }

            return submit;
        }

        private static void HandleGetDescriptor(ref USBIP_CMD_SUBMIT cmd, ref USBIP_RET_SUBMIT submit) {
            if (cmd.urb.wValue == 0x0001 && cmd.urb.wIndex == 0x0000 && cmd.urb.wLength == 0x1200) {
                //Device, length 18
                submit._data = new byte[] { 0x12, 0x01, 0x10, 0x01, 0x00, 0x00, 0x00, 0x08, 0xba, 0x12, 0x00, 0x02, 0x13, 0x03, 0x01, 0x02, 0x00, 0x01 };
                submit.ReadyToSend = true;
            } else if (cmd.urb.wValue == 0x0002 && cmd.urb.wIndex == 0x0000) {
                if (cmd.urb.wLength == 0x900) {
                    //Config, length 9
                    submit._data = new byte[] { 0x09, 0x02, 0x29, 0x00, 0x01, 0x01, 0x00, 0x80, 0x32 };
                    submit.ReadyToSend = true;
                } else if (cmd.urb.wLength == 0x2900) {
                    //Config, length 41
                    submit._data = new byte[] { 0x09, 0x02, 0x29, 0x00, 0x01, 0x01, 0x00, 0x80, 0x32, 0x09, 0x04, 0x00, 0x00, 0x02, 0x03, 0x00, 0x00, 0x00, 0x09, 0x21, 0x01, 0x01, 0x00, 0x01, 0x22, 0x89, 0x00, 0x07, 0x05, 0x81, 0x03, 0x40, 0x00, 0x0a, 0x07, 0x05, 0x02, 0x03, 0x40, 0x00, 0x01 }; //real
                    submit.ReadyToSend = true;
                }
            } else if (cmd.urb.wValue == 0x103 && cmd.urb.wIndex == 0x904) {
                //wLength: 204
                //First byte is length, second is 0x03

                byte[] bString = Encoding.Unicode.GetBytes("Licensed by Sony Computer Entertainment America");
                //byte[] bString = Encoding.Unicode.GetBytes("Sony");

                submit._data = new byte[bString.Length + 2];
                submit._data[0] = (byte)submit._data.Length;
                submit._data[1] = 0x03;
                Array.Copy(bString, 0, submit._data, 2, bString.Length);

                submit.ReadyToSend = true;
            } else if (cmd.urb.wValue == 0x203 && cmd.urb.wIndex == 0x904) {
                byte[] bString = Encoding.Unicode.GetBytes("Harmonix Guitar for PlayStation®3");
                //byte[] bString = Encoding.Unicode.GetBytes("Harmonix Guitar for PS3");

                submit._data = new byte[bString.Length + 2];
                submit._data[0] = (byte)submit._data.Length;
                submit._data[1] = 0x03;
                Array.Copy(bString, 0, submit._data, 2, bString.Length);

                submit.ReadyToSend = true;
            } else if (cmd.urb.wValue == 0x303 && cmd.urb.wIndex == 0x904) {
                byte[] bString = Encoding.Unicode.GetBytes("M:HEX3000-V3.13-27/03/2004");

                submit._data = new byte[bString.Length + 2];
                submit._data[0] = (byte)submit._data.Length;
                submit._data[1] = 0x03;
                Array.Copy(bString, 0, submit._data, 2, bString.Length);

                submit.ReadyToSend = true;
            } else if (cmd.urb.wValue == 0x403 && cmd.urb.wIndex == 0x904) {
                byte[] bString = Encoding.Unicode.GetBytes("WINBOX  XBOX&PCJOYPAD");

                submit._data = new byte[bString.Length + 2];
                submit._data[0] = (byte)submit._data.Length;
                submit._data[1] = 0x03;
                Array.Copy(bString, 0, submit._data, 2, bString.Length);

                submit.ReadyToSend = true;
            } else if (cmd.urb.wValue == 0x03 && (cmd.urb.wIndex == 0x904 || cmd.urb.wIndex == 0x0000)) {
                submit._data = new byte[] { 0x04, 0x03, 0x09, 0x04 };
                submit.ReadyToSend = true;
            } else {
                submit.status = -32;
                submit.ReadyToSend = true;
            }
        }
    }
}

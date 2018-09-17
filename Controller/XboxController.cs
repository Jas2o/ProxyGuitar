using System.Collections;
using System.Collections.Generic;

namespace ProxyGuitar {
    class XboxController : BaseController {

        //https://github.com/sharpdx/SharpDX-Samples/blob/master/Desktop/XInput/XGamepadApp/Program.cs

        private static Dictionary<byte, byte> DPadTranslate = new Dictionary<byte, byte>() {
            {0, 0x08 }, {1, 0x00 }, {2, 0x02 }, {3, 0x01 }, {4, 0x04 }, {6, 0x03 }, {8, 0x06 }, {9, 0x07 }, {0x0c, 0x05 }
        };
        //private Controller controller;

        public XboxController() : base() {
            VendorID = 0x045E;
            ProductID = 0x02A1;
            //inputSize = 14;
            //Open();

            /*
            var controllers = new[] { new Controller(UserIndex.One), new Controller(UserIndex.Two), new Controller(UserIndex.Three), new Controller(UserIndex.Four) };
            // Get 1st controller available
            foreach (var selectControler in controllers) {
                if (selectControler.IsConnected) {
                    controller = selectControler;
                    break;
                }
            }
            */
        }

        public override byte[] Read() {
            //This is incredibly laggy

            byte[] raw = new byte[27];

            /*
            Gamepad gamepad = controller.GetState().Gamepad;
            GamepadButtonFlags buttons = gamepad.Buttons;

            GuitarData data = new GuitarData();

            data.Blue = buttons.HasFlag(GamepadButtonFlags.X);
            data.Green = buttons.HasFlag(GamepadButtonFlags.A);
            data.Red = buttons.HasFlag(GamepadButtonFlags.B);
            data.Yellow = buttons.HasFlag(GamepadButtonFlags.Y);
            data.Orange = buttons.HasFlag(GamepadButtonFlags.LeftShoulder);
            data.Tilt = buttons.HasFlag(GamepadButtonFlags.RightShoulder);
            data.SelectMinus = buttons.HasFlag(GamepadButtonFlags.Back);
            data.StartPlus = buttons.HasFlag(GamepadButtonFlags.Start);

            if(gamepad.RightTrigger == 0x00)
                data.Whammy = 0x7F;
            else
                data.Whammy = gamepad.RightTrigger;

            if (gamepad.LeftTrigger > 100)
                data.Solo = true;

            BitArray dpadBA = new BitArray(new bool[] {
                buttons.HasFlag(GamepadButtonFlags.DPadUp),
                buttons.HasFlag(GamepadButtonFlags.DPadRight),
                buttons.HasFlag(GamepadButtonFlags.DPadDown),
                buttons.HasFlag(GamepadButtonFlags.DPadLeft)
            });
            byte[] dpadA = new byte[1];
            dpadBA.CopyTo(dpadA, 0);
            data.Dpad = DPadTranslate[dpadA[0]];

            data.UpdateRaw();
            */

            return raw;
        }

        public override string GetName() {
            return "Xbox 360 Controller";
        }

    }
}

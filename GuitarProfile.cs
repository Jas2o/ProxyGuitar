using System.Collections;
using System.Windows.Forms;

namespace ProxyGuitar {
    public class GuitarProfile {

        public ProfileButtonTrigger Green, Red, Yellow, Blue, Orange, Tilt, Solo;
        public ProfileButtonTrigger Home, SelectMinus, StartPlus;

        public ProfileButtonTrigger DpadIdle, DpadUp, DpadRight, DpadDown, DpadLeft;
        public ProfileWhammyTrigger Whammy;

        public GuitarProfile() {
        }
    }

    public class ProfileButtonTrigger {
        public byte byteIndex, byteValue;

        public ProfileButtonTrigger() {
            byteIndex = 0;
            byteValue = 0;
        }

        public ProfileButtonTrigger(TextBox txtSelectedByte, TextBox txtSelectedByteValue) {
            byteIndex = byte.Parse(txtSelectedByte.Text);
            byteValue = byte.Parse(txtSelectedByteValue.Text);
        }
    }

    /*public class ProfileByteTrigger {
        public byte byteIndex, byteValue;

        public ProfileByteTrigger() {
            byteIndex = 0;
            byteValue = 0;
        }

        public ProfileByteTrigger(TextBox txtSelectedByte, TextBox txtSelectedByteValue) {
            byteIndex = byte.Parse(txtSelectedByte.Text);
            byteValue = byte.Parse(txtSelectedByteValue.Text);
        }
    }*/

    public class ProfileWhammyTrigger {
        public byte byteIndex, byteValueIdle, byteValueUp, byteValueDown;

        public ProfileWhammyTrigger() {
            byteIndex = 0;
            byteValueIdle = 0x7F;
            byteValueUp = 0x00;
            byteValueDown = 0xFF;
        }

        public ProfileWhammyTrigger(TextBox txtSelectedByte) {
            byteIndex = byte.Parse(txtSelectedByte.Text);
            byteValueIdle = 0x7F;
            byteValueUp = 0x00;
            byteValueDown = 0xFF;
        }
    }
}

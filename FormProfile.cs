using HidLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxyGuitar {
    public partial class FormProfile : Form {

        private HidDevice usbDevice;
        private bool _attached;

        public FormProfile(HidDevice hid) {
            usbDevice = hid;
            InitializeComponent();
        }

        private GuitarProfile profile = new GuitarProfile();

        private byte[] reportResting;
        private byte[] lastReportData;
        private List<Button> listButtonReportByte;
        private int activeReportByte;

        private void FormProfile_Load(object sender, EventArgs e) {
            if (usbDevice == null)
                return;
            usbDevice.OpenDevice();
            reportResting = usbDevice.Read().Data;

            lastReportData = new byte[reportResting.Length - 1];
            Array.Copy(reportResting, 1, lastReportData, 0, lastReportData.Length);

            activeReportByte = 0;
            txtSelectedByte.Text = activeReportByte.ToString();

            listButtonReportByte = new List<Button>();
            for (int i = 1; i < reportResting.Length; i++) {
                Button button = new Button();
                button.Text = reportResting[i].ToString("X2");
                button.Width = 40;
                button.Click += ReportByteClick;
                flowLayoutPanel1.Controls.Add(button);
                listButtonReportByte.Add(button);
            }
            listButtonReportByte[0].PerformClick();

            usbDevice.Inserted += DeviceAttachedHandler;
            usbDevice.Removed += DeviceRemovedHandler;
            usbDevice.MonitorDeviceEvents = true;
            usbDevice.ReadReport(OnReport);
        }

        private void FormProfile_FormClosing(object sender, FormClosingEventArgs e) {
            _attached = false;
            usbDevice.Inserted -= DeviceAttachedHandler;
            usbDevice.Removed -= DeviceRemovedHandler;
            usbDevice.MonitorDeviceEvents = false;
            usbDevice.CloseDevice();
        }

        private void ReportByteClick(object sender, EventArgs e) {
            for(int i = 0; i < listButtonReportByte.Count; i++) {
                Button btn = (Button)sender;
                if (listButtonReportByte[i] == btn) {
                    activeReportByte = i;
                    SetBits(lastReportData[i]);
                    txtSelectedByte.Text = activeReportByte.ToString();
                    txtSelectedByteValue.Text = int.Parse(btn.Text, System.Globalization.NumberStyles.HexNumber).ToString();
                    break;
                }
            }
        }

        private void OnReport(HidReport report) {
            if (_attached == false) { return; }

            if (report.Data.Length >= 4) {
                if (lastReportData == null)
                    lastReportData = report.Data;

                for (int i = 0; i < report.Data.Length; i++) {
                    Button button = listButtonReportByte[i];

                    if (report.Data[i] != lastReportData[i]) {
                        button.Invoke(new MethodInvoker(() => {
                            button.Text = report.Data[i].ToString("X2");
                            if (report.Data[i] == reportResting[i + 1])
                                button.BackColor = Color.LightGray;
                            else
                                button.BackColor = Color.Orange;

                            //--

                            if (activeReportByte == i) {
                                int value = report.Data[i];
                                btnBit0.Invoke(new MethodInvoker(() => {
                                    SetBits(value);
                                }));
                            }

                            btnProfileGreen.BackColor = (TestProfile(profile.Green, report.Data) ? Color.GreenYellow : Color.LightGray);
                            btnProfileRed.BackColor = (TestProfile(profile.Red, report.Data) ? Color.Red : Color.LightGray);
                            btnProfileYellow.BackColor = (TestProfile(profile.Yellow, report.Data) ? Color.Yellow : Color.LightGray);
                            btnProfileBlue.BackColor = (TestProfile(profile.Blue, report.Data) ? Color.Blue : Color.LightGray);
                            btnProfileOrange.BackColor = (TestProfile(profile.Orange, report.Data) ? Color.Orange : Color.LightGray);

                            btnProfileSolo.BackColor = (TestProfile(profile.Solo, report.Data) ? Color.HotPink : Color.LightGray);
                            btnProfileTilt.BackColor = (TestProfile(profile.Tilt, report.Data) ? Color.HotPink : Color.LightGray);
                            btnProfileStart.BackColor = (TestProfile(profile.StartPlus, report.Data) ? Color.HotPink : Color.LightGray);
                            btnProfileSelect.BackColor = (TestProfile(profile.SelectMinus, report.Data) ? Color.HotPink : Color.LightGray);

                            if (profile.Whammy != null) {
                                lblWhammy.Text = report.Data[profile.Whammy.byteIndex].ToString();
                            }

                            btnDPadIdle.BackColor = (TestProfile(profile.DpadIdle, report.Data, true) ? Color.HotPink : Color.LightGray);
                            btnDPadUp.BackColor = (TestProfile(profile.DpadUp, report.Data, true) ? Color.HotPink : Color.LightGray);
                            btnDPadDown.BackColor = (TestProfile(profile.DpadDown, report.Data, true) ? Color.HotPink : Color.LightGray);
                        }));
                    }
                }

                lastReportData = report.Data;
            }

            usbDevice.ReadReport(OnReport);
        }

        private bool TestProfile(ProfileButtonTrigger profile, byte[] data, bool exact = false) {
            if (profile != null && ((exact && data[profile.byteIndex] == profile.byteValue) || (!exact && (data[profile.byteIndex] & profile.byteValue) == profile.byteValue)))
                return true;
            return false;
        }

        private void SetBits(int value) {
            btnBit0.Text = (value & 0x80).ToString();
            btnBit1.Text = (value & 0x40).ToString();
            btnBit2.Text = (value & 0x20).ToString();
            btnBit3.Text = (value & 0x10).ToString();
            btnBit4.Text = (value & 0x8).ToString();
            btnBit5.Text = (value & 0x4).ToString();
            btnBit6.Text = (value & 0x2).ToString();
            btnBit7.Text = (value & 0x1).ToString();

            if (btnBit0.Text == "0") btnBit0.BackColor = Color.LightGray; else btnBit0.BackColor = Color.Orange;
            if (btnBit1.Text == "0") btnBit1.BackColor = Color.LightGray; else btnBit1.BackColor = Color.Orange;
            if (btnBit2.Text == "0") btnBit2.BackColor = Color.LightGray; else btnBit2.BackColor = Color.Orange;
            if (btnBit3.Text == "0") btnBit3.BackColor = Color.LightGray; else btnBit3.BackColor = Color.Orange;
            if (btnBit4.Text == "0") btnBit4.BackColor = Color.LightGray; else btnBit4.BackColor = Color.Orange;
            if (btnBit5.Text == "0") btnBit5.BackColor = Color.LightGray; else btnBit5.BackColor = Color.Orange;
            if (btnBit6.Text == "0") btnBit6.BackColor = Color.LightGray; else btnBit6.BackColor = Color.Orange;
            if (btnBit7.Text == "0") btnBit7.BackColor = Color.LightGray; else btnBit7.BackColor = Color.Orange;
        }

        private void DeviceAttachedHandler() {
            _attached = true;
            Debug.WriteLine("HID attached.");
            usbDevice.ConnectionCheckOverride = usbDevice.IsConnected;
            usbDevice.ReadReport(OnReport);
        }

        private void DeviceRemovedHandler() {
            _attached = false;
            usbDevice.ConnectionCheckOverride = false;
            Debug.WriteLine("HID removed.");
        }

        #region Buttons: Bits
        private void SetTextByteValue(string value) {
            txtSelectedByteValue.Text = value;
        }

        private void btnBit0_Click(object sender, EventArgs e) {
            SetTextByteValue(btnBit0.Text);
        }

        private void btnBit1_Click(object sender, EventArgs e) {
            SetTextByteValue(btnBit1.Text);
        }

        private void btnBit2_Click(object sender, EventArgs e) {
            SetTextByteValue(btnBit2.Text);
        }

        private void btnBit3_Click(object sender, EventArgs e) {
            SetTextByteValue(btnBit3.Text);
        }

        private void btnBit4_Click(object sender, EventArgs e) {
            SetTextByteValue(btnBit4.Text);
        }

        private void btnBit5_Click(object sender, EventArgs e) {
            SetTextByteValue(btnBit5.Text);
        }

        private void btnBit6_Click(object sender, EventArgs e) {
            SetTextByteValue(btnBit6.Text);
        }

        private void btnBit7_Click(object sender, EventArgs e) {
            SetTextByteValue(btnBit7.Text);
        }
        #endregion

        private void btnProfileGreen_Click(object sender, EventArgs e) {
            if(txtSelectedByte.Text.Length > 0 && txtSelectedByteValue.Text.Length > 0)
                profile.Green = new ProfileButtonTrigger(txtSelectedByte, txtSelectedByteValue);
        }

        private void btnProfileRed_Click(object sender, EventArgs e) {
            if (txtSelectedByte.Text.Length > 0 && txtSelectedByteValue.Text.Length > 0)
                profile.Red = new ProfileButtonTrigger(txtSelectedByte, txtSelectedByteValue);
        }

        private void btnProfileYellow_Click(object sender, EventArgs e) {
            if (txtSelectedByte.Text.Length > 0 && txtSelectedByteValue.Text.Length > 0)
                profile.Yellow = new ProfileButtonTrigger(txtSelectedByte, txtSelectedByteValue);
        }

        private void btnProfileBlue_Click(object sender, EventArgs e) {
            if (txtSelectedByte.Text.Length > 0 && txtSelectedByteValue.Text.Length > 0)
                profile.Blue = new ProfileButtonTrigger(txtSelectedByte, txtSelectedByteValue);
        }

        private void btnProfileOrange_Click(object sender, EventArgs e) {
            if (txtSelectedByte.Text.Length > 0 && txtSelectedByteValue.Text.Length > 0)
                profile.Orange = new ProfileButtonTrigger(txtSelectedByte, txtSelectedByteValue);
        }

        private void btnProfileStart_Click(object sender, EventArgs e) {
            if (txtSelectedByte.Text.Length > 0 && txtSelectedByteValue.Text.Length > 0)
                profile.StartPlus = new ProfileButtonTrigger(txtSelectedByte, txtSelectedByteValue);
        }

        private void btnProfileSelect_Click(object sender, EventArgs e) {

            if (txtSelectedByte.Text.Length > 0 && txtSelectedByteValue.Text.Length > 0) profile.SelectMinus = new ProfileButtonTrigger(txtSelectedByte, txtSelectedByteValue);
        }

        private void btnProfileSolo_Click(object sender, EventArgs e) {
            if (txtSelectedByte.Text.Length > 0 && txtSelectedByteValue.Text.Length > 0)
                profile.Solo = new ProfileButtonTrigger(txtSelectedByte, txtSelectedByteValue);
        }

        private void btnProfileTilt_Click(object sender, EventArgs e) {
            if (txtSelectedByte.Text.Length > 0 && txtSelectedByteValue.Text.Length > 0)
                profile.Tilt = new ProfileButtonTrigger(txtSelectedByte, txtSelectedByteValue);
        }

        private void btnProfileWhammy_Click(object sender, EventArgs e) {
            if (txtSelectedByte.Text.Length > 0)
                profile.Whammy = new ProfileWhammyTrigger(txtSelectedByte);
        }

        private void btnProfileSave_Click(object sender, EventArgs e) {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(GuitarProfile));
            string path = "TestProxyProfile.xml";
            System.IO.FileStream file = System.IO.File.Create(path);
            writer.Serialize(file, profile);
            file.Close();
        }

        private void btnProfileLoad_Click(object sender, EventArgs e) {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(GuitarProfile));
            System.IO.StreamReader file = new System.IO.StreamReader("TestProxyProfile.xml");
            profile = (GuitarProfile)reader.Deserialize(file);
            file.Close();
        }

        private void btnDPadIdle_Click(object sender, EventArgs e) {
            if (txtSelectedByte.Text.Length > 0 && txtSelectedByteValue.Text.Length > 0)
                profile.DpadIdle = new ProfileButtonTrigger(txtSelectedByte, txtSelectedByteValue);
        }

        private void btnDPadUp_Click(object sender, EventArgs e) {
            if (txtSelectedByte.Text.Length > 0 && txtSelectedByteValue.Text.Length > 0)
                profile.DpadUp = new ProfileButtonTrigger(txtSelectedByte, txtSelectedByteValue);
        }

        private void btnDPadDown_Click(object sender, EventArgs e) {
            if (txtSelectedByte.Text.Length > 0 && txtSelectedByteValue.Text.Length > 0)
                profile.DpadDown = new ProfileButtonTrigger(txtSelectedByte, txtSelectedByteValue);
        }
    }
}

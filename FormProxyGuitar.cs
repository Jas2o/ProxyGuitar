using HidLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxyGuitar {
    public partial class FormProxyGuitar : Form {

        List<HidDevice> listHID;
        string[] productNameExclude = new string[] { "Keyboard", "Mouse" };
        int[] vendorIDExclude = new int[] {
            0x28DE, 0x0BB4, // HTC Vive
            0xBEEF // 3DConnexion Emulator
        };

        Thread thread = null;

        public FormProxyGuitar() {
            InitializeComponent();
        }

        private void FormProxyGuitar_Load(object sender, EventArgs e) {
            Console.SetOut(new TextBoxWriter(txtLog));

            listHID = new List<HidDevice>();
            List<HidDevice> listAllHID = HidDevices.Enumerate().ToList();
            foreach (HidDevice hid in listAllHID) {
                byte[] bProduct;
                hid.ReadProduct(out bProduct);
                string sProduct = Encoding.Unicode.GetString(bProduct).TrimEnd('\0');

                if (sProduct.Length > 0 && !productNameExclude.Any(word => sProduct.Contains(word)) && !vendorIDExclude.Any(id => id == hid.Attributes.VendorId)) {
                    listHID.Add(hid);
                    cmbUSBDevices.Items.Add(string.Format("{2} (VID:{0:X4} PID:{1:X4})", hid.Attributes.VendorId, hid.Attributes.ProductId, sProduct));
                }
            }

            if (cmbUSBDevices.Items.Count > 0)
                cmbUSBDevices.SelectedIndex = 0;
        }

        private void FormProxyGuitar_FormClosing(object sender, FormClosingEventArgs e) {
            ServerShutdown();
        }

        private void ServerShutdown() {
            Server.Shutdown();
            if(thread != null)
                thread.Abort();

            cmbUSBDevices.Enabled = true;
            btnServer.Text = "Doing Nothing";
        }

        private void btnServer_Click(object sender, EventArgs e) {
            if (thread == null || !thread.IsAlive) {
                thread = new Thread(() => Server.StartListening());
                thread.Start();
                btnUsbipRun_Click(sender, e);

                cmbUSBDevices.Enabled = false;
                btnServer.Text = "Running";
            } else {
                ServerShutdown();
            }
        }

        private void btnProfile_Click(object sender, EventArgs e) {
            if(cmbUSBDevices.SelectedIndex >= 0)
                new FormProfile(listHID[cmbUSBDevices.SelectedIndex]).Show();
        }

        #region External Programs
        private void btnUsbipForceClose_Click(object sender, EventArgs e) {
            if (Process.GetProcessesByName("usbip").Length > 0) {
                string pathrunning = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string pathusbip = Path.Combine(pathrunning, "USBIP");
                string pathusbipexe = Path.Combine(pathusbip, "usbip.exe");

                if (File.Exists(pathusbipexe)) {
                    ProcessStartInfo psi = new ProcessStartInfo(pathusbipexe);
                    psi.WorkingDirectory = pathusbip;
                    if (!chkUsbipShow.Checked)
                        psi.WindowStyle = ProcessWindowStyle.Hidden;

                    psi.Arguments = "-d 1";
                    Process.Start(psi);
                } else {
                    Console.WriteLine("Could not find: " + pathusbipexe);
                }
            }
        }

        private void btnUsbipRun_Click(object sender, EventArgs e) {
            if (Process.GetProcessesByName("usbip").Length == 0) {
                string pathrunning = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string pathusbip = Path.Combine(pathrunning, "USBIP");
                string pathusbipexe = Path.Combine(pathusbip, "usbip.exe");

                if (File.Exists(pathusbipexe)) {
                    ProcessStartInfo psi = new ProcessStartInfo(pathusbipexe, "-a localhost 1-3");
                    psi.WorkingDirectory = pathusbip;
                    if (!chkUsbipShow.Checked)
                        psi.WindowStyle = ProcessWindowStyle.Hidden;

                    Process.Start(psi);
                } else {
                    Console.WriteLine("Could not find: " + pathusbipexe);
                }
            }
        }

        private void DevconDeviceEnable(bool enable) {
            string pathrunning = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string pathusbip = Path.Combine(pathrunning, "USBIP");
            string pathdevcon = Path.Combine(pathusbip, "devcon.exe");

            if (File.Exists(pathdevcon)) {
                ProcessStartInfo psi = new ProcessStartInfo(pathdevcon, (enable ? "enable" : "disable") + " USB\\VID_12BA*");
                //psi.WorkingDirectory = pathusbip;
                //if (!chkUsbipShow.Checked)
                //psi.WindowStyle = ProcessWindowStyle.Hidden;

                Process.Start(psi);
            } else {
                Console.WriteLine("Could not find: " + pathdevcon);
            }
        }
        #endregion
    }
}

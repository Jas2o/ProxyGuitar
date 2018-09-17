using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ProxyGuitar {
    public class TextBoxWriter : TextWriter {
        TextBox _output = null;

        public TextBoxWriter(TextBox output) {
            _output = output;
        }

        public override void Write(char value) {
            base.Write(value);
            _output.Invoke(new MethodInvoker(() => _output.AppendText(value.ToString())));
        }

        public override Encoding Encoding {
            get { return Encoding.UTF8; }
        }
    }
}

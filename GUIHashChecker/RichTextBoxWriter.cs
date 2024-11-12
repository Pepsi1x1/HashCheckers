using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace HashCheckers
{
    public class RichTextBoxWriter : TextWriter
    {
        private readonly Encoding _encoding;
        private readonly RichTextBox _textBox;

        public RichTextBoxWriter(RichTextBox textBox)
            : this(textBox, Encoding.UTF8)
        {
        }

        public RichTextBoxWriter(RichTextBox textBox, Encoding encoding)
        {
            _textBox = textBox;
            _encoding = encoding;
        }

        public override void WriteLine(string value)
        {
            _textBox.AppendText(value + Environment.NewLine);
        }

        public override void Write(char value)
        {
            _textBox.AppendText(value.ToString());
        }

        public override void Write(string value)
        {
            _textBox.AppendText(value);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            _textBox.AppendText(new string(buffer));
        }

        public override Encoding Encoding
        {
            get
            {
                return _encoding;
            }
        }
    }
}

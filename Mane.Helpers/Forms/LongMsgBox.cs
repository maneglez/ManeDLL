using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mane.Helpers.Forms
{
    public partial class LongMsgBox : Form
    {
        public LongMsgBox(string text,string caption)
        {
            InitializeComponent();
            richTextBox1.Text = text;
            Text = caption;
            StartPosition = FormStartPosition.CenterScreen;
        }
    }
}

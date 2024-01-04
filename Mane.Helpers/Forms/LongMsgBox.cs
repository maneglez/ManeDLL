using System.Windows.Forms;

namespace Mane.Helpers.Forms
{
    public partial class LongMsgBox : Form
    {
        public LongMsgBox(string text, string caption)
        {
            InitializeComponent();
            richTextBox1.Text = text;
            Text = caption;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {

        }
    }
}

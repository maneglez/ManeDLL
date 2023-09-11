using System;
using System.Windows.Forms;

namespace Mane.Licenciamiento
{
    public partial class InfoDeLicencia : Form
    {

        public InfoDeLicencia()
        {
            InitializeComponent();

        }

        private void InfoDeLicencia_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Licencia.Check();
            tbAppId.Text = ManeApp.AppId;
            tbHk.Text = Licencia.GetHardwareKey();
            tbExpiracion.Text = Licencia.Expiracion.ToString("d");
            Cursor = Cursors.Default;
        }
        private void ImportarLicencia()
        {
            if (Licencia.Import())
            {
                Licencia.ValidarLicencia(true);
                Cursor = Cursors.WaitCursor;
                tbAppId.Text = ManeApp.AppId;
                tbHk.Text = Licencia.GetHardwareKey();
                tbExpiracion.Text = Licencia.Expiracion.ToString("d");
                Cursor = Cursors.Default;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbHk.Text);
            MessageBox.Show("Copiado!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ImportarLicencia();
        }
    }
}

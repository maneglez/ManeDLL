using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Mane.Sap.Forms
{
    public partial class ConectionTester : Form, IConexionSap
    {
        public static void ShowTester()
        {
            new ConectionTester().Show();
        }
        public ConectionTester()
        {
            InitializeComponent();
            Mane.Sap.Helpers.Utils.EnumToComboBox<TipoServidorSap>(cbTipoServidor);
        }
        public void Test()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
               // MessageBox.Show(Sap.TestConnection(this) ? "Conectado Existosamente" : Sap.LastError);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            Cursor = Cursors.Default;
        }

        public string DbCompany { get => tbSapBd.Text; set => tbSapBd.Text = value; }
        public string DbPassword { get => tbPassBd.Text; set => tbPassBd.Text = value; }
        public string DbUser { get => tbUsuarioBd.Text; set => tbUsuarioBd.Text = value; }
        public string LicenseServer { get => tbLicenseServer.Text; set => tbLicenseServer.Text = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        private string _nombre;
        public string Password { get => tbSapPassword.Text; set => tbSapPassword.Text = value; }
        public string Server { get => tbServidor.Text; set => tbServidor.Text = value; }
        public string SLDServer { get => tbSldServer.Text; set => tbSldServer.Text = value; }
        public string User { get => tbSapUser.Text; set => tbSapUser.Text = value; }
        public TipoServidorSap TipoServidor { get => (TipoServidorSap)cbTipoServidor.SelectedValue; set => cbTipoServidor.SelectedValue = value; }
        public List<SapUser> AlternativeUsers { get; set; }

        private void ConectionTester_Load(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Test();
        }
    }
}

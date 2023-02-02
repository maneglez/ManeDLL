using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mane.Licenciamiento.Generador
{
    public partial class frmGenerador : Form
    {
        public string AppId { get => tbAppId.Text; set => tbAppId.Text = value; }
        public string ClaveDeApp { get => tbAppCve.Text; set => tbAppCve.Text = value; }
        public string HardwareKey { get => tbHk.Text; set => tbHk.Text = value; }
        public DateTime Expiracion { get => dtpExp.Value; set => dtpExp.Value = value; }
        public frmGenerador()
        {
            InitializeComponent();
        }

        private void GenerarLic()
        {
            var ruta = "";
            using(var fm = new FolderBrowserDialog())
            {
                fm.Description = "Seleccione en dónde se guardará la licencia";
                if (fm.ShowDialog() == DialogResult.OK)
                    ruta = fm.SelectedPath;
            }
            if (string.IsNullOrEmpty(ruta))
                return;
            ruta = Path.Combine(ruta, $"Licencia_{HardwareKey}.xml");
            var lic = Generador.GenerarLicencia(HardwareKey, AppId, Expiracion, ClaveDeApp);
            Licencia.SaveAs(lic, ruta);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GenerarLic();
        }
    }
}

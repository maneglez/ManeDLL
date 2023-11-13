using Mane.Sap.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mane.Sap.Forms
{
    public partial class ConnectionManager : Form
    {
        BindingList<ConexionSap> Conexiones;
        string rutaConexiones;
        public ConnectionManager(string rutaConexiones = "")
        {
            this.rutaConexiones = rutaConexiones;
            InitializeComponent();
            var colTipo = dataGridView1.Columns[colTipoServidor.Name] as DataGridViewComboBoxColumn;
            Utils.EnumToComboBox(colTipo, typeof(TipoServidorSap));
            var cons = Sap.InstanciarConexiones(rutaConexiones);
            Conexiones = new BindingList<ConexionSap>(cons.ToList());
            dataGridView1.DataSource = Conexiones;
        }

        private void probarConexionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            var con = dataGridView1.SelectedRows[0].DataBoundItem as ConexionSap;
            Cursor = Cursors.WaitCursor;
            MessageBox.Show(Sap.TestConnection(con) ? "Conectado correctamente" : "Error " + Sap.LastError);
            Cursor = Cursors.Default;
        }
        public void Guardar()
        {
            Sap.GuardarConexiones(Conexiones, rutaConexiones);
            Sap.Conexiones.Clear();
            Sap.Conexiones.AddRange(Conexiones);
            MessageBox.Show("Informacion guardada");
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }
    }
}

using Mane.BD.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mane.BD.Forms
{
    public partial class GestionarConexiones : Form
    {
        BindingList<Conexion> Conexiones;
        string FileName;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">Ruta del archivo de configuracion (opcional)</param>
        public GestionarConexiones(string fileName = "")
        {
            FileName = fileName;
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            colTipoBd.DataSource = Utils.EnumToList(typeof(TipoDeBd));
            colTipoBd.DisplayMember = "Name";
            colTipoBd.ValueMember = "Value";

            colSubtipoBd.DataSource = Utils.EnumToList(typeof(TipoDeBd));
            colSubtipoBd.DisplayMember = "Name";
            colSubtipoBd.ValueMember = "Value";

            Conexiones = new BindingList<Conexion>();
            var cons = string.IsNullOrEmpty(FileName) ? Bd.LoadConnectionsFromFile() :
                Bd.LoadConnectionsFromFile(FileName);
            if(cons != null)
                foreach (var c in cons)
                    Conexiones.Add(c);
            dataGridView1.DataSource =  Conexiones;

        }

        private void GestionarConexiones_Load(object sender, EventArgs e)
        {

        }

        private void Guardar()
        {
            var cons = new Bd.ConexionCollection();
            try
            {
                foreach (var c in Conexiones)
                {
                    if (!string.IsNullOrEmpty(c.Nombre))
                        cons.Add(c);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            if (string.IsNullOrEmpty(FileName))
                Bd.SaveConnectionsToFile(cons);
            else Bd.SaveConnectionsToFile(cons, FileName);
            MessageBox.Show("Configuracion guardada correctamente");
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Guardar();
        }
    }
}

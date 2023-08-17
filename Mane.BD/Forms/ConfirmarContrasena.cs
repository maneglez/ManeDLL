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
    public partial class ConfirmarContrasena : Form
    {
        private static int Intentos = 0;
        private static int MaximoIntentos = 5;
        private string[] Contrasenas = new string[]
        {
            "maneg13",
            "tie.2023",
            "saoilkew",
            "saoilkew12",
            "mane1234",
            "sapo12"
        };
        private string password { get => textBox1.Text; set => textBox1.Text = value; }
        private string rutaArchivoConfig;
        public ConfirmarContrasena(string RutaArchivoConfig = "")
        {
            InitializeComponent();
            rutaArchivoConfig = RutaArchivoConfig;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                ValidarPass();
            }
        }

        private void ValidarPass()
        {
            if (Intentos >= MaximoIntentos)
            {
                MessageBox.Show("Se excedió el numero máximo de intentos");
                Close();
                Dispose();
                return;
            }
            if (!Contrasenas.Contains(password))
            {
                Intentos++;
                MessageBox.Show("Contraseña incorrecta");
                return;
            }
            Intentos = 0;
            var fm = new GestionarConexiones(rutaArchivoConfig);
            fm.Show();
            Close();
            Dispose();
        }
    }
}

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
    public partial class EncryptTool : Form
    {
        public string Contrasena { get => tbContrasena.Text;  }
        public string Texto { get => tbTexto.Text;  }
        public string Resultado { get => tbResultado.Text; set => tbResultado.Text = value; }
        public EncryptTool()
        {
            InitializeComponent();
        }

        private void Encriptar()
        {
            try
            {
                if (string.IsNullOrEmpty(Contrasena) || string.IsNullOrEmpty(Texto))
                    throw new Exception("Especifique una contrasena y una cadena de texto para encriptar");
               Resultado = Crypto.Encriptar(Texto, Contrasena);
            }
            catch (Exception e)
            {
                MsgBox.error(e.Message);
            }
        }

        private void Decriptar()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Contrasena) || string.IsNullOrWhiteSpace(Texto))
                    throw new Exception("Especifique una contrasena y una cadena de texto para dencriptar");
                Resultado = Crypto.Decriptar(Texto, Contrasena);
            }
            catch (Exception e)
            {
                MsgBox.error(e.Message);
            }
        }
        private void CopiarResultado()
        {
            if (!string.IsNullOrWhiteSpace(Resultado))
            {
                Clipboard.SetText(Resultado);
                MsgBox.info("Copiado!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Encriptar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Decriptar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CopiarResultado();
        }
    }
}

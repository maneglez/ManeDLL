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
    public partial class CapturarDato : Form
    {
        public string Titulo { get => Text; set => Text = value; }
        public string Dato { get => tbDato.Text; set => tbDato.Text = value; }
        public string TextoPeticion { get => lbTextoPeticion.Text; set => lbTextoPeticion.Text = value; }
        public CapturarDato()
        {
            InitializeComponent();
        }

        public CapturarDato( string textoPeticion, string titulo = null,string dato = null)
        {
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(titulo))
                Titulo = titulo;
            if(!string.IsNullOrWhiteSpace(dato))
                Dato = dato;
            TextoPeticion = textoPeticion;
        }

        private void tbDato_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Mane.Helpers.Forms
{
    public partial class EstablecerValoresADataGridView : Form
    {
        private IEnumerable<DataGridViewRow> Rows;
        public EstablecerValoresADataGridView(IEnumerable<DataGridViewRow> rows)
        {
            InitializeComponent();
            Rows = rows;
            if (rows.Count() >= 1)
            {
                var r = rows.ElementAt(0);
                foreach (DataGridViewCell c in r.Cells)
                {
                    if (!c.ReadOnly && c.OwningColumn.Visible)
                        cbColumnas.Items.Add(new { Name = c.OwningColumn.HeaderText, Value = c.OwningColumn.Name });
                }
                cbColumnas.DisplayMember = "Name";
                cbColumnas.ValueMember = "Value";
                cbColumnas.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbColumnas.SelectedItem != null && Rows != null)
            {
                var col = (string)(cbColumnas.SelectedItem as dynamic).Value;
                foreach (var r in Rows)
                {
                    try
                    {
                        r.Cells[col].Value = textBox1.Text;
                    }
                    catch (Exception)
                    {

                    }
                }
                MessageBox.Show("Valores asignados correctamente");
            }
        }
    }
}

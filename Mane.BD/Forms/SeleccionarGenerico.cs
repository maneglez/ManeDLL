using Mane.BD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mane.BD.Forms
{
    public partial class SeleccionarGenerico : Form
    {
        private QueryBuilder query { get; set; }
        public event EventHandler<ItemCambiaEventArgs> ItemSeleccionadoCambia;
        /// <summary>
        /// Texto a buscar
        /// </summary>
        public string Busqueda { get => tbBusqueda.Text; set => tbBusqueda.Text = value; }
        /// <summary>
        /// Indica si se habilitan los controles para filtrar. Por defecto activado
        /// </summary>
        public bool HabilitarBusqueda
        {
            get => habilitarBusqueda; set
            {
                cbFiltro.Visible =
                tbBusqueda.Visible =
                habilitarBusqueda = value;
            }
        }
        public bool CerrarAlSeleccionar { get; set; }
        public bool MostrarBotonesDeSeleccionYCancelar
        {
            set
            {
                btnCancelar.Visible = value;
                btnSeleccionar.Visible = value;
            }
        }

        private string ConnName;
        private int cBusqueda;
        private bool habilitarBusqueda;

        public DataRow SelectedRow { get; private set; }

        public DataRow GetSelectedRow()
        {
            if (dgvContenido.SelectedRows.Count == 0) return null;
            var dtSource = (dgvContenido.DataSource as DataTable);
            return dtSource.Rows[dgvContenido.SelectedRows[0].Index];

        }
        
        /// <summary>
        /// Seleccion de elementos generico
        /// </summary>
        /// <param name="query">Consulta</param>
        /// <param name="queryColNames_DisplayColNames">(opcional) Columna(query) -> nombre a mostrar, ej. [NombreUsuario, Nombre de usuario]</param>
        public SeleccionarGenerico(QueryBuilder query, string connName, Dictionary<string, string> queryColNames_DisplayColNames = null, string[] FilterBy = null)
        {
            InitializeComponent();
            this.query = query;
            if (query.CurrentLimit == 0)
                query.Limit(300);
            ConnName = connName;
            HabilitarBusqueda = true;
            var dtFiltro = new DataTable();
            dtFiltro.Columns.Add(new DataColumn("value"));
            dtFiltro.Columns.Add(new DataColumn("name"));
            if (queryColNames_DisplayColNames != null)
            {
                this.query.Select(queryColNames_DisplayColNames.Keys.ToArray());
                foreach (var item in queryColNames_DisplayColNames.Keys)
                {
                    dgvContenido.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = item,
                        DataPropertyName = GetColumnName(item),
                        HeaderText = queryColNames_DisplayColNames[item],
                        SortMode = DataGridViewColumnSortMode.NotSortable,

                    });
                    if (FilterBy == null)
                        dtFiltro.Rows.Add(item, queryColNames_DisplayColNames[item]);
                    else if (FilterBy.Contains(item))
                    {
                        dtFiltro.Rows.Add(item, queryColNames_DisplayColNames[item]);
                    }
                }

            }
            else
            {
                foreach (var item in query.GetCurrentColumnsAlias())
                {
                    var r = dtFiltro.NewRow();
                    r["name"] = item;
                    dtFiltro.Rows.Add(r);
                    dgvContenido.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = item,
                        DataPropertyName = item,
                        HeaderText = item,
                        SortMode = DataGridViewColumnSortMode.NotSortable,
                    });
                }
                string[] colNames = query.GetCurrentColumnsNames();
                for (int i = 0; i < colNames.Length; i++)
                {
                    string item = colNames[i];
                    dtFiltro.Rows[i]["value"] = item;

                }
            }
            cbFiltro.DataSource = dtFiltro;
            cbFiltro.ValueMember = "value";
            cbFiltro.DisplayMember = "name";
            if (dtFiltro.Rows.Count > 0)
                cbFiltro.SelectedIndex = 0;

            MostrarBotonesDeSeleccionYCancelar = true;
            CerrarAlSeleccionar = true;
        }
        public void Buscar()
        {
            if (dgvContenido.Disposing || dgvContenido.IsDisposed) return;
            if (dgvContenido.InvokeRequired)
            {
                dgvContenido.BeginInvoke(new MethodInvoker(Buscar));
                return;
            }
            Cursor = Cursors.WaitCursor;
            try
            {
                DataTable dt;
                if (string.IsNullOrEmpty(Busqueda))
                {
                    dt = query.Get(ConnName);
                }
                else
                {
                    var q = query.Copy();
                    q.Where(cbFiltro.SelectedValue.ToString(), "LIKE", $"%{Busqueda}%");
                    dt = q.Get(ConnName);
                }
                dgvContenido.DataSource = dt;
                AjustarTamano();
            }
            catch (Exception e)
            {
                //ey
                MessageBox.Show(e.ToString());
            }
            Cursor = Cursors.Default;
        }

        private void AjustarTamano()
        {
            int ancho = 0;
            foreach (DataGridViewColumn c in dgvContenido.Columns)
            {
                ancho += c.Width;
            }
            if (ancho <= MinimumSize.Width)
                Size = MinimumSize;
            else
            {
                Size = new Size(ancho, Size.Height);
            }
        }

        private void SeleccionarGenerico_Load(object sender, EventArgs e)
        {
            if(dgvContenido.Rows.Count == 0)
            {
                Buscar();
                if (dgvContenido.Rows.Count == 1 && dgvContenido.SelectedRows.Count == 1)
                    Seleccionar();
            }
                 
        }

        private void tbBusqueda_TextChanged(object sender, EventArgs e)
        {
            Task.Run(() =>
                EjecutarFuncionConDelay(Buscar, 500, ref cBusqueda)
                );
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Seleccionar();
        }

        private void Seleccionar()
        {
            if (dgvContenido.SelectedRows.Count == 0) MessageBox.Show("No se seleccionó nada");
            SelectedRow = GetSelectedRow();
            if (!CerrarAlSeleccionar) return;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void dgvContenido_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
                Seleccionar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        public class ItemCambiaEventArgs : EventArgs
        {
            public DataRow SelectedRow { get; set; }

            public ItemCambiaEventArgs(DataRow selectedRow)
            {
                SelectedRow = selectedRow;
            }
        }

        private void dgvContenido_SelectionChanged(object sender, EventArgs e)
        {
            ItemSeleccionadoCambia?.Invoke(this, new ItemCambiaEventArgs(GetSelectedRow()));
        }
        public void Actualizar()
        {
            Buscar();
        }


        /// <summary>
        /// Ejecuta una acción después de un tiempo transcurrido
        /// </summary>
        /// <param name="funcion">Función a ejecutar</param>
        /// <param name="delayMs">Retraso en milisegundos</param>
        /// <param name="contador">Crear una variable int a nivel clase y establecerla en 0, posteriormente pasarla como parámetro</param>
        public static void EjecutarFuncionConDelay(Action funcion, int delayMs, ref int contador)
        {
            contador++;
            Thread.Sleep(delayMs);
            if (contador == 1)
                funcion.Invoke();
            contador--;
        }

        private string GetColumnName(string column)
        {
            if (column.ToLower().Contains(" as "))
            {
                var aux = column.Replace(" AS ", " as ")
                    .Replace(" As ", " as ")
                    .Replace(" aS ", " as ")
                    .Replace(" as ", ";")
                    .Split(';');
                if (aux.Length > 0)
                    return aux[aux.Length - 1];
            }
            else if (column.Contains("."))
            {
                var aux = column.Split('.');
                return aux[aux.Length - 1];
            }

             return column;
        }

        private void dgvContenido_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvContenido.SelectedRows.Count == 1 && e.KeyCode == Keys.Enter)
                Seleccionar();
        }
    }
}

using Mane.BD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mane.Helpers
{
    public partial class MostrarDatosGenerico : Form
    {

        private QueryBuilder query { get; set; }
        public event EventHandler<ItemCambiaEventArgs> ItemSeleccionadoCambia;

        private string Desde => Bd.toDateSqlFormat(dtpDesde.Value);
        private string Hasta => Bd.toDateSqlFormat(dtpHasta.Value);
        /// <summary>
        /// Indica la columna que se utilizará para filtrar los datos por fecha
        /// </summary>
        public string ColumnaFiltradoFecha
        {
            get => columnaFiltradoFecha; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    grpFecha.Visible = false;
                }
                else
                {
                    grpFecha.Visible = true;
                }
                columnaFiltradoFecha = value;
            }
        }
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

        private string ConnName;
        private int cBusqueda;
        private bool habilitarBusqueda;
        private string columnaFiltradoFecha;

        public DataRow SelectedRow { get; private set; }

        private DataRow getSelectedRow()
        {
            if (dgvContenido.SelectedRows.Count == 0) return null;
            var dtSource = (dgvContenido.DataSource as DataTable);
            return dtSource.Rows[dgvContenido.SelectedRows[0].Index];

        }
        private string FormatCol(string col)
        {
            if (col.Contains("."))
            {
                var aux = col.Split('.');
               return aux[aux.Length - 1];
            }
            return col;
        }
        /// <summary>
        /// Seleccion de elementos generico
        /// </summary>
        /// <param name="query">Consulta</param>
        /// <param name="queryColNames_DisplayColNames">(opcional) Columna(query) -> nombre a mostrar, ej. [NombreUsuario, Nombre de usuario]</param>
        public MostrarDatosGenerico(QueryBuilder query, string connName, Dictionary<string, string> queryColNames_DisplayColNames = null, string[] FilterBy = null)
        {
            InitializeComponent();
            this.query = query;
            query.limit(300);
            ConnName = connName;
            HabilitarBusqueda = true;
            ColumnaFiltradoFecha = "";
            dtpDesde.Value = DateTime.Now.AddMonths(-1);
            dtpDesde.ValueChanged += (s, e) => Buscar();
            dtpHasta.ValueChanged += (s, e) => Buscar();
            var dtFiltro = new DataTable();
            dtFiltro.Columns.Add(new DataColumn("value"));
            dtFiltro.Columns.Add(new DataColumn("name"));
            
            if (queryColNames_DisplayColNames != null)
            {
                this.query.select(queryColNames_DisplayColNames.Keys.ToArray());
                foreach (var item in queryColNames_DisplayColNames.Keys)
                {
                    dgvContenido.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = item,
                        DataPropertyName = FormatCol(item),
                        HeaderText = queryColNames_DisplayColNames[item],
                        SortMode = DataGridViewColumnSortMode.NotSortable,

                    });
                    if (FilterBy == null)
                        dtFiltro.Rows.Add(item, queryColNames_DisplayColNames[item]);
                    else if (FilterBy.Contains(item))
                        dtFiltro.Rows.Add(item, queryColNames_DisplayColNames[item]);
                    
                }
            }
            else
            {
                foreach (var item in query.getCurrentColumnsAlias())
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
                string[] colNames = query.getCurrentColumnsNames();
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
        }
        private void Buscar()
        {
            if (dgvContenido.Disposing || dgvContenido.IsDisposed) return;
            if (dgvContenido.InvokeRequired)
            {
                dgvContenido.BeginInvoke(new MethodInvoker(Buscar));
                return;
            }
            Cursor = Cursors.WaitCursor;
            DataTable dt = null;
            try
            {
                var q = query.copy();
                if (!string.IsNullOrEmpty(ColumnaFiltradoFecha))
                    q.whereBetween(ColumnaFiltradoFecha, Desde, Hasta);
                if (string.IsNullOrEmpty(Busqueda))
                {
                    dt = q.get(ConnName);
                }
                else
                {
                    q.where(cbFiltro.SelectedValue.ToString(), "LIKE", $"%{Busqueda}%");
                    dt = q.get(ConnName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgvContenido.DataSource = dt;
            Cursor = Cursors.Default;
        }

        private void SeleccionarGenerico_Load(object sender, EventArgs e)
        {
            Buscar();
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
            SelectedRow = getSelectedRow();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void dgvContenido_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
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
            ItemSeleccionadoCambia?.Invoke(this, new ItemCambiaEventArgs(getSelectedRow()));
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
    }
}

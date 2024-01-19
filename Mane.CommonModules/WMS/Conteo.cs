using Mane.BD;
using Mane.BD.Forms;
using Mane.BD.Helpers;
using Mane.Helpers;
using Mane.Helpers.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAPbobsCOM;

using System.Reflection;

namespace Mane.CommonModules.WMS
{
    public partial class Conteo : Form
    {
        public string Articulo { get => tbArticulo.Text; set => tbArticulo.Text = value; }
        public double Cantidad { get => Convert.ToDouble(nudCantidad.Value); set => nudCantidad.Value = Convert.ToDecimal(value); }
        public string Ubicacion { get => tbUbicacion.Text; set => tbUbicacion.Text = value; }
        public string Almacen
        {
            get => (cbAlmacen.SelectedItem as AlmacenModel)?.WhsCode.ToString();
            set
            {
                cbAlmacen.SelectedValue = value;
            }
        }
        public string Comentarios { get => tbComentarios.Text; set => tbComentarios.Text = value; }
        public string SerieLote { get => tbSerieLote.Text; set => tbSerieLote.Text = value; }
        public string NoConteo { get => tbNoConteo.Text; set => tbNoConteo.Text = value; }
        public string Unidad { get => cbUnidad.SelectedValue?.ToString(); set => cbUnidad.SelectedValue = value; }
        AlmacenModel AlmacenObj => cbAlmacen.SelectedItem as AlmacenModel;

        private bool EditandoConteo
        {
            get => editandoConteo; set
            {
                editandoConteo = value;
                btnCrear.Text = value ? "Actualizar" : "Procesar";
            }
        }
        private LineaConteo EditLn;
        private string TipoManejo;
        private BindingList<LineaConteo> Lineas;
        private bool editando;
        private bool editandoConteo;
        private List<LineaConteo> LineasNuevas = new List<LineaConteo>();
        private List<LineaConteo> LineasEliminadas = new List<LineaConteo>();
        private List<LineaConteo> LineasPreProcesadas = new List<LineaConteo>();
        private List<LineaConteo> LineasNuevasPreProcesadas = new List<LineaConteo>();

        public Conteo()
        {
            InitializeComponent();
            Lineas = new BindingList<LineaConteo>();
            dgvConteo.AutoGenerateColumns = false;
            dgvConteo.DataSource = Lineas;
            cbAlmacen.DisplayMember = "DisplayName";
            cbAlmacen.ValueMember = "WhsCode";
            cbUnidad.DisplayMember = "UomName";
            cbUnidad.ValueMember = "UomCode";
            Task.Run(() =>
            {
                //Cargar los almacenes en segundo plano para que el formulario abra más rápido
                var alms = AlmacenModel.AlmacenesActivos();
                cbAlmacen.BeginInvoke(new MethodInvoker(() => cbAlmacen.DataSource = alms));
            });

        }
        private void ContarArticulo()
        {
            if (!ValidarContarArticulo())
                return;


            var matchs = (from DataGridViewRow r in dgvConteo.Rows
                          where (r.DataBoundItem as LineaConteo).ItemCode == Articulo
                          && (r.DataBoundItem as LineaConteo).FromWhs == Almacen
                          select r).ToArray();
            if (AlmacenObj.BinActivat == "Y")
                matchs = matchs.Where(x => (x.DataBoundItem as LineaConteo).BinCode == Ubicacion).ToArray();
            var lineNum = -1;
            DataGridViewRow match = null;
            if (TipoManejo != QueriesSapb1.TipoManejoNinguno)
            {
                match = matchs.Where(x => (x.DataBoundItem as LineaConteo).SerieLote == SerieLote).FirstOrDefault();
                if (match == null && matchs.Length > 0)
                    lineNum = (matchs.First().DataBoundItem as LineaConteo).LineNum;
            }
            else
            {
                match = matchs.FirstOrDefault();
            }

            var dt = QueriesSapb1.Stock(Ubicacion, AlmacenObj.WhsCode, Articulo, SerieLote).Get(Configuration.ConnectionName);
            if (match == null)
            {
                //if (!MsgBox.confirm("El artículo no se encuentra en la lista de conteo actual ¿Desea agregarlo?"))
                //    return;
                var itemName = Bd.Query("OITM").Select("ItemName")
                    .Where("ItemCode", Articulo).GetScalar(Configuration.ConnectionName, "").ToString();
                var um = cbUnidad.SelectedItem as UnidadDeMedidaArticulo;
                var ln = new LineaConteo()
                {
                    LineNum = lineNum,
                    ItemCode = Articulo,
                    ItemDescription = itemName,
                    Quantity = 0,
                    BinCode = Ubicacion,
                    SerieLote = SerieLote,
                    FromWhs = Almacen,
                    UnidadDeMedida = Unidad,
                    ArticulosPorUnidad = um.Quantity,
                    TipoManejo = TipoManejo
                };
                if (dt.Rows.Count > 0)
                {
                    if (lineNum == -1)
                        ln.Quantity = dt.Rows[0]["Quantity"].ToDbl();
                    ln.SysNumber = dt.Rows[0]["SysNumber"].ToInt();
                }

                ln.CantidadContada = Cantidad * ln.ArticulosPorUnidad;

                if (lineNum == -1)
                {
                    Lineas.Add(ln);
                    LineasNuevas.Add(ln);
                    match = dgvConteo.Rows[dgvConteo.Rows.Count - 1];
                }
                else
                {
                    var lnPadre = matchs.First().DataBoundItem as LineaConteo;
                    ln.Quantity = lnPadre.Quantity;
                    var indexPadre = Lineas.IndexOf(lnPadre);
                    Lineas.Insert(indexPadre + 1, ln);
                    match = dgvConteo.Rows[indexPadre + 1];
                }


            }
            else
            {
                var ln = match.DataBoundItem as LineaConteo;
                var um = cbUnidad.SelectedItem as UnidadDeMedidaArticulo;
                ln.SerieLote = SerieLote;
                if (Unidad != ln.UnidadDeMedida)
                {
                    ln.UnidadDeMedida = Unidad;
                    ln.ArticulosPorUnidad = um.Quantity;
                    ln.CantidadContada = 0;
                }
                ln.CantidadContada += ln.ArticulosPorUnidad * Cantidad;
                ln.UnidadesDeMedida.Clear();
                if (dt.Rows.Count > 0)
                {
                    ln.Quantity = dt.Rows[0]["Quantity"].ToDbl();
                    ln.SysNumber = dt.Rows[0]["SysNumber"].ToInt();
                }
            }
            dgvConteo.ClearSelection();
            dgvConteo.FirstDisplayedScrollingRowIndex = match.Index;
            match.Selected = true;

            FormatearRow(match);
            (match.DataBoundItem as LineaConteo).Contado = true;
            Articulo = "";
            SerieLote = "";
            Cantidad = 1;
            nudCantidad.ReadOnly = false;
            tbArticulo.Focus();
        }

        private bool ValidarContarArticulo()
        {
            var valid = true;
            try
            {
                if (string.IsNullOrWhiteSpace(Articulo)
                || (AlmacenObj.BinActivat == "Y" && string.IsNullOrWhiteSpace(Ubicacion))
                || (TipoManejo != QueriesSapb1.TipoManejoNinguno && string.IsNullOrWhiteSpace(SerieLote))
                ) throw new Exception("Falta llenar alguno de los campos");

                if (!Bd.Query("OITM").Where("ItemCode", Articulo).Where("validFor", "Y").Exists(Configuration.ConnectionName))
                    throw new Exception("El artículo seleccionado no existe o está deshabilitado");

                if (AlmacenObj.BinActivat == "Y")
                {
                    if (!Bd.Query("OBIN").Where("BinCode", Ubicacion).Where("WhsCode", AlmacenObj.WhsCode).Exists(Configuration.ConnectionName))
                        throw new Exception("La ubicación seleccionada no existe o no pertenece al almacén seleccionado");
                }

                if (Cantidad <= 0)
                    throw new Exception("La cantidad no puede ser negativa o inferior a cero");


            }
            catch (Exception e)
            {
                MsgBox.warning(e.Message);
                valid = false;
            }
            return valid;
        }

        private void CargarUnidades()
        {
            Cursor = Cursors.WaitCursor;
            cbUnidad.DataSource = UnidadDeMedidaArticulo.Get(Articulo);
            Cursor = Cursors.Default;
        }
        private void BuscarArticuloEnLaTabla()
        {

            Cursor = Cursors.WaitCursor;
            var matchs = from DataGridViewRow r in dgvConteo.Rows
                         where (r.DataBoundItem as LineaConteo).ItemCode == Articulo
                         && (r.DataBoundItem as LineaConteo).FromWhs == Almacen
                         select r;
            if (!string.IsNullOrWhiteSpace(Ubicacion) && AlmacenObj.BinActivat == "Y")
                matchs = matchs.Where(r => r.Cells[colUbicacion.Name].Value?.ToString() == Ubicacion);
            //if (!string.IsNullOrWhiteSpace(SerieLote))
            //    matchs = matchs.Where(r => r.Cells[colSerieLote.Name].Value?.ToString() == SerieLote);

            var row = matchs.FirstOrDefault();
            if (row != null)
            {
                dgvConteo.ClearSelection();
                row.Selected = true;
                dgvConteo.FirstDisplayedScrollingRowIndex = row.Index;
            }
            Cursor = Cursors.Default;
        }

        private void tbArticulo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Cursor = Cursors.WaitCursor;
                if (QueriesSapb1.BuscarArticulo(out string item, Articulo, Configuration.ConnectionName))
                {
                    Articulo = item;
                    TipoManejo = QueriesSapb1.TipoDeManejo(Articulo, Configuration.ConnectionName);
                    if (TipoManejo == QueriesSapb1.TipoManejoNinguno)
                    {
                        SerieLote = "";
                        tbSerieLote.Enabled = false;
                        nudCantidad.ReadOnly = false;
                    }
                    else
                    {
                        tbSerieLote.Enabled = true;
                        if (TipoManejo == QueriesSapb1.TipoManejoSerie)
                        {
                            Cantidad = 1;
                            nudCantidad.ReadOnly = true;
                        }
                        else nudCantidad.ReadOnly = false;
                    }
                    CargarUnidades();
                    BuscarArticuloEnLaTabla();
                    Cantidad = 1;
                    nudCantidad.Select(0, 1);
                    nudCantidad.Focus();
                }
                Cursor = Cursors.Default;
            }
        }

        private void cbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbUbicacion.Enabled = QueriesSapb1.TieneUbicacionesActivas(Almacen, Configuration.ConnectionName);
            Ubicacion = "";
        }

        private void nudCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Cantidad <= 0)
                    return;
                if ((cbAlmacen.SelectedItem as AlmacenModel).BinActivat == "Y")
                {
                    tbUbicacion.SelectAll();
                    tbUbicacion.Focus();
                }
                else if (TipoManejo == QueriesSapb1.TipoManejoNinguno)
                {
                    ContarArticulo();
                }
                else
                {
                    tbSerieLote.SelectAll();
                    tbSerieLote.Focus();
                }
            }
        }

        private void tbUbicacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Cursor = Cursors.WaitCursor;
                if (QueriesSapb1.BuscarUbicacion(out string binCode, Ubicacion, Almacen, Configuration.ConnectionName))
                {
                    Ubicacion = binCode;
                    if (TipoManejo == QueriesSapb1.TipoManejoNinguno)
                        ContarArticulo();
                    else
                    {
                        BuscarArticuloEnLaTabla();
                        tbSerieLote.SelectAll();
                        tbSerieLote.Focus();
                    }
                }
                Cursor = Cursors.Default;
            }
        }

        private void tbSerieLote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(SerieLote))
                    if (QueriesSapb1.Stock(Ubicacion, Almacen, Articulo, SerieLote).Exists(Configuration.ConnectionName))
                    {
                        ContarArticulo();
                        return;
                    }
                var snbOk = false;
                var query = QueriesSapb1.Stock(Ubicacion, Almacen, Articulo);
                using (var fm = new SeleccionarGenerico(query, Configuration.ConnectionName))
                {
                    fm.Busqueda = SerieLote;
                    fm.HabilitarBusqueda = false;
                    fm.MostrarSoloEstasColumnas = new string[] { "DistNumber", "Quantity" };
                    fm.OrdenDeColumnas = new Dictionary<string, int>
                    {
                        {"DistNumber",0 },
                        {"Quantity",1 }
                    };
                    fm.RenombrarEstasColumnas = new Dictionary<string, string>
                    {
                        {"DistNumber","Serie/Lote" },
                        {"Quantity","Stock" }
                    };
                    if (fm.ShowDialog() == DialogResult.OK)
                    {
                        SerieLote = fm.SelectedRow["DistNumber"].ToString();
                        snbOk = true;
                    }
                }
                if (snbOk)
                    ContarArticulo();
            }
        }
        private bool SeEncuentraRepetida(LineaConteo ln)
        {
            return Lineas.Where(l => l.ItemCode == ln.ItemCode
            && l.BinCode == ln.BinCode
            && l.SerieLote == ln.SerieLote).Count() > 0;

        }
        private void btnAgregarArticulos_Click(object sender, EventArgs e)
        {
            AgregarArticulos();
        }
        private void AgregarArticulos()
        {
            using (var fm = new BuscarArticulosConteo())
            {
                fm.Almacen = Almacen;
                if (fm.ShowDialog() == DialogResult.OK)
                {
                    var dt = fm.dgvStock.DataSource as DataTable;
                    Cursor = Cursors.WaitCursor;
                    var lns = new List<LineaConteo>();
                    foreach (DataRow row in dt.Rows)
                    {
                        var ln = new LineaConteo
                        {
                            ItemCode = row["ItemCode"].ToStr(),
                            ItemDescription = row["ItemName"].ToStr(),
                            Quantity = row["Quantity"].ToDbl(),
                            BinCode = row["BinCode"].ToStr(),
                            FromWhs = row["WhsCode"].ToStr(),
                            SerieLote = row["DistNumber"].ToStr(),
                            SysNumber = row["SysNumber"].ToInt(),
                            TipoManejo = row["TipoManejo"].ToStr()
                        };
                        if (!SeEncuentraRepetida(ln))
                        {
                            lns.Add(ln);
                            LineasNuevas.Add(ln);
                        }
                    }
                    if (lns.Count > 0)
                    {
                        var lineas = (from ln in lns select ln.ItemCode).ToArray();
                        var dtum = QueriesSapb1.UnidadesDeMedidaArticulo(lineas).Get(Configuration.ConnectionName).AsEnumerable();
                        foreach (var ln in lns)
                        {
                            var row = dtum.Where(r => r["ItemCode"].ToStr() == ln.ItemCode).FirstOrDefault();
                            if (row != null)
                            {
                                ln.UnidadDeMedida = row["UomCode"].ToStr();
                                ln.ArticulosPorUnidad = row["BaseQty"].ToDbl() / row["AltQty"].ToDbl();
                            }
                            Lineas.Add(ln);
                        }
                    }
                    Cursor = Cursors.Default;

                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BorrarLineaClickeada();
        }

        private void BorrarLineaClickeada()
        {
            if (_ClickedRow == null)
                return;

            var ln = _ClickedRow.DataBoundItem as LineaConteo;
            if (ln.LineNum != -1)
            {
                if (Lineas.Where(x => x.LineNum == ln.LineNum).Count() == 1)
                    LineasEliminadas.Add(ln);
            }
            else
            {
                LineasNuevas.Remove(ln);
            }
            FormatearRow(_ClickedRow);
            Lineas.Remove(ln);
        }
        private void FormatearRow(DataGridViewRow row)
        {
            var ln = row.DataBoundItem as LineaConteo;
            double diferencia = 0;
            double contado = 0;
            if (ln.LineNum == -1)
            {
                diferencia = ln.Diferencia;
                contado = ln.CantidadContada;
                row.DefaultCellStyle.ForeColor = diferencia == 0 || contado == 0 ? Color.Black : Color.Red;
            }
            else
            {
                var parientes = (from DataGridViewRow r in dgvConteo.Rows
                                 where (r.DataBoundItem as LineaConteo).LineNum == ln.LineNum
                                 select r);
                foreach (var item in parientes)
                {
                    contado += (item.DataBoundItem as LineaConteo).CantidadContada;
                }
                diferencia = contado - ln.Quantity;
                foreach (var item in parientes)
                {
                    item.DefaultCellStyle.ForeColor = diferencia == 0 || contado == 0 ? Color.Black : Color.Red;
                }
            }
        }
        private void chkFijar_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in Lineas)
            {
                item.Fijar = chkFijar.Checked;
            }
        }

        private void chkContado_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in Lineas)
            {
                item.Contado = chkContado.Checked;
            }
        }

        private void btnNuevoConteo_click(object sender, EventArgs e)
        {
            NuevoConteo();
        }

        private void NuevoConteo()
        {
            if (Lineas.Count > 0)
                if (!Mane.Helpers.MsgBox.confirm("La información actual no se guardará ¿Seguro que desea iniciar un nuevo conteo?"))
                    return;
            Articulo =
            Ubicacion =
            SerieLote = "";
            EditandoConteo = false;
            cbAlmacen.Enabled = true;
            Lineas.Clear();
            LineasEliminadas.Clear();
            LineasNuevas.Clear();
        }

        private void CargarConteo()
        {
            //if (Lineas.Count > 0)
            //    if (!MsgBox.confirm("La información capturada actualmente se perderá, ¿Desea continuar de todos modos?"))
            //        return;
            Cursor = Cursors.WaitCursor;
            if (Lineas.Count > 0)
                if (Mane.Helpers.MsgBox.confirm($"La información actual no se guardará ¿Seguro que desea cargar el conteo {NoConteo}?"))
                    Lineas.Clear();
                else
                {
                    Cursor = Cursors.Default;
                    return;
                }
            NuevoConteo();
            var dt = Bd.Query("OINC t0")
                .Join("INC1 t1", "t1.DocEntry", "t0.DocEntry")
                .LeftJoin("OBIN t2", "t2.AbsEntry", "t1.BinEntry")
                .LeftJoin("INC3 t3", "t3.DocEntry", "t0.DocEntry", q => q.WhereColumn("t3.LineNum", "t1.LineNum"))
                .LeftJoin("OBTN t4", "t4.AbsEntry", "t3.ObjAbs", q => q.Where("t3.ObjId", QueriesSapb1.SnbTypeLote))
                .LeftJoin("OSRN t5", "t5.AbsEntry", "t3.ObjAbs", q => q.Where("t3.ObjId", QueriesSapb1.SnbTypeSerie))
                .Join("OITM t6", "t6.ItemCode", "t1.ItemCode")
                .Join("OUGP t7", "t7.UgpEntry", "t6.UgpEntry")
                .Join("UGP1 t8", "t8.UgpEntry", "t7.UgpEntry", q => q.WhereColumn("t8.UomEntry", "t1.IUomEntry"))
                .Select("t1.DocEntry", "t1.LineNum", "t1.ItemCode", "t1.ItemDesc", "t1.WhsCode",
                "t1.Freeze", "t1.Counted", "t1.InWhsQty", "t7.UgpEntry", "t1.UomCode", "t2.BinCode", "t8.AltQty", "t8.BaseQty",
                "t0.Remarks")
                .Case(c =>
                c.When("t3.ObjId", QueriesSapb1.SnbTypeLote)
                .ThenColumn("t4.DistNumber")
                .When("t3.ObjId", QueriesSapb1.SnbTypeSerie)
                .ThenColumn("t5.DistNumber")
                .As("DistNumber"))

                .Case(c =>
                c.When("t3.ObjId", QueriesSapb1.SnbTypeLote)
                .ThenColumn("t4.SysNumber")
                .When("t3.ObjId", QueriesSapb1.SnbTypeSerie)
                .ThenColumn("t5.SysNumber")
                .As("SysNumber"))

                .Case(c =>
                c.WhenNull("t3.Quantity")
                .ThenColumn("t1.CountQty")
                .ElseColumn("t3.Quantity")
                .As("CountQty"))
                .Case(c =>
            c.When("t6.ManBtchNum", "Y").Then(QueriesSapb1.TipoManejoLote)
             .When("t6.ManSerNum", "Y").Then(QueriesSapb1.TipoManejoSerie)
             .Else(QueriesSapb1.TipoManejoNinguno).As("TipoManejo"))
                .Where("t0.DocNum", NoConteo)
                .OrderBy("t1.VisOrder")
                .Get(Configuration.ConnectionName);

            foreach (DataRow r in dt.Rows)
            {
                Lineas.Add(new LineaConteo
                {
                    DocEntry = r["DocEntry"].ToInt(),
                    LineNum = r["LineNum"].ToInt(),
                    ItemCode = r["ItemCode"].ToStr(),
                    ItemDescription = r["ItemDesc"].ToStr(),
                    Quantity = r["InWhsQty"].ToDbl(),
                    BinCode = r["BinCode"].ToStr(),
                    FromWhs = r["WhsCode"].ToStr(),
                    CantidadContada = r["CountQty"].ToDbl(),
                    SerieLote = r["DistNumber"].ToStr(),
                    SysNumber = r["SysNumber"].ToInt(),
                    Fijar = r["Freeze"].ToStr() == "Y",
                    Contado = r["Counted"].ToStr() == "Y",
                    UnidadDeMedida = r["UomCode"].ToStr(),
                    ArticulosPorUnidad = r["BaseQty"].ToDbl() / r["AltQty"].ToDbl(),
                    TipoManejo = r["TipoManejo"].ToStr(),
                });
            }
            if (dt.Rows.Count > 0)
                Comentarios = dt.Rows[0]["Remarks"].ToStr();
            //Información de lineas con multiples UdMs
            dt = Bd.Query("INC2")
                .Where("DocEntry", Lineas[0].DocEntry)
                .Get(Configuration.ConnectionName);
            foreach (DataRow row in dt.Rows)
            {
                var ln = Lineas.Where(l => l.LineNum == row["LineNum"].ToInt()).First();
                ln.UnidadesDeMedida.Add(new UdmConteo
                {
                    CantidadPorUnidad = row["ItmsPerUnt"].ToDbl(),
                    CantidadContadaUnidad = row["UomQty"].ToDbl(),
                    CodigoDeUnidad = row["UomCode"].ToStr()
                });
            }

            foreach (DataGridViewRow item in dgvConteo.Rows)
            {
                FormatearRow(item);
            }
            EditandoConteo = true;
            Cursor = Cursors.Default;
        }
        private void ActualizarConteo()
        {
            PreProcesarLineas();
            Cursor = Cursors.WaitCursor;
            using(var executor = new Mane.Sap.SapExecutor(Sap.Sap.Conexiones.Find(Configuration.ConnectionName)))
            {
                var oCompany = executor.Company;
                try
                {
                    if (LineasPreProcesadas.Count == 0)
                        throw new Exception("EL conteo de inventario está vacío");
                    if (oCompany.Connect() != 0)
                        throw new Exception(oCompany.GetLastErrorDescription());

                    CompanyService cmpnyService = oCompany.GetCompanyService();
                    InventoryCountingsService countingService = cmpnyService.GetBusinessService(ServiceTypes.InventoryCountingsService) as InventoryCountingsService;
                    SAPbobsCOM.InventoryCountingParams countParams = countingService.GetDataInterface(InventoryCountingsServiceDataInterfaces.icsInventoryCountingParams);
                    countParams.DocumentEntry = LineasPreProcesadas[0].DocEntry;
                    InventoryCounting invCounting = countingService.Get(countParams);
                    invCounting.Remarks = Comentarios;
                    InventoryCountingLines countingLines = invCounting.InventoryCountingLines;
                    foreach (var ln in LineasEliminadas)
                    {
                        for (int i = 0; i < countingLines.Count; i++)
                        {
                          var item =  countingLines.Item(i);
                            if(item.LineNumber == ln.LineNum)
                            {
                                countingLines.Remove(i);
                                break;
                            }
                        }
                    }
                    EditarLineas(LineasPreProcesadas, countingLines);
                    AgregarLineas(LineasNuevasPreProcesadas, countingLines);
                    countingService.Update(invCounting);
                    MsgBox.info("Recuento actualizado con éxito");
                    oCompany.Disconnect();
                    Lineas.Clear();
                    CargarConteo();
                }
                catch (Exception ex)
                {
                    MsgBox.error("Error al crear conteo: " + ex.Message);
                    if (oCompany.Connected)
                        oCompany.Disconnect();
                }
            }
           

            Cursor = Cursors.Default;
        }

        private void EditarLineas(List<LineaConteo> lineas, InventoryCountingLines countingLines)
        {
            InventoryCountingLine FindByLineNum(int lineNum)
            {
                int busquedaBinaria(int lnNum, int left, int right)
                {
                    if (left > right)
                        return -1;
                    int mid = left + (right - left) / 2;
                    var itm = countingLines.Item(mid);
                    if (itm.LineNumber == lnNum)
                        return mid;
                    else if (itm.LineNumber < lnNum)
                        return busquedaBinaria(lnNum, mid + 1, right);
                    else
                        return busquedaBinaria(lnNum, left, mid - 1);
                }
                //buscar primero un pasito atrás
                var index = lineNum - 1;
                if (index >= 0)
                {
                    try
                    {
                        var ln = countingLines.Item(index);
                        if (ln.LineNumber == lineNum)
                            return ln;
                    }
                    catch (Exception)
                    {

                    }
                }
                //busqueda binaria
                index = busquedaBinaria(lineNum, 0, countingLines.Count);
                if (index == -1)
                {//buscar a la fuerza
                    for (int i = 0; i < countingLines.Count; i++)
                    {
                        var ln = countingLines.Item(i);
                        if (ln.LineNumber == lineNum)
                            return ln;
                    }
                }
                else return countingLines.Item(index);

                return countingLines.Item(0);
            }
            foreach (var ln in lineas)
            {
                try
                {
                    var countLn = FindByLineNum(ln.LineNum);

                    countLn.ItemCode = ln.ItemCode;
                    countLn.CountedQuantity = ln.CantidadContada;
                    countLn.WarehouseCode = ln.FromWhs;
                    countLn.Freeze = ln.Fijar ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                    countLn.Counted = ln.Contado ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                    if (!string.IsNullOrWhiteSpace(ln.BinCode))
                    {
                        countLn.BinEntry = ln.BinAllocations.First().BinAbsEntry;
                    }
                    if (ln.UnidadesDeMedida.Count > 1)
                    {
                        var countUms = countLn.InventoryCountingLineUoMs;
                        var indexUm = 0;
                        foreach (var item in ln.UnidadesDeMedida)
                        {
                            var countUm = countUms.Count > indexUm ? countUms.Item(indexUm) : countUms.Add();
                            countUm.UoMCode = item.CodigoDeUnidad;
                            countUm.CountedQuantity = item.CantidadContada;
                            indexUm++;
                        }
                        for (int i = indexUm; i < countUms.Count; i++)
                        {
                            countUms.Remove(i);
                        }
                    }
                    else
                    {
                        countLn.UoMCode = ln.UnidadDeMedida;
                    }
                    int indexSnb = 0;
                    foreach (var item in ln.BatchNumbers)
                    {
                        var countBtch = countLn.InventoryCountingBatchNumbers.Count > indexSnb ? countLn.InventoryCountingBatchNumbers.Item(indexSnb) : countLn.InventoryCountingBatchNumbers.Add();
                        countBtch.Quantity = item.Quantity;
                        countBtch.BatchNumber = item.BatchNumber;
                        indexSnb++;
                    }

                    for (int i = indexSnb; i < countLn.InventoryCountingBatchNumbers.Count; i++)
                    {
                        countLn.InventoryCountingBatchNumbers.Remove(i);
                    }

                    foreach (var item in ln.SerialNumbers)
                    {
                        var countSer = countLn.InventoryCountingSerialNumbers.Count > indexSnb ? countLn.InventoryCountingSerialNumbers.Item(indexSnb) : countLn.InventoryCountingSerialNumbers.Add();
                        countSer.SystemSerialNumber = item.SystemSerialNumber;
                        countSer.Quantity = item.Quantity;
                        indexSnb++;
                    }
                    for (int i = indexSnb; i < countLn.InventoryCountingSerialNumbers.Count; i++)
                    {
                        countLn.InventoryCountingSerialNumbers.Remove(i);
                    }
                }
                catch (Exception ex)
                {
                    Mane.Helpers.Log.Add(ex);
                    throw ex;
                }


            }
        }

        private void CrearConteo()
        {
            PreProcesarLineas();
            Cursor = Cursors.WaitCursor;
            using (var executor = new Mane.Sap.SapExecutor(Sap.Sap.Conexiones.Find(Configuration.ConnectionName)))
            {
                var oCompany = executor.Company;
                try
                {
                    if (oCompany.Connect() != 0)
                        throw new Exception(oCompany.GetLastErrorDescription());

                    CompanyService cmpnyService = oCompany.GetCompanyService();
                    InventoryCountingsService countingService = cmpnyService.GetBusinessService(ServiceTypes.InventoryCountingsService) as InventoryCountingsService;
                    InventoryCounting invCounting = countingService.GetDataInterface(InventoryCountingsServiceDataInterfaces.icsInventoryCounting) as InventoryCounting;
                    invCounting.CountDate = DateTime.Now;
                    invCounting.Remarks = Comentarios;
                    InventoryCountingLines countingLines = invCounting.InventoryCountingLines;
                    AgregarLineas(LineasPreProcesadas, countingLines);

                    InventoryCountingParams oICP = countingService.Add(invCounting);
                    MsgBox.info("Se creó correctamente el conteo con el folio " + oICP.DocumentNumber);
                    oCompany.Disconnect();
                    NoConteo = oICP.DocumentNumber.ToString();
                    Lineas.Clear();
                    CargarConteo();
                }
                catch (Exception ex)
                {
                    MsgBox.error("Error al crear conteo: " + ex.Message);
                    if (oCompany.Connected)
                        oCompany.Disconnect();
                }
            }
              

            Cursor = Cursors.Default;
        }
        private void AgruparLineas(LineaConteo lineaReferencia, List<LineaConteo> origen, List<LineaConteo> destino)
        {
            var condicion = new Func<LineaConteo, bool>(ln => ln.ItemCode == lineaReferencia.ItemCode
               && ln.FromWhs == lineaReferencia.FromWhs && ln.BinCode == lineaReferencia.BinCode);
            var lineasMismoOrigen = origen.Where(condicion).ToList();
            var lnPadre = lineasMismoOrigen.First().CopyObject();

            switch (lineaReferencia.TipoManejo)
            {
                case QueriesSapb1.TipoManejoSerie:
                    foreach (var item in lineasMismoOrigen)
                    {
                        if (item.CantidadContada == 0) continue;
                        if (!string.IsNullOrWhiteSpace(item.SerieLote))
                        {
                            lnPadre.SerialNumbers.Add(new Mane.Helpers.Common.SerialNumbers
                            {
                                SystemSerialNumber = item.SysNumber,
                                Quantity = item.CantidadContada
                            });
                        }

                        if (string.IsNullOrWhiteSpace(item.BinCode))
                            continue;

                        lnPadre.BinAllocations.Add(new BinAllocations
                        {
                            SerialAndBatchNumbersBaseLine = lnPadre.SerialNumbers.Count - 1,
                            Quantity = item.CantidadContada,
                            BinAbsEntry = dtBins.Where(r => r["BinCode"].ToStr() == item.BinCode)
                            .First()["AbsEntry"].ToInt()
                        });
                    }
                    break;
                case QueriesSapb1.TipoManejoLote:

                    foreach (var item in lineasMismoOrigen)
                    {
                        if (item.CantidadContada == 0) continue;
                        if (!string.IsNullOrWhiteSpace(item.SerieLote))
                        {
                            lnPadre.BatchNumbers.Add(new Mane.Helpers.Common.BatchNumbers
                            {
                                BatchNumber = item.SerieLote,
                                Quantity = item.CantidadContada
                            });

                        }


                        if (string.IsNullOrWhiteSpace(item.BinCode))
                            continue;

                        lnPadre.BinAllocations.Add(new BinAllocations
                        {
                            SerialAndBatchNumbersBaseLine = lnPadre.BatchNumbers.Count - 1,
                            Quantity = item.CantidadContada,
                            BinAbsEntry = dtBins.Where(r => r["BinCode"].ToStr() == item.BinCode)
                            .First()["AbsEntry"].ToInt()
                        });
                    }

                    break;
                default:
                    if (!string.IsNullOrWhiteSpace(lineaReferencia.BinCode))
                    {
                        lnPadre.BinAllocations.Add(new Mane.Helpers.Common.BinAllocations
                        {
                            Quantity = lnPadre.CantidadContada,
                            BinAbsEntry = dtBins.Where(r => r["BinCode"].ToStr() == lineaReferencia.BinCode)
                            .First()["AbsEntry"].ToInt()
                        });

                    }
                    break;
            }
            //agrupar las udms
            var udms = new Dictionary<string, UdmConteo>();
            foreach (var item in lineasMismoOrigen)
            {
                if (item.UnidadesDeMedida.Count <= 1)
                    continue;
                foreach (var udm in item.UnidadesDeMedida)
                {
                    if (udms.ContainsKey(udm.CodigoDeUnidad))
                        udms[udm.CodigoDeUnidad].CantidadContada += udm.CantidadContada;
                    else udms.Add(udm.CodigoDeUnidad, udm);
                }
            }
            foreach (var key in udms.Keys)
            {
                lnPadre.UnidadesDeMedida.Add(udms[key]);
            }
            if (lnPadre.BinAllocations.Count > 0 || lnPadre.SerialNumbers.Count > 0 || lnPadre.BatchNumbers.Count > 0)
            {
                double totalContado = 0;
                foreach (var item in lnPadre.BinAllocations)
                {
                    totalContado += item.Quantity;
                }
                foreach (var item in lnPadre.SerialNumbers)
                {
                    totalContado += item.Quantity;
                }
                foreach (var item in lnPadre.BatchNumbers)
                {
                    totalContado += item.Quantity;
                }
                lnPadre.CantidadContada = totalContado;
            }
            origen.RemoveWhere(condicion); //Optimizar la lista origen
            destino.Add(lnPadre);
        }
        //Guarda las ubicaciones 
        private IEnumerable<DataRow> dtBins;
        /// <summary>
        /// Acomodar las series/lotes y ubicaciones para que parescan como lineas de diapi sap
        /// </summary>
        private void PreProcesarLineas()
        {
            Cursor = Cursors.WaitCursor;
            try
            {

                var binCodes = (from ln in Lineas
                                where !string.IsNullOrWhiteSpace(ln.BinCode)
                                select ln.BinCode).Distinct().ToArray();
                var todasLasLineas = Lineas.ToList();
                //Crear otra lista para no alterar la lista original
                //Borrar las lineas nuevas de la lista de lineas
                foreach (var item in LineasNuevas)
                {
                    todasLasLineas.RemoveWhere(l => l.ItemCode == item.ItemCode
                    && l.SerieLote == item.SerieLote
                    && item.FromWhs == l.FromWhs
                    && l.BinCode == item.BinCode);
                }

                dtBins = Bd.Query("OBIN").Select("AbsEntry", "BinCode")
                   .WhereIn("BinCode", binCodes)
                   .Get(Configuration.ConnectionName).AsEnumerable();
                var hashAgrupadas = new HashSet<string>();
                var lineasCopy = todasLasLineas.ToList();
                LineasPreProcesadas.Clear();
                foreach (var l in todasLasLineas)
                {
                    l.ClearSubInfo();
                    if (!hashAgrupadas.Add(l.ItemCode + l.FromWhs + l.BinCode))
                        continue;
                    AgruparLineas(l, lineasCopy, LineasPreProcesadas);
                }
                hashAgrupadas.Clear();
                lineasCopy = LineasNuevas.ToList();
                LineasNuevasPreProcesadas.Clear();
                foreach (var l in LineasNuevas)
                {
                    l.ClearSubInfo();
                    if (!hashAgrupadas.Add(l.ItemCode + l.FromWhs + l.BinCode))
                        continue;
                    AgruparLineas(l, lineasCopy, LineasNuevasPreProcesadas);
                }
            }
            catch (Exception ex)
            {
                Mane.Helpers.Log.Add(ex);
                Cursor = Cursors.Default;
                throw ex;
            }
            Cursor = Cursors.Default;
        }
        private void tbNoConteo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            if (Bd.Query("OINC").Where("DocNum", NoConteo).Exists(Configuration.ConnectionName))
            {
                CargarConteo();
                return;
            }
            var query = Bd.Query("OINC").Where("Status", "O");
            var colMap = new Dictionary<string, string>
            {
                {"DocNum","No. Conteo" },
                {"CountDate","Fecha" },
                {"Remarks","Comentarios" }
            };
            var filterBy = new string[] { "DocNum", "Remarks" };
            using (var fm = new SeleccionarGenerico(query, Configuration.ConnectionName, colMap, filterBy))
            {
                fm.Busqueda = NoConteo;
                if (fm.ShowDialog() == DialogResult.OK)
                {
                    NoConteo = fm.SelectedRow["DocNum"].ToString();
                    CargarConteo();
                }
            }
        }

        private void copiarToolMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvConteo.SelectedCells.Count == 0)
                return;
            var cell = dgvConteo.SelectedCells[0];
            if (cell.RowIndex == -1 || cell.ColumnIndex == -1)
                return;
            if (cell.Value == null || cell.Value == DBNull.Value)
                Clipboard.SetText("");
            else Clipboard.SetText(cell.Value.ToString());

        }

        private void dgvConteo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            FormatearRow(dgvConteo.Rows[e.RowIndex]);
        }

        private void editarUnidadesDeMedidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_ClickedRow == null)
                return;
            var ln = _ClickedRow.DataBoundItem as LineaConteo;
            using (var fm = new RecuentoPorUm(ln))
            {
                fm.ShowDialog();
                FormatearRow(_ClickedRow);
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (EditandoConteo)
                ActualizarConteo();
            else CrearConteo();
        }

        private void AgregarLineas(List<LineaConteo> lineas, InventoryCountingLines countingLines)
        {
            try
            {
                foreach (var ln in lineas)
                {
                    var countLn = countingLines.Add();
                    countLn.ItemCode = ln.ItemCode;
                    countLn.CountedQuantity = ln.CantidadContada;
                    countLn.WarehouseCode = ln.FromWhs;
                    countLn.Freeze = ln.Fijar ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                    countLn.Counted = ln.Contado ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
                    if (!string.IsNullOrWhiteSpace(ln.BinCode))
                    {
                        countLn.BinEntry = ln.BinAllocations.First().BinAbsEntry;
                    }
                    if (ln.UnidadesDeMedida.Count > 1)
                    {
                        var countUms = countLn.InventoryCountingLineUoMs;
                        foreach (var item in ln.UnidadesDeMedida)
                        {
                            var countUm = countUms.Add();
                            countUm.UoMCode = item.CodigoDeUnidad;
                            countUm.CountedQuantity = item.CantidadContada;
                        }
                    }
                    else
                    {
                        countLn.UoMCode = ln.UnidadDeMedida;
                    }

                    foreach (var item in ln.BatchNumbers)
                    {
                        var countBtch = countLn.InventoryCountingBatchNumbers.Add();
                        countBtch.Quantity = item.Quantity;
                        countBtch.BatchNumber = item.BatchNumber;
                    }

                    foreach (var item in ln.SerialNumbers)
                    {
                        var countSer = countLn.InventoryCountingSerialNumbers.Add();
                        countSer.SystemSerialNumber = item.SystemSerialNumber;
                        countSer.Quantity = item.Quantity;
                    }

                }
            }
            catch (Exception ex)
            {
                Mane.Helpers.Log.Add(ex);
                throw ex;
            }
        }

        private DataGridViewRow _ClickedRow;
        private void dgvConteo_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Right)
            {
                _ClickedRow = dgvConteo.Rows[e.RowIndex];
                menuTabla.Show(Cursor.Position);
            }
            else
            {
                _ClickedRow = null;
            }
        }
    }
}

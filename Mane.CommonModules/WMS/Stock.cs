using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mane.Helpers;
using Mane.BD;
using System.Collections.Generic;
using System;

namespace Mane.CommonModules.WMS
{
    public partial class Stock : Form
    {
        #region Propiedades
        public string Almacen { get => tbAlmacen.Text; set => tbAlmacen.Text = value; }
        public string Ubicacion { get => tbUbicacion.Text; set => tbUbicacion.Text = value; }
        public string Articulo { get => tbArticulo.Text; set => tbArticulo.Text = value; }
        public string SerieLote { get => tbSerieLote.Text; set => tbSerieLote.Text = value; }
        public bool StockConUbicaciones { get => chkStockPorUbicacion.Checked; set => chkStockPorUbicacion.Checked = value; }
        public bool MostrarArticulosSinStock { get => chkSinStock.Checked; set => chkSinStock.Checked = value; }
        public bool MostrarStockBasadoEnSeriesLotes { get => chkStockBasadoEnSerieLote.Checked; set => chkStockBasadoEnSerieLote.Checked = value; }
        public bool MostrarArticulosGestionadosPorSeries { get => chkGestionSerie.Checked; set => chkGestionSerie.Checked = value; }
        public bool MostrarArticulosGestionadosPorLotes { get => chkGestionLotes.Checked; set => chkGestionLotes.Checked = value; }
        public bool MostrarArticulosSinGestion { get => chkGestionNinguna.Checked; set => chkGestionNinguna.Checked = value; }
        #endregion
        public Stock()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AddCopiarTablaOption();
            FormatearForm();
        }
        private void Consultar(object sender, EventArgs e)
        {
            Consultar();
        }

        public void Consultar()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                var dt = ArmarConsulta().Limit(100000).Get(Configuration.ConnectionName);
                dataGridView1.DataSource = dt;
                lbRecuento.Text = $"Mostrando: {dt.Rows.Count} registros";
                lblStock.Text = $"Stock:  {(dt.Rows.Count > 0 ? Convert.ToDouble(dt.Compute("Sum(Quantity)", null)) : 0):n0}";
            }
            catch (Exception ex)
            {
                MsgBox.error(ex.ToString());
            }
            Cursor = Cursors.Default;
        }

        private void BuscarAlmacen(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            var query = Bd.Query();
            query.From("OWHS")
                .Select("WhsCode", "WhsName");
            var dic = new Dictionary<string, string>();
            dic.Add("WhsCode", "Codigo Almacén");
            dic.Add("WhsName", "Nombre Almacén");
            using (var fm = new Mane.BD.Forms.SeleccionarGenerico(query, Configuration.ConnectionName, dic))
            {
                var aux = Almacen.Split(',');
                fm.Busqueda = aux[aux.Length - 1];
                if (fm.ShowDialog() == DialogResult.OK)
                {
                    Almacen = "";
                    aux[aux.Length - 1] = fm.SelectedRow["WhsCode"]?.ToString();
                    foreach (var item in aux)
                    {
                        Almacen += item + ",";
                    }
                    Almacen = Almacen.TrimEnd(',');
                    tbAlmacen.Select(Almacen.Length, 0);
                }
            }
        }
        private void BuscarUbicacion(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            var query = Bd.Query();
            query.From("OBIN")
                .Select("BinCode", "WhsCode", "AbsEntry")
                .Where("Deleted", "N");
            if (!string.IsNullOrEmpty(Almacen))
            {
                if (Almacen.Contains(','))
                    query.WhereIn("WhsCode", Almacen.TrimEnd(',').Split(','));
                else query.Where("WhsCode", Almacen);
            }
            var dic = new Dictionary<string, string>();
            dic.Add("BinCode", "Ubicacion");
            dic.Add("WhsCode", "Almacen");
            using (var fm = new Mane.BD.Forms.SeleccionarGenerico(query, Configuration.ConnectionName, dic))
            {
                var aux = Ubicacion.Split(',');
                fm.Busqueda = aux[aux.Length - 1];
                if (fm.ShowDialog() == DialogResult.OK)
                {
                    Ubicacion = "";
                    aux[aux.Length - 1] = fm.SelectedRow["BinCode"]?.ToString();
                    foreach (var item in aux)
                    {
                        Ubicacion += item + ",";
                    }
                    Ubicacion = Ubicacion.TrimEnd(',');
                    tbUbicacion.Select(Ubicacion.Length, 0);
                }
            }
        }
        private void BuscarArticulo(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            var query = Bd.Query();
            query.From("OITM")
                .Select("ItemCode", "ItemName");
            var dic = new Dictionary<string, string>();
            dic.Add("ItemCode", "Articulo");
            dic.Add("ItemName", "Descripción");
            using (var fm = new Mane.BD.Forms.SeleccionarGenerico(query, Configuration.ConnectionName, dic))
            {
                var aux = Articulo.Split(',');
                fm.Busqueda = aux[aux.Length - 1];
                if (fm.ShowDialog() == DialogResult.OK)
                {
                    Articulo = "";
                    aux[aux.Length - 1] = fm.SelectedRow["ItemCode"]?.ToString();
                    foreach (var item in aux)
                    {
                        Articulo += item + ",";
                    }
                    Articulo = Articulo.TrimEnd(',');
                    tbArticulo.Select(Articulo.Length, 0);//posicionar el cursor al final
                }
            }

        }


        private void checkStateChanged(object sender, EventArgs e)
        {
            // Consultar();
            FormatearForm();
        }

        private void FormatearForm()
        {
            colUbicacion.Visible =
            tbUbicacion.Enabled = StockConUbicaciones;
            colSerieLote.Visible =
            tbSerieLote.Enabled = MostrarStockBasadoEnSeriesLotes;
            if (MostrarArticulosGestionadosPorLotes || MostrarArticulosGestionadosPorSeries)
            {
                chkGestionNinguna.Enabled = true;
            }
            else
            {
                chkGestionNinguna.Enabled = false;
                chkGestionNinguna.Checked = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                dataGridView1.ExportToCSV();
            }
            catch (Exception ex)
            {

                MsgBox.error(ex.Message);
            }
            Cursor = Cursors.Default;
        }

        private QueryBuilder ArmarConsulta()
        {
            var query = Bd.Query("OITM t0")
                .Select("t12.WhsCode", "t0.ItemCode", "t0.ItemName")
                .Join("OITW t1", "t1.ItemCode", "t0.ItemCode")
                .Join("OWHS t12", "t12.WhsCode", "t1.WhsCode");
            //Tipo de Manejo
            query.Case(c =>
            c.When("t0.ManBtchNum", "Y").Then("Lotes")
             .When("t0.ManSerNum", "Y").Then("Series")
             .Else("N/A").As("TipoManejo"));

            if (StockConUbicaciones)
            {
                query.LeftJoin("OIBQ t2", "t2.ItemCode", "t1.ItemCode", q => q.WhereColumn("t2.WhsCode", "t1.WhsCode"))
                .LeftJoin("OBIN t3", "t3.AbsEntry", "t2.BinAbs")
                .AddSelect("t3.BinCode");
                if (MostrarStockBasadoEnSeriesLotes)
                {
                    query.LeftJoin("OBTQ t4", "t4.ItemCode", "t1.ItemCode", q => q.WhereColumn("t4.WhsCode", "t1.WhsCode").Where("t12.BinActivat", "N"))
                   .LeftJoin("OBTN t5", "t5.AbsEntry", "t4.MdAbsEntry")
                   .LeftJoin("OSRQ t6", "t6.ItemCode", "t1.ItemCode", q => q.WhereColumn("t6.WhsCode", "t1.WhsCode").Where("t12.BinActivat", "N"))
                   .LeftJoin("OSRN t7", "t7.AbsEntry", "t6.MdAbsEntry")
                   .LeftJoin("OBBQ t8", "t8.ItemCode", "t1.ItemCode", q => q.WhereColumn("t8.BinAbs", "t2.BinAbs"))
                   .LeftJoin("OBTN t9", "t9.AbsEntry", "t8.SnBMDAbs")
                   .LeftJoin("OSBQ t10", "t10.ItemCode", "t1.ItemCode", q => q.WhereColumn("t10.BinAbs", "t2.BinAbs"))
                   .LeftJoin("OSRN t11", "t11.AbsEntry", "t10.SnBMDAbs");

                    //ahora los case
                    //Stock
                    query.Case(c =>
                    c.When("t12.BinActivat", "N")
                    .Then(c1 =>
                    c1.When("t0.ManBtchNum", "Y").ThenColumn("t4.Quantity")
                    .When("t0.ManSerNum", "Y").ThenColumn("t6.Quantity")
                    .ElseColumn("t1.OnHand")
                  ).Else(c1 =>
                    c1.When("t0.ManBtchNum", "Y").ThenColumn("t8.OnHandQty")
                    .When("t0.ManSerNum", "Y").ThenColumn("t10.OnHandQty")
                    .ElseColumn("t2.OnHandQty")
                  ).As("Quantity")
                    );

                    //DistNumber
                    query.Case(c =>
                    c.When("t12.BinActivat", "N")
                    .Then(c1 =>
                    c1.When("t0.ManBtchNum", "Y").ThenColumn("t5.DistNumber")
                    .When("t0.ManSerNum", "Y").ThenColumn("t7.DistNumber")
                    ).Else(c1 =>
                    c1.When("t0.ManBtchNum", "Y").ThenColumn("t9.DistNumber")
                    .When("t0.ManSerNum", "Y").ThenColumn("t11.DistNumber")
                    ).As("DistNumber"));

                    if (!MostrarArticulosSinStock)
                    {
                        query.Where(q =>
               q.Where(q2 =>
               q2.Where("t12.BinActivat", "N")
               .Where(q3 =>
               q3.Where("t0.ManBtchNum", "N")
               .Where("t0.ManSerNum", "N")
               .Where("t1.OnHand", ">", 0))
               .OrWhere(q3 =>
                q3.Where("t0.ManBtchNum", "Y")
                .Where("t4.Quantity", ">", 0))
               .OrWhere(q3 =>
               q3.Where("t0.ManSerNum", "Y")
               .Where("t6.Quantity", ">", 0))
               ).OrWhere(q2 =>
               q2.Where("t12.BinActivat", "Y")
               .Where(q3 =>
               q3.Where("t0.ManBtchNum", "N")
               .Where("t0.ManSerNum", "N")
               .Where("t2.OnHandQty", ">", 0))
               .OrWhere(q3 =>
                q3.Where("t0.ManBtchNum", "Y")
                .Where("t8.OnHandQty", ">", 0))
               .OrWhere(q3 =>
               q3.Where("t0.ManSerNum", "Y")
               .Where("t10.OnHandQty", ">", 0))
               ));
                    }

                }
                else
                {
                    //Stock
                    query.Case(c =>
                    c.When("t12.BinActivat", "N")
                    .ThenColumn("t1.OnHand")
                    .ElseColumn("t2.OnHandQty")
                    .As("Quantity")
                    );
                    if (!MostrarArticulosSinStock)
                    {
                        query.Where(q =>
                        q.Where(q2 =>
                        q2.Where("t12.BinActivat", "N")
                        .Where("t1.OnHand", ">", 0)
                        ).OrWhere(q2 =>
                        q2.Where("t12.BinActivat", "Y")
                        .Where("t2.OnHandQty", ">", 0))
                        );
                    }

                }
            }
            else
            {
                if (MostrarStockBasadoEnSeriesLotes)
                {
                    query.LeftJoin("OBTQ t4", "t4.ItemCode", "t1.ItemCode", q => q.WhereColumn("t4.WhsCode", "t1.WhsCode").Where("t12.BinActivat", "N"))
                   .LeftJoin("OBTN t5", "t5.AbsEntry", "t4.MdAbsEntry")
                   .LeftJoin("OSRQ t6", "t6.ItemCode", "t1.ItemCode", q => q.WhereColumn("t6.WhsCode", "t1.WhsCode").Where("t12.BinActivat", "N"))
                   .LeftJoin("OSRN t7", "t7.AbsEntry", "t6.MdAbsEntry");

                    //Stock
                    query.Case(c =>
                    c.When("t0.ManBtchNum", "Y").ThenColumn("t4.Quantity")
                    .When("t0.ManSerNum", "Y").ThenColumn("t6.Quantity")
                    .ElseColumn("t1.OnHand").As("Quantity")
                    );

                    //DistNumber
                    query.Case(c =>
                    c.When("t0.ManBtchNum", "Y").ThenColumn("t5.DistNumber")
                    .When("t0.ManSerNum", "Y").ThenColumn("t7.DistNumber")
                    .As("DistNumber"));

                    if (!MostrarArticulosSinStock)
                    {
                        query.Where(q =>
               q.Where(q2 =>
               q2.Where("t12.BinActivat", "N")
               .Where(q3 =>
               q3.Where("t0.ManBtchNum", "N")
               .Where("t0.ManSerNum", "N")
               .Where("t1.OnHand", ">", 0))
               .OrWhere(q3 =>
                q3.Where("t0.ManBtchNum", "Y")
                .Where("t4.Quantity", ">", 0))
               .OrWhere(q3 =>
               q3.Where("t0.ManSerNum", "Y")
               .Where("t6.Quantity", ">", 0))
               ));
                    }

                }
                else
                {
                    //Stock
                    query.AddSelect("t1.OnHand as Quantity");


                    if (!MostrarArticulosSinStock)
                    {
                        query.Where("t1.OnHand", ">", 0);
                    }
                }
            }
            //Filtrar en base a parámetros
            if (!string.IsNullOrEmpty(Ubicacion) && StockConUbicaciones)
            {
                if (Ubicacion.Contains(','))
                {
                    var ubicacion = Ubicacion.Trim();
                    ubicacion = ubicacion.TrimEnd(',');
                    var ubis = ubicacion.Split(',');
                    query.WhereIn("t3.BinCode", ubis);
                }
                else
                    query.Where("t3.BinCode", Ubicacion);
            }

            if (!string.IsNullOrEmpty(Almacen))
            {
                if (Almacen.Contains(','))
                {
                    var almacen = Almacen.Trim();
                    almacen = almacen.TrimEnd(',');
                    var alms = almacen.Split(',');
                    query.WhereIn("t1.WhsCode", alms);
                }
                else
                    query.Where("t1.WhsCode", Almacen);
            }

            if (!string.IsNullOrEmpty(Articulo))
            {
                if (Articulo.Contains(','))
                {
                    var itemCode = Articulo.Trim();
                    itemCode = itemCode.TrimEnd(',');
                    var items = itemCode.Split(',');
                    query.WhereIn("t0.ItemCode", items);
                }
                else
                    query.Where("t0.ItemCode", Articulo);
            }

            if (!string.IsNullOrWhiteSpace(SerieLote) && MostrarStockBasadoEnSeriesLotes)
            {
                var serieLote = SerieLote;
                if (StockConUbicaciones)
                {
                    if (SerieLote.Contains(","))
                    {
                        serieLote = serieLote.Trim().TrimEnd(',');
                        var lotes = serieLote.Split(',');
                        query.Where(q =>
                        q.WhereIn("t5.DistNumber", lotes)
                        .OrWhereIn("t7.DistNumber", lotes)
                        .OrWhereIn("t9.DistNumber", lotes)
                        .OrWhereIn("t11.DistNumber", lotes)
                        );
                    }
                    else
                    {
                        query.Where(q =>
                       q.Where("t5.DistNumber", serieLote)
                       .OrWhere("t7.DistNumber", serieLote)
                       .OrWhere("t9.DistNumber", serieLote)
                       .OrWhere("t11.DistNumber", serieLote)
                       );
                    }
                }
                else
                {
                    if (SerieLote.Contains(","))
                    {
                        serieLote = serieLote.Trim().TrimEnd(',');
                        var lotes = serieLote.Split(',');
                        query.Where(q =>
                        q.WhereIn("t5.DistNumber", lotes)
                        .OrWhereIn("t7.DistNumber", lotes)
                        );
                    }
                    else
                    {
                        query.Where(q =>
                       q.Where("t5.DistNumber", serieLote)
                       .OrWhere("t7.DistNumber", serieLote)
                       );
                    }
                }

            }

            if (MostrarArticulosGestionadosPorSeries && MostrarArticulosGestionadosPorLotes && !MostrarArticulosSinGestion)
                query.Where(q =>
                q.OrWhere("t0.ManSerNum", "Y").OrWhere("t0.ManBtchNum", "Y"));
            else if (MostrarArticulosGestionadosPorSeries && !MostrarArticulosGestionadosPorLotes && !MostrarArticulosSinGestion)
                query.Where("t0.ManSerNum", "Y").Where("t0.ManBtchNum", "N");
            else if (!MostrarArticulosGestionadosPorSeries && MostrarArticulosGestionadosPorLotes && !MostrarArticulosSinGestion)
                query.Where("t0.ManBtchNum", "Y").Where("t0.ManSerNum", "N");
            else if (!MostrarArticulosGestionadosPorSeries && !MostrarArticulosGestionadosPorLotes && MostrarArticulosSinGestion)
                query.Where("t0.ManBtchNum", "N").Where("t0.ManSerNum", "N");
            else if (MostrarArticulosGestionadosPorSeries && !MostrarArticulosGestionadosPorLotes && MostrarArticulosSinGestion)
                query.Where("t0.ManBtchNum", "N");
            else if (!MostrarArticulosGestionadosPorSeries && MostrarArticulosGestionadosPorLotes && MostrarArticulosSinGestion)
                query.Where("t0.ManSerNum", "N");


            return query;
        }
    }
}


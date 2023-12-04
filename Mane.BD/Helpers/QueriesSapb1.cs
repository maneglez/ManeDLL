using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mane.BD;
using Mane.BD.Forms;

namespace Mane.BD.Helpers
{
    public static class QueriesSapb1
    {
        public const string SnbTypeSerie = "10000045",
         SnbTypeLote = "10000044",
         SnbTypeNinguno = "4";
        public const string TipoManejoSerie = "S",
            TipoManejoLote = "L",
            TipoManejoNinguno = "N";
        /// <summary>
        /// Obtiene las ubicaciones utilizadas en un documento de sap
        /// </summary>
        /// <param name="docEntry">ID de documento</param>
        /// <returns>ItemCode,DocLineNum,Quantity,BinAbs,BinCode,WhsCode,SysNumber,DistNum</returns>
        public static QueryBuilder UbicacionesDeDocumento(int docEntry,int objType)
        {
            return Bd.Query("OBTL t0")
                .Join("OILM t1", "t1.MessageID", "t0.MessageID")
                .Join("OBIN t2", "t2.AbsEntry", "t0.BinAbs")
                .LeftJoin("OBTN t3", "t3.AbsEntry", "t0.SnBMDAbs", q => q.Where("t1.SnBType", SnbTypeLote))
                .LeftJoin("OSRN t4", "t4.AbsEntry", "t0.SnBMDAbs", q => q.Where("t1.SnBType", SnbTypeSerie))
                .Where("t1.TransType", objType)
                .Where("t1.DocEntry", docEntry)
                .Select("t1.ItemCode", "t1.DocLineNum", "t0.Quantity", "t0.BinAbs", "t2.BinCode","t2.WhsCode")

                .Case(c => c.When("t1.SnBType", SnbTypeLote).Then(TipoManejoLote)
                .When("t1.SnBType", SnbTypeSerie).Then(TipoManejoSerie)
                .Else(TipoManejoNinguno).As("TipoManejo"))

                .Case(c => c.When("t1.SnBType", SnbTypeLote).ThenColumn("t3.SysNumber")
                .When("t1.SnBType", SnbTypeSerie).ThenColumn("t4.SysNumber")
                .Else(0).As("SysNumber"))

                .Case(c => c.When("t1.SnBType", SnbTypeLote).ThenColumn("t3.DistNumber")
                .When("t1.SnBType", SnbTypeSerie).Then("t4.DistNumber")
                .Else("").As("DistNumber"));
        }

        /// <summary>
        /// Obtiene las series/lotes de los artículos que pertenecen a un documento
        /// </summary>
        /// <param name="docEntry">docEntry del documento</param>
        /// <returns>ItemCode,Quantity,DocLine,DistNumber,SysNumber,TipoManejo</returns>
        [Obsolete("Utilizar los métodos SeriesLotesDocumento[Con/Sin]Ubicacion()")]
        public static QueryBuilder SeriesLotesDeDocumento(int docEntry, int tipoDoc)
        {
            return SeriesLotesDeDocumento(tipoDoc).Where("t0.DocEntry", docEntry);
        }
        public static QueryBuilder SeriesLotesDeDocumento(object[] docEntrys, int tipoDoc)
        {
            return SeriesLotesDeDocumento(tipoDoc).WhereIn("t0.DocEntry", docEntrys);
        }

        /// <summary>
        /// Consulta las series y lotes de un movimiento sin información de ubicaciones
        /// Quantity Mayor que 0 Destino
        /// Quantity Menor que 0 Origen
        /// </summary>
        /// <param name="tipoDoc"></param>
        /// <param name="docEntry"></param>
        /// <returns>"t0.ApplyLine", "t0.LocCode", "t0.ItemCode", "t0.Quantity",SysNumber,DistNumber</returns>
        public static QueryBuilder SeriesLotesDocumentoSinUbicacion(int tipoDoc, int docEntry = -1)
        {
            var query = Bd.Query("B1_InvPostListILWithoutBinView t0")
               .LeftJoin("OBTN t3", "t3.AbsEntry", "t0.MdAbsEntry", q => q.Where("t0.ManagedBy", SnbTypeLote))
               .LeftJoin("OSRN t4", "t4.AbsEntry", "t0.MdAbsEntry", q => q.Where("t0.ManagedBy", SnbTypeSerie))
                .Join("OITM t5", "t0.ItemCode", "t5.ItemCode")
               .Case(c => c.When("t0.ManagedBy", SnbTypeLote).Then(TipoManejoLote)
               .When("t0.ManagedBy", SnbTypeSerie).Then(TipoManejoSerie)
               .Else(TipoManejoNinguno).As("TipoManejo"))

               .Case(c => c.When("t0.ManagedBy", SnbTypeLote).ThenColumn("t3.SysNumber")
               .When("t0.ManagedBy", SnbTypeSerie).ThenColumn("t4.SysNumber")
               .Else(0).As("SysNumber"))

               .Case(c => c.When("t0.ManagedBy", SnbTypeLote).ThenColumn("t3.DistNumber")
               .When("t0.ManagedBy", SnbTypeSerie).ThenColumn("t4.DistNumber")
               .Else("").As("DistNumber"))
               .Where("t0.ApplyType", (int)tipoDoc)
               .Select("t0.ApplyLine", "t0.LocCode", "t0.ItemCode","t5.ItemName", "t0.Quantity");
            if (docEntry > 0)
                query.Where("t0.ApplyEntry", docEntry);

            return query;
        }

        /// <summary>
        /// Consulta las series y lotes de un documento
        /// DocAction = 1  Destino
        /// DocAction = 2  Origen
        /// </summary>
        /// <param name="tipoDoc"></param>
        /// <param name="docEntry"></param>
        /// <returns>"t0.ApplyLine", "t0.LocCode", "t0.ItemCode", "t1.Quantity", "t0.BinAbs", "t2.BinCode", "t1.DocAction"</returns>
        public static QueryBuilder SeriesLotesDocumentoConUbicacion(int tipoDoc,int docEntry = -1)
        {
            
            var query = Bd.Query("B1_InvPostListILWithBinView t0")
                .Join("OILM t1", "t1.MessageID", "t0.MessageID")
                .Join("OBIN t2", "t2.AbsEntry", "t0.BinAbs")
                .LeftJoin("OBTN t3", "t3.AbsEntry", "t0.MdAbsEntry", q => q.Where("t0.ManagedBy", SnbTypeLote))
                .LeftJoin("OSRN t4", "t4.AbsEntry", "t0.MdAbsEntry", q => q.Where("t0.ManagedBy", SnbTypeSerie))
                .Join("OITM t5","t0.ItemCode","t5.ItemCode")
                .Case(c => c.When("t0.ManagedBy", SnbTypeLote).Then(TipoManejoLote)
                .When("t0.ManagedBy", SnbTypeSerie).Then(TipoManejoSerie)
                .Else(TipoManejoNinguno).As("TipoManejo"))

                .Case(c => c.When("t0.ManagedBy", SnbTypeLote).ThenColumn("t3.SysNumber")
                .When("t0.ManagedBy", SnbTypeSerie).ThenColumn("t4.SysNumber")
                .Else(0).As("SysNumber"))

                .Case(c => c.When("t0.ManagedBy", SnbTypeLote).ThenColumn("t3.DistNumber")
                .When("t0.ManagedBy", SnbTypeSerie).ThenColumn("t4.DistNumber")
                .Else("").As("DistNumber"))
                .Where("t0.ApplyType", (int)tipoDoc)
                .Select("t0.ApplyLine", "t0.LocCode", "t0.ItemCode","t5.ItemName", "t1.Quantity", "t0.BinAbs", "t2.BinCode", "t1.DocAction");
            if (docEntry > 0)
                query.Where("t0.ApplyEntry", docEntry);

            return query;


        }

        /// <summary>
        /// Obtiene las series/lotes de los artículos que pertenecen a un documento
        /// </summary>
        /// <param name="tipoDoc">Indica el ObjectId del Documento</param>
        /// <returns>ItemCode,Quantity,DocLine,DistNumber,SysNumber,TipoManejo</returns>
        public static QueryBuilder SeriesLotesDeDocumento(int tipoDoc)
        {
            //Quantity < 0 =
            return Bd.Query("OITL t0")
                    .Join("ITL1 t1", "t1.LogEntry", "t0.LogEntry")
                    .LeftJoin("OBTN t2", "t2.AbsEntry", "t1.MdAbsEntry", q => q.Where("t0.ManagedBy", SnbTypeLote))
                    .LeftJoin("OSRN t3", "t3.AbsEntry", "t1.MdAbsEntry", q => q.Where("t0.ManagedBy", SnbTypeSerie))
                    .Where("t0.ManagedBy", "!=", SnbTypeNinguno)
                    .Select("t0.DocLine", "t1.Quantity * -1 as Quantity", "t0.ItemCode")
                    .Case(c => c.When("t0.ManagedBy", SnbTypeLote).ThenColumn("t2.SysNumber")
                    .ElseColumn("t3.SysNumber").As("SysNumber"))
                    .Case(c => c.When("t0.ManagedBy", SnbTypeLote).ThenColumn("t2.DistNumber")
                    .ElseColumn("t3.DistNumber").As("DistNumber"))
                    .Case(c => c.When("t0.ManagedBy", SnbTypeLote).Then(TipoManejoLote)
                    .Else(TipoManejoSerie).As("TipoManejo"))
                    .Where("t0.DocType", tipoDoc);
        }

        public static bool TieneUbicacionesActivas(string almacen,string conexion = "")
        {
            var value = Bd.Query("OWHS").Select("BinActivat")
                 .Where("WhsCode", almacen)
                 .GetScalar(conexion);
            if (value == null) return false;
            return value.ToString() == "Y";
        }
        public static bool UbicacionExiste(string ubicacion, string conexion = "")
        {
            return Bd.Query("OBIN").Select("BinCode")
                 .Where("BinCode", ubicacion)
                 .Exists(conexion);
        }
      
        /// <summary>
        /// Obtiene el AbsEntry de una ubicación dada
        /// </summary>
        /// <param name="binCode">código de ubicación</param>
        /// <param name="conexion">nombre conexion</param>
        /// <returns>OBIN.AbsEntry</returns>
        public static int GetBinEntry(string binCode,string conexion = "")
        {
            return Convert.ToInt32(Bd.Query("OBIN").Select("AbsEntry").Where("BinCode", binCode).GetScalar(conexion,0));
        }

        /// <summary>
        /// Obtiene el tipo de manejo de un artículo
        /// </summary>
        /// <param name="itemCode">Articulo</param>
        /// <param name="nombreConexion">conexion</param>
        /// <returns></returns>
        public static string TipoDeManejo(string itemCode,string nombreConexion = "")
        {
            var dt = Bd.Query("OITM").Select("ManSerNum", "ManBtchNum")
                .Where("ItemCode", itemCode).Get(nombreConexion);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ManBtchNum"].ToString() == "Y")
                    return TipoManejoLote;
                if (dt.Rows[0]["ManSerNum"].ToString() == "Y")
                    return TipoManejoSerie;
            }
            return TipoManejoNinguno;
        }

        /// <summary>
        /// Devuele el stock general, con/sin ubicaciones, con/sin series/lotes
        /// </summary>
        /// <param name="ubicacion">ubicaciones separadas por coma</param>
        /// <param name="almacen">alacenes separados por coma</param>
        /// <param name="itemCode">articulos separados por coma</param>
        /// <param name="serieLote">series lotes separados por coma</param>
        /// <param name="mostrarStockVacio">indica si se deben de mostrar los artículos sin stock</param>
        /// <returns>t12.WhsCode, t3.BinCode, t2.BinAbs, t0.ItemCode, t0.ItemName,Quantity,SysNumber,DistNumber,InDate,ExpDate,TipoManejo</returns>
        public static QueryBuilder Stock(string ubicacion = "", string almacen = "", string itemCode = "", string serieLote = "", bool mostrarStockVacio = false)
        {
            var query = Bd.Query("OITM t0")
                .SelectDistinct("t12.WhsCode", "t3.BinCode", "t2.BinAbs", "t0.ItemCode", "t0.ItemName")
                .Join("OITW t1", "t1.ItemCode", "t0.ItemCode")
                .LeftJoin("OIBQ t2", "t2.ItemCode", "t1.ItemCode", q => q.WhereColumn("t2.WhsCode", "t1.WhsCode"))
                .LeftJoin("OBIN t3", "t3.AbsEntry", "t2.BinAbs")
                .LeftJoin("OBTQ t4", "t4.ItemCode", "t1.ItemCode", q => q.WhereColumn("t4.WhsCode", "t1.WhsCode").WhereIsNull("t3.BinCode"))
                .LeftJoin("OBTN t5", "t5.AbsEntry", "t4.MdAbsEntry")
                .LeftJoin("OSRQ t6", "t6.ItemCode", "t1.ItemCode", q => q.WhereColumn("t6.WhsCode", "t1.WhsCode").WhereIsNull("t3.BinCode"))
                .LeftJoin("OSRN t7", "t7.AbsEntry", "t6.MdAbsEntry")
                .LeftJoin("OBBQ t8", "t8.ItemCode", "t1.ItemCode", q => q.WhereColumn("t8.BinAbs", "t2.BinAbs"))
                .LeftJoin("OBTN t9", "t9.AbsEntry", "t8.SnBMDAbs")
                .LeftJoin("OSBQ t10", "t10.ItemCode", "t1.ItemCode", q => q.WhereColumn("t10.BinAbs", "t2.BinAbs"))
                .LeftJoin("OSRN t11", "t11.AbsEntry", "t10.SnBMDAbs")
                .Join("OWHS t12", "t12.WhsCode", "t1.WhsCode");
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
            //SysNumber
            query.Case(c =>
            c.When("t12.BinActivat", "N")
            .Then(c1 =>
            c1.When("t0.ManBtchNum", "Y").ThenColumn("t5.SysNumber")
            .When("t0.ManSerNum", "Y").ThenColumn("t7.SysNumber")
            ).Else(c1 =>
            c1.When("t0.ManBtchNum", "Y").ThenColumn("t9.SysNumber")
            .When("t0.ManSerNum", "Y").ThenColumn("t11.SysNumber")
            ).As("SysNumber")
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

            //InDate
            query.Case(c =>
            c.When("t12.BinActivat", "N")
            .Then(c1 =>
            c1.When("t0.ManBtchNum", "Y").ThenColumn("t5.InDate")
            .When("t0.ManSerNum", "Y").ThenColumn("t7.InDate")
            ).Else(c1 =>
            c1.When("t0.ManBtchNum", "Y").ThenColumn("t9.InDate")
            .When("t0.ManSerNum", "Y").ThenColumn("t11.InDate")
            ).As("InDate"));

            //ExpDate
            query.Case(c =>
            c.When("t12.BinActivat", "N")
            .Then(c1 =>
            c1.When("t0.ManBtchNum", "Y").ThenColumn("t5.ExpDate")
            .When("t0.ManSerNum", "Y").ThenColumn("t7.ExpDate")
            ).Else(c1 =>
            c1.When("t0.ManBtchNum", "Y").ThenColumn("t9.ExpDate")
            .When("t0.ManSerNum", "Y").ThenColumn("t11.ExpDate")
            ).As("ExpDate"));

            //Tipo de Manejo
            query.Case(c =>
            c.When("t0.ManBtchNum", "Y").Then(TipoManejoLote)
             .When("t0.ManSerNum", "Y").Then(TipoManejoSerie)
             .Else(TipoManejoNinguno).As("TipoManejo"));

            //Filtrar en base a parámetros
            if (!string.IsNullOrEmpty(ubicacion))
            {
                if (ubicacion.Contains(','))
                {
                    ubicacion = ubicacion.Trim();
                    ubicacion = ubicacion.TrimEnd(',');
                    var ubis = ubicacion.Split(',');
                    query.WhereIn("t3.BinCode", ubis);
                }
                else
                    query.Where("t3.BinCode", ubicacion);
            }

            if (!string.IsNullOrEmpty(almacen))
            {
                if (almacen.Contains(','))
                {
                    almacen = almacen.Trim();
                    almacen = almacen.TrimEnd(',');
                    var alms = almacen.Split(',');
                    query.OrWhereIn("t1.WhsCode", alms);
                }
                else
                    query.Where("t1.WhsCode", almacen);
            }

            if (!string.IsNullOrEmpty(itemCode))
            {
                if (itemCode.Contains(','))
                {
                    itemCode = itemCode.Trim();
                    itemCode = itemCode.TrimEnd(',');
                    var items = itemCode.Split(',');
                    query.WhereIn("t0.ItemCode", items);
                }
                else
                    query.Where("t0.ItemCode", itemCode);
            }
            if (!mostrarStockVacio)
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

            if (!string.IsNullOrWhiteSpace(serieLote))
            {
                if (serieLote.Contains(","))
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
            return query;
        }

        public static bool BuscarArticulo(out string itemCode, string busqueda = "",string nombreConexion = "")
        {
            itemCode = "";
            if(!string.IsNullOrWhiteSpace(busqueda))
            if (Bd.Query("OITM").Where("ItemCode", busqueda).Where("validFor", "Y").Exists(nombreConexion))
            {
                itemCode = busqueda;
                return true;
            }
            var retVal = false;
            var query = Bd.Query("OITM").Where("validFor", "Y");
            var dic = new Dictionary<string, string>
            {
                {"ItemCode","Artículo" },
                { "ItemName","Nombre" }
            };
            using (var fm = new SeleccionarGenerico(query,nombreConexion,dic))
            {
                fm.Busqueda = busqueda;
                if(fm.ShowDialog() == DialogResult.OK)
                {
                    itemCode = fm.SelectedRow["ItemCode"].ToString();
                    retVal = true;
                }
            }
            return retVal;
        }

        public static bool BuscarUbicacion(out string binCode,string busqueda = "",string almacen = "",string nombreConexion = "")
        {
            binCode = "";
            if(!string.IsNullOrWhiteSpace(busqueda))
                if (Bd.Query("OBIN").Where("BinCode", busqueda).Exists(nombreConexion))
                {
                    binCode = busqueda;
                    return true;
                }
            var result = false;
            var query = Bd.Query();
            query.From("OBIN")
                .Select("BinCode", "WhsCode", "AbsEntry")
                .Where("Deleted", "N");
            if (!string.IsNullOrWhiteSpace(almacen))
                query.Where("WhsCode", almacen);
            var dic = new Dictionary<string, string>();
            dic.Add("BinCode", "Ubicacion");
            dic.Add("WhsCode", "Almacen");
            dic.Add("AbsEntry", "ID Ubicacion");
            using (var fm = new Mane.BD.Forms.SeleccionarGenerico(query,nombreConexion,dic))
            {
                fm.Busqueda = busqueda;
                fm.Text = "Seleccione una ubicación";
                if (fm.ShowDialog() == DialogResult.OK)
                {
                    binCode = fm.SelectedRow["BinCode"]?.ToString();
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Obtiene las unidades de medida para un determinado artículo
        /// </summary>
        /// <param name="itemCode">Artículo</param>
        /// <returns>t0.UgpEntry,"t3.UomCode", "t3.UomName", "t2.BaseQty", "t2.AltQty"</returns>
        public static QueryBuilder UnidadesDeMedidaArticulo(string itemCode)
        {
            return Bd.Query("OITM t0")
                  .Join("OUGP t1", "t1.UgpEntry", "t0.UgpEntry")
                  .Join("UGP1 t2", "t2.UgpEntry", "t1.UgpEntry")
                  .Join("OUOM t3", "t3.UomEntry", "t2.UomEntry")
                  .Select("t3.UomCode", "t3.UomName", "t2.BaseQty", "t2.AltQty", "t0.UgpEntry")
                  .Where("t0.ItemCode", itemCode);
                  //.Where("t2.IsActive", "Y");
        }
        /// <summary>
        /// Obtiene las unidades de medida para un determinado artículo
        /// </summary>
        /// <param name="itemCode">Artículo</param>
        /// <returns>t0.ItemCode,"t3.UomCode", "t3.UomName", "t2.BaseQty", "t2.AltQty",UgpEntry</returns>
        public static QueryBuilder UnidadesDeMedidaArticulo(object[] itemCodes)
        {
            return Bd.Query("OITM t0")
                  .Join("OUGP t1", "t1.UgpEntry", "t0.UgpEntry")
                  .Join("UGP1 t2", "t2.UgpEntry", "t1.UgpEntry")
                  .Join("OUOM t3", "t3.UomEntry", "t2.UomEntry")
                  .Select("t0.ItemCode", "t3.UomCode", "t3.UomName", "t2.BaseQty", "t2.AltQty", "t0.UgpEntry")
                  .WhereIn("t0.ItemCode", itemCodes);
                  //.Where("t2.IsActive", "Y");
        }

        /// <summary>
        /// Join en tablas para obtener articulos de lista de materiales
        /// </summary>
        /// <param name="articuloPadre"></param>
        /// <returns>OITT t0, ITT1 t1, OITM t2</returns>
        public static QueryBuilder ArticulosDeListaDeMateriales(string articuloPadre)
        {
            return Bd.Query("OITT t0")
                .Join("ITT1 t1", "t1.Father", "t0.Code")
                .Join("OITM t2", "t2.ItemCode", "t1.Code")
                .Where("t0.Code", articuloPadre);
        }

        /// <summary>
        /// Obtiene los valores válidos para un campo de usuario y tabla dados
        /// </summary>
        /// <param name="campoDeUsuario">nombre del campo de usuario, puede ser con o sin el prefijo U_</param>
        /// <param name="tabla">Nombre de la tabla por ejemplo: ORDR,OINV,OPCH,OCRD</param>
        /// <returns>Consulta con los valores válidos, Columnas: Code,Name </returns>
        public static QueryBuilder ValoresValidosCampoDeUsuario(string campoDeUsuario,string tabla)
        {
            return Bd.Query("CUFD t0")
                .Join("UFD1 t1", "t1.FieldID", "t0.FieldID", q => q.WhereColumn("t1.TableID", "t0.TableID"))
                .Where("t0.TableID", tabla)
                .Where("t0.AliasID", campoDeUsuario.StartsWith("U_") ? campoDeUsuario.Substring(2, campoDeUsuario.Length - 2) : campoDeUsuario)
                .Select("t1.FldValue as Code", "t1.Descr as Name");
        }
    }
}

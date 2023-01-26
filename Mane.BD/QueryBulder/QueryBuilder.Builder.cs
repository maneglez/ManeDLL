using Mane.BD.QueryBulder.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD
{
    public partial class QueryBuilder : IBuilder
    {
         char[] IBuilder.ColumnDelimiters { get; set; }
         char[] IBuilder.ValueDelimiters { get; set; }
        QueryBuilder IBuilder.QueryBuilder { get; set; }

         string IBuilder.SelectLastInsertedIndexQuery => throw new NotImplementedException();

        string IBuilder.BuildGroupBy()
        {
            return GetBuilder()?.BuildGroupBy();
        }

         string IBuilder.BuildJoins()
        {
            return GetBuilder()?.BuildJoins();
        }

         string IBuilder.BuildLimit()
        {
            return GetBuilder()?.BuildLimit();
        }

         string IBuilder.BuildOrderBy()
        {
            return GetBuilder()?.BuildOrderBy();
        }

         string IBuilder.BuildQuery()
        {
            return GetBuilder()?.BuildQuery();
        }
        internal string BuildQuery(TipoDeBd tipo)
        {
            return GetBuilder(tipo)?.BuildQuery();
        }

         string IBuilder.BuildSelect()
        {
            return GetBuilder()?.BuildSelect();
        }

         string IBuilder.BuildWeres()
        {
            return GetBuilder()?.BuildWeres();
        }

         string IBuilder.Count()
        {
            return GetBuilder()?.Count();
        }

         string IBuilder.Delete()
        {
            return GetBuilder()?.Delete();
        }

         string IBuilder.FormatColumn(string columna)
        {
            return GetBuilder()?.FormatColumn(columna);
        }

         string IBuilder.FormatTable(string value)
        {
            return GetBuilder()?.FormatTable(value);
        }

         string IBuilder.FormatValue(object value)
        {
            return GetBuilder()?.FormatValue(value);
        }

         string IBuilder.Insert(object objeto)
        {
            return GetBuilder()?.Insert(objeto);
        }

         string IBuilder.Insert(Dictionary<string, object> diccionario)
        {
            return GetBuilder()?.Insert(diccionario);
        }

         string IBuilder.Update(Dictionary<string, object> diccionario)
        {
          return  GetBuilder().Update(diccionario);
        }
        private IBuilder GetBuilder(string NombreConexion = null)
        {
            if (NombreConexion != null)
                this.NombreConexion = NombreConexion;
            var con = Bd.Conexiones.Find(this.NombreConexion);
            if (con == null) throw new Exception($"La conexion '{NombreConexion}' no existe");
            return GetBuilder(con);
        }
        internal IBuilder GetBuilder(TipoDeBd tipo)
        {
            switch (tipo)
            {
                case TipoDeBd.SqlServer:
                    return new BuilderSQL(this);
                case TipoDeBd.SQLite:
                    return new BuilderSQLite(this);
                case TipoDeBd.Hana:
                    return new BuilderHana(this);
                default:
                    return null;
            }
            
        }
        internal IBuilder GetBuilder(Conexion c)
        {
            switch (c.TipoDeBaseDeDatos)
            {
                case TipoDeBd.SqlServer:
                    return new BuilderSQL(this);
                case TipoDeBd.SQLite:
                    return new BuilderSQLite(this);
                case TipoDeBd.Hana:
                    //Requiere el nombre de la BD
                    return new BuilderHana(this,c.NombreBD);
                case TipoDeBd.ApiWeb:
                    return GetBuilder(c.SubTipoDeBD);
                default:
                    return null;
            }

        }

        string IBuilder.BuildExecProcedure(string ProcedureName, object[] ProcParameters = null)
        {
            return "";
            
        }
    }
}

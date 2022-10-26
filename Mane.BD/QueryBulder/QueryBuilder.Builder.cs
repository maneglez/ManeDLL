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
        public char[] ColumnDelimiters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public char[] ValueDelimiters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        QueryBuilder IBuilder.QueryBuilder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string BuildGroupBy()
        {
            return GetBuilder()?.BuildGroupBy();
        }

        public string BuildJoins()
        {
            return GetBuilder()?.BuildJoins();
        }

        public string BuildLimit()
        {
            return GetBuilder()?.BuildLimit();
        }

        public string BuildOrderBy()
        {
            return GetBuilder()?.BuildOrderBy();
        }

        public string BuildQuery()
        {
            return GetBuilder()?.BuildQuery();
        }
        public string BuildQuery(TipoDeBd tipo)
        {
            return GetBuilder(tipo)?.BuildQuery();
        }

        public string BuildSelect()
        {
            return GetBuilder()?.BuildSelect();
        }

        public string BuildWeres()
        {
            return GetBuilder()?.BuildWeres();
        }

        public string Count()
        {
            return GetBuilder()?.Count();
        }

        public string Delete()
        {
            return GetBuilder()?.Delete();
        }

        public string FormatColumn(string columna)
        {
            return GetBuilder()?.FormatColumn(columna);
        }

        public string FormatTable(string value)
        {
            return GetBuilder()?.FormatTable(value);
        }

        public string FormatValue(object value)
        {
            return GetBuilder()?.FormatValue(value);
        }

        public string Insert(object objeto)
        {
            return GetBuilder()?.Insert(objeto);
        }

        public string Insert(Dictionary<string, object> diccionario)
        {
            return GetBuilder()?.Insert(diccionario);
        }

        public string Update(Dictionary<string, object> diccionario)
        {
          return  GetBuilder().Update(diccionario);
        }
        private IBuilder GetBuilder(string NombreConexion = null)
        {
            if (NombreConexion != null)
                this.NombreConexion = NombreConexion;
            return GetBuilder(Bd.GetTipoBd(this.NombreConexion));
        }
        private IBuilder GetBuilder(TipoDeBd tipo)
        {
            switch (tipo)
            {
                case TipoDeBd.SqlServer:
                    return new BuilderSQL(this);
                case TipoDeBd.SQLite:
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}

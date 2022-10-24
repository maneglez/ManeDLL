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
            throw new NotImplementedException();
        }

        public string BuildJoins()
        {
            throw new NotImplementedException();
        }

        public string BuildLimit()
        {
            throw new NotImplementedException();
        }

        public string BuildOrderBy()
        {
            throw new NotImplementedException();
        }

        public string BuildQuery()
        {
            throw new NotImplementedException();
        }

        public string BuildSelect()
        {
            throw new NotImplementedException();
        }

        public string BuildWeres()
        {
            throw new NotImplementedException();
        }

        public string Count()
        {
            throw new NotImplementedException();
        }

        public string Delete()
        {
            throw new NotImplementedException();
        }

        public string FormatColumn(string columna)
        {
            throw new NotImplementedException();
        }

        public string FormatTable(string value)
        {
            throw new NotImplementedException();
        }

        public string FormatValue(object value)
        {
            throw new NotImplementedException();
        }

        public string Insert(object objeto)
        {
            throw new NotImplementedException();
        }

        public string Insert(Dictionary<string, object> diccionario)
        {
            throw new NotImplementedException();
        }

        public string Update(Dictionary<string, object> diccionario)
        {
            throw new NotImplementedException();
        }
        private IBuilder GetBuilder()
        {
            switch (Bd.getTipoBd(NombreConexion))
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

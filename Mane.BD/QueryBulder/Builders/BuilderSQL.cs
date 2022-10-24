using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD.QueryBulder.Builders
{
    internal class BuilderSQL : IBuilder
    {
        public QueryBuilder QueryBuilder { get; set; }
        public char[] ColumnDelimiters { get; set; }
        public char[] ValueDelimiters { get; set; }

        public BuilderSQL(QueryBuilder queryBuilder)
        {
            QueryBuilder = queryBuilder;
            ColumnDelimiters = new char[] { '[', ']' };
            ValueDelimiters = new char[] { '\'', '\'' };
        }

        public string BuildGroupBy()
        {
            string groupBy = "";
            if (QueryBuilder.GroupBy != null)
            {
                foreach (string col in QueryBuilder.GroupBy)
                {
                    groupBy += FormatColumn(col) + ",";
                }
                groupBy = groupBy.Trim(',');
                groupBy = $"GROUP BY {groupBy}";
            }
            return groupBy;
        }

        public string BuildJoins()
        {
            string joins = "";
            string extraCondicion = "";
            string consulta = "";
            foreach (var j in QueryBuilder.Joins)
            {
                extraCondicion = j.ExtraCondicion != null ? "AND " + j.ExtraCondicion.buildWhereSQL() : "";
                consulta = j.Consulta != null ? $"({j.Consulta.buildQuerySQL()}) {formatearColumnaSQL(j.AliasDeConsulta)}" : "";
                joins += $" {j.Tipo} JOIN {consulta}{FormatTable(j.Tabla)} ON {formatearColumnaSQL(j.Columna1)} {j.Operador} {formatearColumnaSQL(j.Columna2)} {extraCondicion}";
            }
            return joins;
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
            return Common.FormatColumn(columna, ColumnDelimiters);
        }

        public string FormatTable(string value)
        {
            throw new NotImplementedException();
        }

        public string FormatValue(object value)
        {
           return Common.FormatValue(value, ValueDelimiters);
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
    }
}

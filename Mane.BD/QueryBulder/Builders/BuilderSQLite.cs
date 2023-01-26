using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD.QueryBulder.Builders
{
    internal class BuilderSQLite : BuilderSQL
    {
        protected override TipoDeBd Tipo => TipoDeBd.SQLite;
        public override string SelectLastInsertedIndexQuery => "SELECT last_insert_rowid()";
        public BuilderSQLite(QueryBuilder queryBuilder) : base(queryBuilder)
        {
        }
        override public string BuildLimit()
        {
            return QueryBuilder._Limit > 0 ? $"LIMIT {QueryBuilder._Limit}" : "";
        }
        override public string BuildQuery()
        {
            var q = QueryBuilder;
            if (q._RawQuery != null) return q._RawQuery;
            string select = BuildSelect(),
                where = BuildWeres(),
                joins = BuildJoins(),
                limit = BuildLimit(),
                orderBy = BuildOrderBy(),
                groupBy = BuildGroupBy();
            if (where != "") where = "WHERE " + where;
            string query = $"SELECT {select} FROM {FormatTable(q.Tabla)} {joins} {where} {orderBy} {groupBy} {limit}";
            return System.Text.RegularExpressions.Regex.Replace(query, @"\s+", " ");
        }
    }
}

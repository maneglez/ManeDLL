using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD.QueryBulder.Builders
{
    internal class BuilderHana : BuilderSQLite
    {
        public override string SelectLastInsertedIndexQuery => $"select current_identity_value() FROM {FormatTable(QueryBuilder.Tabla)};";
        public BuilderHana(QueryBuilder queryBuilder) : base(queryBuilder)
        {
            ColumnDelimiters = new char[] { '"', '"' };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD.QueryBulder.Builders
{
   internal interface IBuilder
    {
        char[] ColumnDelimiters { get; set; }
        char[] ValueDelimiters { get; set; }
        string SelectLastInsertedIndexQuery { get; }
        QueryBuilder QueryBuilder { get; set; }
        string FormatValue(object value);
        string FormatTable(string value);
        string FormatColumn(string columna);
        string Insert(object objeto);
        string Insert(Dictionary<string, object> diccionario);
        string Update(Dictionary<string, object> diccionario);
        string Delete();
        string BuildWeres();
        string BuildJoins();
        string BuildLimit();
        string BuildSelect();
        string BuildOrderBy();
        string BuildGroupBy();
        string Count();
        string BuildQuery();

    }
}

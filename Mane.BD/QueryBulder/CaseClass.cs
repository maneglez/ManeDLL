using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD.QueryBulder
{
    [Serializable]
    public class CaseClass
    {
        internal List<WhenDataClass> WhenData;
        internal List<ThenDataClass> ThenData;
        internal string Alias;
        internal string CaseColumn;
        internal object ElseValue;
        internal string _ElseColumn;
        internal QueryBuilder ElseQuery;

        private void Construct()
        {
            WhenData = new List<WhenDataClass>();
            ThenData = new List<ThenDataClass>();
        }
        public CaseClass()
        {
            Construct();
        }
        public CaseClass(string caseColumn)
        {
            Construct();
            CaseColumn = caseColumn;
        }
        [Serializable]
        internal class WhenDataClass
        {
            public WhenThenType TipoWhen;
            public string Column { get; set; }
            public string Operador { get; set; }
            public object Value { get; set; }
            public QueryBuilder Query { get; set; }
        }
        [Serializable]
        internal class ThenDataClass
        {
            public WhenThenType TipoThen;
            public string Column { get; set; }
            public object Value { get; set; }
            public QueryBuilder Query { get; set; }

        }
        public CaseClass When(string column,object value)
        {
            return When(column, "=", value);
        }
        public CaseClass When(string column, string operador, object value)
        {
            WhenData.Add(new WhenDataClass
            {
                Column = column,
                Value = value,
                Operador = operador,
                TipoWhen = WhenThenType.Value
            });
            return this;
        }
        public CaseClass When(QueryBuilder query, object value)
        {
            return When(query, "=", value);
        }
        public CaseClass When(QueryBuilder query, string operador,object value )
        {
            WhenData.Add(new WhenDataClass
            {
               Query = query,
                Value = value,
                Operador = operador,
                TipoWhen = WhenThenType.Query
            });
            return this;
        }
        public CaseClass WhenColumn(string column, string column2)
        {
            return WhenColumn(column, "=", column2);
        }
        public CaseClass WhenColumn(string column,string operador, string column2)
        {
            WhenData.Add(new WhenDataClass
            {
                Column = column,
                Value = column2,
                Operador = operador,
                TipoWhen = WhenThenType.Column
            });
            return this;
        }


        public CaseClass ThenColumn(string column)
        {
            ThenData.Add(new ThenDataClass
            {
                Column = column,
                TipoThen = WhenThenType.Column
            });
            return this;
        }
        public CaseClass Then(object value)
        {
            ThenData.Add(new ThenDataClass
            {
                Value = value,
                TipoThen = WhenThenType.Value
            });
            return this;
        }
        public CaseClass Then(QueryBuilder query)
        {
            ThenData.Add(new ThenDataClass
            {
                Query = query,
                TipoThen = WhenThenType.Query
            });
            return this;
        }

        public CaseClass Else(object value)
        {
            ElseValue = value;
            return this;
        }
        public CaseClass Else(QueryBuilder query)
        {
            ElseQuery = query;
            return this;
        }
        public CaseClass ElseColumn(string column)
        {
            _ElseColumn = column;
            return this;
        }

        public CaseClass As(string alias)
        {
            Alias = alias;
            return this;
        }

    }
    internal enum WhenThenType
    {
        Value,
        Column,
        Query
    }
   

}

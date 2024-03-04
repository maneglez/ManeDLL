using System;
using System.Collections.Generic;
using System.Linq;

namespace Mane.BD.QueryBulder
{
    public class CaseClass
    {
        internal List<WhenDataClass> WhenData;
        internal List<ThenDataClass> ThenData;
        internal string Alias;
        internal string CaseColumn;
        internal object ElseValue;
        internal string _ElseColumn;
        internal QueryBuilder ElseQuery;
        internal CaseClass ElseSubCase;

        private void Construct()
        {
            WhenData = new List<WhenDataClass>();
            ThenData = new List<ThenDataClass>();
        }
        public CaseClass Copy()
        {
            var copy = new CaseClass
            {
                Alias = Alias,
                CaseColumn = CaseColumn,
                ElseValue = ElseValue,
                _ElseColumn = _ElseColumn,
                ElseQuery = ElseQuery?.Copy(),
                ElseSubCase = ElseSubCase?.Copy()
            };

            copy.WhenData.AddRange(from w in WhenData select w.Copy());
            copy.ThenData.AddRange(from t in ThenData select t.Copy());
            return copy;
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
        internal class WhenDataClass
        {
            public WhenThenType TipoWhen;
            public string Column { get; set; }
            public string Operador { get; set; }
            public object Value { get; set; }
            public QueryBuilder Query { get; set; }
            public CaseClass SubWhen {get; set;}
            public WhenDataClass Copy()
            {
                return new WhenDataClass
                {
                    TipoWhen = TipoWhen,
                    Column = Column,
                    Operador = Operador,
                    Value = Value,
                    Query = Query?.Copy(),
                    SubWhen = SubWhen?.Copy()
                };
            }
        }
        internal class ThenDataClass
        {
            public WhenThenType TipoThen;
            public string Column { get; set; }
            public object Value { get; set; }
            public QueryBuilder Query { get; set; }
            public CaseClass SubWhen { get; set; }
            public ThenDataClass Copy()
            {
                return new ThenDataClass
                {
                    TipoThen = TipoThen,
                    Column = Column,
                    Value = Value,
                    Query = Query?.Copy(),
                    SubWhen = SubWhen?.Copy()
                };
            }

        }
        public CaseClass When(string column, object value)
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
        public CaseClass When(QueryBuilder query, string operador, object value)
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
        public CaseClass WhenNull(string column)
        {
            WhenData.Add(new WhenDataClass
            {
                Column = column,
                TipoWhen = WhenThenType.Null
            });
            return this;
        } 
        public CaseClass WhenNotNull(string column)
        {
            WhenData.Add(new WhenDataClass
            {
                Column = column,
                TipoWhen = WhenThenType.NotNull
            });
            return this;
        }
        public CaseClass WhenColumn(string column, string column2)
        {
            return WhenColumn(column, "=", column2);
        }
        public CaseClass WhenColumn(string column, string operador, string column2)
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
        public CaseClass Else(Func<CaseClass,CaseClass> subCase)
        {
            ElseSubCase = subCase.Invoke(new CaseClass());
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
        public CaseClass When(Func<CaseClass, CaseClass> subWhere)
        {
            WhenData.Add(new WhenDataClass
            {
                SubWhen = subWhere.Invoke(new CaseClass()),
                TipoWhen = WhenThenType.SubCase
            });
            return this;
        }
        public CaseClass Then(Func<CaseClass, CaseClass> subWhere)
        {
            ThenData.Add(new ThenDataClass
            {
                SubWhen = subWhere.Invoke(new CaseClass()),
                TipoThen = WhenThenType.SubCase
            });
            return this;
        }

    }
    internal enum WhenThenType
    {
        Value,
        Column,
        Query,
        Null,
        NotNull,
        SubCase
    }


}

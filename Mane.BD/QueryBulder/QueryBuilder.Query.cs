using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD
{
    public partial class QueryBuilder : IQuery<QueryBuilder>
    {
        public int Count(string NombreConexion = "")
        {
            return Convert.ToInt32(Bd.ExecuteEscalar(GetBuilder(NombreConexion).Count(), NombreConexion));
        }

        public void Delete(string NombreConexion = "",bool forzar = false)
        {
            if (Wheres.Count == 0 && !forzar) throw new QueryBuilderExeption("No puede ejecutar un delete sin condición");
            if (Tabla == "") throw new QueryBuilderExeption("No se eligió una tabla");
            Bd.ExecuteNonQuery(GetBuilder(NombreConexion).Delete(), NombreConexion);
        }

        public bool Exists(string NombreConexion = "")
        {
            Limit(1);
            return Bd.Exists(GetBuilder(NombreConexion).BuildQuery(), NombreConexion);
        }

        public QueryBuilder From(string tabla)
        {
            this.Tabla = tabla;
            return this;
        }

        public QueryBuilder GroupBy(params string[] columnas)
        {
            _GroupBy = columnas;
            return this;
        }

        public object Insert(Dictionary<string, object> col_values, string NombreConexion = "")
        {
            if (Tabla == "") throw new QueryBuilderExeption("No se eligió una tabla");
            object result = Bd.ExecuteEscalar(GetBuilder(NombreConexion).Insert(col_values), NombreConexion);
            if (result == DBNull.Value || result == null)
                return null;
            else
                return result;
        }

        public object Insert(object col_values, string NombreConexion = "")
        {
            if (Tabla == "") throw new QueryBuilderExeption("No se eligió una tabla");
            object result = Bd.ExecuteEscalar(GetBuilder(NombreConexion).Insert(col_values), NombreConexion);
            if (result == DBNull.Value)
                return "";
            else return result.ToString();

        }

        public QueryBuilder Join(QueryBuilder consulta, string alias, string col1, string col2)
        {
            return CommonJoin(consulta, alias, col1, col2, "=", "INNER");
        }

        public QueryBuilder Join(QueryBuilder consulta, string alias, string col1, string col2, Func<QueryBuilder, QueryBuilder> otrasCondiciones)
        {
           return CommonJoin(consulta, alias, col1, col2, "=", "INNER", otrasCondiciones);
        }

        public QueryBuilder Join(string tabla, string col1, string col2)
        {
            return Join(tabla, col1, col2, "=");
        }

        public QueryBuilder Join(string tabla, string col1, string col2, string operador = "=")
        {
            return CommonJoin(tabla, col1, col2, operador, "INNER");
        }

        public QueryBuilder Join(string tabla, string col1, string col2, Func<QueryBuilder, QueryBuilder> func, string operador = "=")
        {
           return CommonJoin(tabla, col1, col2, func, operador, "INNER");
        }

        public QueryBuilder LeftJoin(string tabla, string col1, string col2, string operador = "=")
        {
            return CommonJoin(tabla, col1, col2, operador, "LEFT");
        }

        public QueryBuilder LeftJoin(string tabla, string col1, string col2, Func<QueryBuilder, QueryBuilder> func, string operador = "=")
        {
            return CommonJoin(tabla, col1, col2, func, operador, "LEFT");
        }

        public QueryBuilder Limit(int limit)
        {
            _Limit = limit;
            return this;
        }

        public QueryBuilder OrderBy(string columna, OrderDireccion orden = OrderDireccion.Asendente)
        {
            Order = new OrderByClass()
            {
                Columna = columna,
                Orden = orden
            };
            return this;
        }

        public QueryBuilder OrWhere(Func<QueryBuilder, QueryBuilder> func)
        {
            return CommonWhere("", "", "OR", func);
        }

        public QueryBuilder OrWhere(string col1, string operador, object Valor)
        {
            return CommonWhere(col1, operador, Valor, "OR", TipoWhere.Where);
        }

        public QueryBuilder OrWhere(string col, object valor)
        {
            return OrWhere(col, "=", valor);
        }

        public QueryBuilder OrWhereBetween(string col1, object valor1, object valor2)
        {
            return CommonWhere(col1, "BETWEEN", new object[] { valor1, valor2 }, "OR", TipoWhere.WhereBetween);
        }

        public QueryBuilder RawQuery(string query)
        {
            _RawQuery = query;
            return this;
        }

        public QueryBuilder RightJoin(string tabla, string col1, string col2, string operador = "=")
        {
            return CommonJoin(tabla, col1, col2, operador, "RIGHT");
        }

        public QueryBuilder RightJoin(string tabla, string col1, string col2, Func<QueryBuilder, QueryBuilder> func, string operador = "=")
        {
            return CommonJoin(tabla, col1, col2, func, operador, "INNER");
        }

        public QueryBuilder Select(Dictionary<string, QueryBuilder> SelectSubquery)
        {
            if (SelectSubquery == null) throw new ArgumentNullException("SelectSubquery");
            foreach (var item in SelectSubquery.Keys)
            {
                this._SelectSubquery.Add(item, SelectSubquery[item]);
            }
            return this;
        }

        public QueryBuilder Select(params string[] columnas)
        {
            Columnas = columnas;
            return this;
        }

        public QueryBuilder Select(QueryBuilder query, string alias)
        {
            if(query == null || string.IsNullOrEmpty(alias)) throw new ArgumentNullException("query");
            _SelectSubquery.Add(alias, query);
            return this;
        }

        public QueryBuilder SelectRaw(string RawQuery, string alias)
        {
            _SelectSubquery.Add(alias, RawQuery);
            return this;
        }

        public int Update(Dictionary<string, object> dic, string NombreConexion = "",bool forzar = false)
        {
            if (dic == null) return 0;
            if (Wheres.Count == 0 && !forzar) throw new QueryBuilderExeption("No puede ejecutar un update sin condición");
            if (Tabla == "") throw new QueryBuilderExeption("No se eligió una tabla");
            return Bd.ExecuteNonQuery(GetBuilder(NombreConexion).Update(dic), NombreConexion);
        }

        public int Update(object objeto, string NombreConexion = "",bool forzar = false)
        {
            return Update(Common.ObjectToKeyValue(objeto), NombreConexion,forzar);
        }

        public QueryBuilder Where(Func<QueryBuilder, QueryBuilder> func)
        {
            return CommonWhere("", "", "AND", func);
        }

        public QueryBuilder Where(string col, Func<QueryBuilder, QueryBuilder> func)
        {
            return Where(col, "=", func);
        }

        public QueryBuilder Where(string col1, object valor)
        {
            return Where(col1, "=", valor);
        }

        public QueryBuilder Where(string col, string operador, Func<QueryBuilder, QueryBuilder> func)
        {
            return CommonWhere(col, operador, "AND", func);
        }

        public QueryBuilder Where(string col1, string operador, object valor)
        {
            return CommonWhere(col1, operador, valor, "AND", TipoWhere.Where);
        }

        public QueryBuilder WhereBetween(string col1, object valor1, object valor2)
        {
            return CommonWhere(col1, "BETWEEN", new object[] { valor1, valor2 }, "AND", TipoWhere.WhereBetween);
        }

        public QueryBuilder WhereColumn(string col1, string col2)
        {
            return WhereColumn(col1, "=", col2);
        }

        public QueryBuilder WhereColumn(string col1, string operador, string col2)
        {
            return CommonWhere(col1, operador, col2, "", TipoWhere.WhereColumn);
        }

        public QueryBuilder WhereIn(string columna, Func<QueryBuilder, QueryBuilder> func)
        {
            var q = func.Invoke(new QueryBuilder());
            return CommonWhere(columna, "IN", q, "AND", TipoWhere.WhereIn);
        }

        public QueryBuilder WhereIn(string columna, object[] valores)
        {
            return CommonWhere(columna, "IN", valores, "AND", TipoWhere.WhereIn);
        }

        public QueryBuilder WhereIsNotNull(string columna)
        {
            return CommonWhere(columna, "IS NOT NULL", null, "AND", TipoWhere.WhereNull);
        }

        public QueryBuilder WhereIsNull(string columna)
        {
            return CommonWhere(columna, "IS NULL", null, "AND", TipoWhere.WhereNull);
        }

        public QueryBuilder WhereNotIn(string columna, Func<QueryBuilder, QueryBuilder> func)
        {
            var q = func.Invoke(new QueryBuilder());
            return CommonWhere(columna, "NOT IN", q, "AND", TipoWhere.WhereIn);
        }

        public QueryBuilder WhereNotIn(string columna, object[] valores)
        {
            return CommonWhere(columna, "NOT IN", valores, "AND", TipoWhere.WhereIn);
        }
    }
}

using System;
using System.Collections.Generic;

namespace Mane.BD.QueryBulder.Builders
{
    internal class BuilderSQL : IBuilder
    {
        protected virtual TipoDeBd Tipo => TipoDeBd.SqlServer;
        public QueryBuilder QueryBuilder { get; set; }
        public char[] ColumnDelimiters { get; set; }
        public char[] ValueDelimiters { get; set; }
        public virtual string SelectLastInsertedIndexQuery => "SELECT SCOPE_IDENTITY()";

        public BuilderSQL(QueryBuilder queryBuilder)
        {
            QueryBuilder = queryBuilder;
            ColumnDelimiters = new char[] { '[', ']' };
            ValueDelimiters = new char[] { '\'', '\'' };
        }

        public string BuildGroupBy()
        {
            string groupBy = "";
            if (QueryBuilder._GroupBy != null)
            {
                foreach (string col in QueryBuilder._GroupBy)
                {
                    groupBy += FormatColumn(col) + $"{Environment.NewLine},";
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
                extraCondicion = j.ExtraCondicion != null ? j.ExtraCondicion.Wheres[0].Boleano + " " + j.ExtraCondicion.GetBuilder(Tipo).BuildWeres() : "";
                consulta = j.Consulta != null ? $"({BuildSubQuery(j.Consulta)}) {FormatColumn(j.AliasDeConsulta)}" : "";
                joins += $" {j.Tipo} JOIN {consulta}{FormatTable(j.Tabla)} ON {FormatColumn(j.Columna1)} {j.Operador} {FormatColumn(j.Columna2)} {extraCondicion}";
                joins += Environment.NewLine;
            }
            return joins;
        }

        virtual public string BuildLimit()
        {
            return QueryBuilder._Limit > 0 ? $"TOP({QueryBuilder._Limit})" : "";
        }

        public virtual string BuildOrderBy()
        {
            string orderBy = "";
            var q = QueryBuilder;

            if (q.Order != null)
            {
                var order = q.Order;
                string orden = order.Orden == OrderDireccion.Asendente ? "ASC" : "DESC";
                orderBy = $"ORDER BY {FormatColumn(order.Columna)} {orden}";
                if (q.Pagination != null)
                {
                    var p = q.Pagination;
                    orderBy += $" OFFSET {(p.Page - 1) * p.Paginate} ROWS";
                    orderBy += $" FETCH NEXT {p.Paginate} ROWS ONLY";
                }
            }
            return orderBy;
        }

        virtual public string BuildQuery()
        {
            var q = QueryBuilder;
            if (q._RawQuery != null) return q._RawQuery;
            string select = BuildSelect(),
                where = BuildWeres(),
                joins = BuildJoins(),
                limit = BuildLimit(),
                orderBy = BuildOrderBy(),
                groupBy = BuildGroupBy(),
                distinct = q._Distinct ? "DISTINCT" : "";
            if (where != "") where = "WHERE " + where;
            string query = $"SELECT {distinct} {limit} {select} FROM {FormatTable(q.Tabla)} {joins} {where} {orderBy} {groupBy}";
            return query;
            // return System.Text.RegularExpressions.Regex.Replace(query, @"\s+", " ");
        }

        public string BuildSelect()
        {
            var q = QueryBuilder;
            string select = "";
            foreach (string item in q.Columnas)
            {
                select += FormatColumn(item) + $"{Environment.NewLine},";
            }
            if (q._SelectSubquery.Count > 0)
            {
                foreach (var key in q._SelectSubquery.Keys)
                {
                    if (q._SelectSubquery[key] is QueryBuilder subquery)
                        select += $"({BuildSubQuery(subquery)})";
                    else select += $"({q._SelectSubquery[key]})";
                    select += $" AS {FormatColumn(key)},";
                }
            }
            if (q._SelectCase.Count > 0)
            {
                foreach (var c in q._SelectCase)
                {
                    if (c.WhenData.Count != c.ThenData.Count)
                        throw new Exception("Select Case: La lista then contiene más o menos elemntos que la lista when");
                    select += "CASE";
                    if (!string.IsNullOrWhiteSpace(c.CaseColumn))
                        select += " " + FormatColumn(c.CaseColumn);
                    int contador = 0;
                    foreach (var w in c.WhenData)
                    {
                        select += Environment.NewLine + " WHEN ";
                        switch (w.TipoWhen)
                        {
                            case WhenThenType.Value:
                                select += $"{FormatColumn(w.Column)} {w.Operador} {FormatValue(w.Value)}" + Environment.NewLine;
                                break;
                            case WhenThenType.Column:
                                select += $"{FormatColumn(w.Column)} {w.Operador} {FormatColumn(w.Value?.ToString())}" + Environment.NewLine;
                                break;
                            case WhenThenType.Query:
                                select += $"({w.Query.GetQuery(Tipo)}) {w.Operador} {FormatValue(w.Value)}" + Environment.NewLine;
                                break;
                            case WhenThenType.Null:
                                select += $"{FormatColumn(w.Column)} IS NULL" + Environment.NewLine;
                                break;
                            default:
                                break;
                        }
                        var t = c.ThenData[contador];
                        select += " THEN ";
                        switch (t.TipoThen)
                        {
                            case WhenThenType.Value:
                                select += FormatValue(t.Value) + Environment.NewLine;
                                break;
                            case WhenThenType.Column:
                                select += FormatColumn(t.Column) + Environment.NewLine;
                                break;
                            case WhenThenType.Query:
                                select += $"({t.Query.GetQuery(Tipo)}){Environment.NewLine}";
                                break;
                            default:
                                break;
                        }
                        contador++;
                    }
                    if (c.ElseQuery != null || c.ElseValue != null || !string.IsNullOrWhiteSpace(c._ElseColumn))
                    {
                        select += " ELSE ";
                        if (!string.IsNullOrWhiteSpace(c._ElseColumn))
                            select += FormatColumn(c._ElseColumn);
                        else if (c.ElseValue != null)
                            select += FormatValue(c.ElseValue);
                        else if (c.ElseQuery != null)
                            select += $"({c.ElseQuery.GetQuery(Tipo)})";
                    }
                    if (string.IsNullOrWhiteSpace(c.Alias)) throw new Exception("Falta el alias para la sentencia case");
                    select += $"{Environment.NewLine} END AS {FormatColumn(c.Alias)}{Environment.NewLine},";
                }
            }
            select = select.Trim(',');
            if (select == "") select = "*";
            return select;
        }

        virtual public string BuildWeres()
        {
            var where = "";
            var qb = QueryBuilder;
            if (qb.Wheres.Count == 0) return where;
            foreach (var w in qb.Wheres)
            {
                where += $" {w.Boleano} {(w.Query == null ? FormatColumn(w.Columna) : $"({BuildSubQuery(w.Query)})")} {w.Operador} ";
                switch (w.Tipo)
                {
                    case QueryBuilder.TipoWhere.WhereIn:
                        string val = "";
                        if (w.Valor.GetType().IsArray)
                        {
                            Array valores = (Array)w.Valor;
                            foreach (var item in valores)
                            {
                                val += $"{FormatValue(item)},";
                            }
                            val = val.Trim(',');
                            val = $"({val})";
                        }
                        else if (w.Valor.GetType() == typeof(QueryBuilder))
                        {
                            var q = (QueryBuilder)w.Valor;
                            val = $"({BuildSubQuery(q)})";
                        }
                        where = where.Trim(' ');
                        where += val;
                        break;
                    case QueryBuilder.TipoWhere.WhereColumn:
                        where += FormatColumn((string)w.Valor);
                        break;
                    case QueryBuilder.TipoWhere.WhereGroup:
                        if (w.Valor is QueryBuilder q1)
                        {
                            if (q1.Columnas.Length == 0) where += $"({q1.GetBuilder(Tipo).BuildWeres()})";
                            else where += $"({q1.GetQuery(Tipo)})";
                        }
                        else if (w.Query != null)
                        {
                            where += FormatValue(w.Valor);
                        }

                        break;
                    case QueryBuilder.TipoWhere.WhereBetween:
                        var vals = (object[])w.Valor;
                        where += $"{FormatValue(vals[0])} AND {FormatValue(vals[1])}";
                        break;
                    case QueryBuilder.TipoWhere.WhereNull:
                        break;
                    default:
                        where += FormatValue(w.Valor);
                        break;
                }
            }
            where = where.Substring(qb.Wheres[0].Boleano.Length + 1);
            return where;
        }

        public string Count()
        {
            return $"SELECT COUNT(*) AS [Count] FROM ({BuildQuery()}) AS [QueryBuilder_Count]";
        }

        public string Delete()
        {
            return $"DELETE FROM {FormatTable(QueryBuilder.Tabla)} WHERE {BuildWeres()}";
        }

        public string FormatColumn(string columna)
        {
            return Common.FormatColumn(columna, ColumnDelimiters);
        }

        public virtual string FormatTable(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return String.Empty;
            return Common.FormatTable(value, ColumnDelimiters);
        }

        public string FormatValue(object value)
        {
            return Common.FormatValue(value, ValueDelimiters);
        }

        public string Insert(object objeto)
        {
            Dictionary<string, object> modelDic = BD.Common.ObjectToKeyValue(objeto);
            return Insert(modelDic);
        }

        public string Insert(Dictionary<string, object> diccionario)
        {
            var q = QueryBuilder;
            if (q.Tabla == null || q.Tabla == "") throw new QueryBuilderExeption("No se ha establecido el nombre de la tabla para el insert");
            string into = "";
            string values = "";
            foreach (string item in diccionario.Keys)
            {
                into += FormatColumn(item) + $"{Environment.NewLine},";
            }
            foreach (object item in diccionario.Values)
            {
                values += FormatValue(item) + $"{Environment.NewLine},";
            }
            into = into.Trim(',');
            values = values.Trim(',');
            return $"INSERT INTO {FormatTable(q.Tabla)}({into}) VALUES ({values}); {SelectLastInsertedIndexQuery}";
        }

        public string Update(Dictionary<string, object> dic)
        {
            string set = "";
            foreach (string key in dic.Keys)
            {
                set += $"{FormatColumn(key)} = {FormatValue(dic[key])},";
            }
            set = set.Trim(',');
            return $"UPDATE {FormatTable(QueryBuilder.Tabla)} SET {set} WHERE {BuildWeres()}";
        }

        public string BuildExecProcedure(string ProcedureName, object[] ProcParameters = null)
        {
            var proc = $"EXEC {ProcedureName}";
            if (ProcParameters == null) return proc;
            foreach (var item in ProcParameters)
            {
                proc += $" '{item}',";
            }
            return proc.TrimEnd(',');
        }
        protected virtual string BuildSubQuery(QueryBuilder q)
        {
            if (string.IsNullOrWhiteSpace(QueryBuilder.NombreConexion))
                return q.BuildQuery(Tipo);
            else return q.GetBuilder(QueryBuilder.NombreConexion).BuildQuery();
        }
    }
}

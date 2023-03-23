﻿using System;
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
                extraCondicion = j.ExtraCondicion != null ? "AND " + j.ExtraCondicion.GetBuilder(Tipo).BuildWeres() : "";
                consulta = j.Consulta != null ? $"({(j.Consulta as IBuilder).BuildQuery()}) {FormatColumn(j.AliasDeConsulta)}" : "";
                joins += $" {j.Tipo} JOIN {consulta}{FormatTable(j.Tabla)} ON {FormatColumn(j.Columna1)} {j.Operador} {FormatColumn(j.Columna2)} {extraCondicion}";
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
                if(q.Pagination != null)
                {
                    var p = q.Pagination;
                    orderBy += $" OFFSET {(p.Page - 1 * p.Paginate)} ROWS";
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
                groupBy = BuildGroupBy();
            if (where != "") where = "WHERE " + where;
            string query = $"SELECT {limit} {select} FROM {FormatTable(q.Tabla)} {joins} {where} {orderBy} {groupBy}";
            return System.Text.RegularExpressions.Regex.Replace(query, @"\s+", " ");
        }

        public string BuildSelect()
        {
            string select = "";
            var q = QueryBuilder;
            foreach (string item in q.Columnas)
            {
                select += FormatColumn(item) + ",";
            }
            if (q._SelectSubquery.Count > 0)
            {
                foreach (var key in q._SelectSubquery.Keys)
                {
                    if (q._SelectSubquery[key] is QueryBuilder)
                        select += $"({(q._SelectSubquery[key] as IBuilder).BuildQuery()})";
                    else select += $"({q._SelectSubquery[key]})";
                    select += $" AS {FormatColumn(key)},";
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
                where += $" {w.Boleano} {FormatColumn(w.Columna)} {w.Operador} ";
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
                            var q = (IBuilder)w.Valor;
                            val = $"({q.BuildQuery()})";
                        }
                        where = where.Trim(' ');
                        where += val;
                        break;
                    case QueryBuilder.TipoWhere.WhereColumn:
                        where += FormatColumn((string)w.Valor);
                        break;
                    case QueryBuilder.TipoWhere.WhereGroup:
                        QueryBuilder q1 = (QueryBuilder)w.Valor;
                        if (q1.Columnas.Length == 0) where += $"({q1.GetBuilder(Tipo).BuildWeres()})";
                        else where += $"({q1.GetQuery(TipoDeBd.SqlServer)})";
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
                into += FormatColumn(item) + ",";
            }
            foreach (object item in diccionario.Values)
            {
                values += FormatValue(item) + ",";
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
    }
}

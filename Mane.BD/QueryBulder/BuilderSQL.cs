using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mane.BD
{
    
    public partial class QueryBuilder
    {
        private static string formatValueForSQL(object value)
        {
            if (value == null) return "''";
            if(value is string)
            {
                var val = (string)value;
                if (val.Contains("'")) val = val.Replace("'", "''");
                return $"'{val}'";
            }
            if(value is bool)
                return (bool)value ? "'1'" : "'0'";
            if(value is DateTime)
                return $"'{Bd.toDateTimeSqlFormat((DateTime)value)}'";
            if (value.GetType().BaseType == typeof(Enum))
                return $"'{(int)value}'";
            return $"'{value}'";        
            

        }
        private string insertSQL(Dictionary<string,object> diccionario)
        {
            if (Tabla == null || Tabla == "") throw new QueryBuilderExeption("No se ha establecido el nombre de la tabla para el insert");
            string into = "";
            string values = "";
            foreach (string item in diccionario.Keys)
            {
                into += $"[{item}],";
            }
            foreach (object item in diccionario.Values)
            {
                values += formatValueForSQL(item) + ",";
            }
            into = into.Trim(',');
            values = values.Trim(',');
            return $"INSERT INTO {formatearColumnaSQL(this.Tabla)}({into}) VALUES ({values}); SELECT SCOPE_IDENTITY();";
        }
        private string insertSQL(object objeto)
        {
            Dictionary<string, object> modelDic = Common.ObjectToKeyValue(objeto);
            return insertSQL(modelDic);
        }
        private string buildWhereSQL()
        {
            var where = "";
            if (Wheres.Count == 0) return where;
            foreach (var w in Wheres)
            {
                where += $" {w.Boleano} {formatearColumnaSQL(w.Columna)} {w.Operador} ";
                switch (w.Tipo)
                {
                    case TipoWhere.WhereIn:
                        string val = "";
                        if (w.Valor.GetType().IsArray)
                        {
                            Array valores = (Array)w.Valor;
                            foreach (var item in valores)
                            {
                                val += $"{formatValueForSQL(item)},";
                            }
                            val = val.Trim(',');
                            val = $"({val})";
                        }else if(w.Valor.GetType() == typeof(QueryBuilder))
                        {
                            QueryBuilder q = (QueryBuilder)w.Valor;
                            val = $"({q.buildQuerySQL()})";
                        }
                        where = where.Trim(' ');
                        where += val;
                        break;
                    case TipoWhere.WhereColumn:
                        where += formatearColumnaSQL((string)w.Valor);
                        break;
                    case TipoWhere.WhereGroup:
                        QueryBuilder q1 = (QueryBuilder)w.Valor;
                        if (q1.Columnas.Length == 0) where += $"({q1.buildWhereSQL()})";
                        else where += $"({q1.getQuery(TipoDeBd.SqlServer)})";
                        break;
                    case TipoWhere.WhereBetween:
                        var vals = (object[])w.Valor;
                        where += $"{formatValueForSQL(vals[0])} AND {formatValueForSQL(vals[1])}";
                        break;
                    default:
                        where += formatValueForSQL(w.Valor);
                        break;
                }
            }
            where = where.Substring(Wheres[0].Boleano.Length + 1);
            return where;
        }
        private string buildJoinsSQL()
        {
            string joins = "";
            string extraCondicion = "";
            string consulta = "";
            foreach (var j in Joins)
            {
                extraCondicion = j.ExtraCondicion != null ?  "AND " + j.ExtraCondicion.buildWhereSQL() : "";
                consulta = j.Consulta != null ? $"({j.Consulta.buildQuerySQL()}) {formatearColumnaSQL(j.AliasDeConsulta)}" : "";
                joins += $" {j.Tipo} JOIN {consulta}{formatearTablaSQL(j.Tabla)} ON {formatearColumnaSQL(j.Columna1)} {j.Operador} {formatearColumnaSQL(j.Columna2)} {extraCondicion}";
            }
            return joins;
        }
        private string formatearTablaSQL(string nombreTabla)
        {
            string output = nombreTabla.Trim();
            if(output.Contains(" "))
            {
                var aux = output.Split(' ');
                if (aux.Length == 2)
                    return $"{formatearColumnaSQL(aux[0])} {formatearColumnaSQL(aux[1])}";
                else return output;
            }
            return formatearColumnaSQL(output);
        }
        private static string formatearColumnaSQL(string columna)
        {
            if (columna == "") return columna;
            string colFormateada = "";


            if (columna.ToLower().Contains(" as ")) // Columna as otraCosa
            {
                string funcion = "";
                if (columna.Contains(")"))//Funcion()
                {
                    var splitFunc = columna.Split(')');
                    if (splitFunc.Length == 1)//funcion( as )
                        return columna;
                    funcion = splitFunc[0] + ")";

                    if (funcion.ToLower().Contains(" as "))//funcion( as ) as columna
                    {
                        var colTmp = splitFunc[1];
                        colTmp = colTmp.Trim();
                        colTmp = colTmp.Replace("As ", "as ").Replace("AS ", "as ").Replace("aS ", "as ").Replace("as ", "");
                        return $"{funcion} AS {formatearColumnaSQL(colTmp)}";

                    }

                }
                columna = columna.Replace(" As ", " as ").Replace(" AS ", " as ").Replace(" aS ", " as ");
                string[] aux2 = columna.Replace(" as ", ";").Split(';');
                if (aux2.Length == 2)
                {
                    if (aux2[0].Contains("'"))// 'Algo' as Columna
                        return $"{aux2[0]} AS {formatearColumnaSQL(aux2[1])}";
                    return $"{formatearColumnaSQL(aux2[0])} AS {formatearColumnaSQL(aux2[1])}";
                }
                else if (aux2.Length == 1) // as columna
                    return formatearColumnaSQL(aux2[0]);

            }
            if (columna.Contains("("))//Funcion()
            {
                return columna;
            }

            if (columna.Contains("."))//BaseDeDatos.Esquema.Tabla.Columna
            {
                string[] aux = columna.Split('.');
                foreach (var item in aux)
                {
                    if (item == "*") colFormateada += item; //Columna.*
                    else
                        colFormateada += $"[{item}].";
                }
                colFormateada = colFormateada.Trim('.');
            }
            else colFormateada = $"[{columna}]";
            return colFormateada;
        }
            private string buildLimitSQL() =>
         this.Limit > 0 ? $"TOP({Limit})" : "";
        private string buildSelectSQL()
        {
            string select = "";
            foreach (string item in Columnas)
            {
                select += formatearColumnaSQL(item) + ",";
            }
            if(SelectSubquery.Count > 0)
            {
                foreach (var key in SelectSubquery.Keys)
                {
                    if (SelectSubquery[key] is QueryBuilder)
                        select += $"({(SelectSubquery[key] as QueryBuilder).buildQuerySQL()})";
                    else select += $"({SelectSubquery[key]})";
                    select += $" AS {formatearColumnaSQL(key)},";
                }
            }
            select = select.Trim(',');
            if (select == "") select = "*";
            return select;
        }
        private string buildOrderBySQL()
        {
            string orderBy = "";
            if (this.Order != null)
            {
                string orden = Order.Orden == OrderDireccion.Asendente ? "ASC" : "DESC";
                orderBy = $"ORDER BY {formatearColumnaSQL(Order.Columna)} {orden}";
            }
            return orderBy;
        }
        private string buildGroupBySQL()
        {
            string groupBy = "";
            if(GroupBy != null)
            {
                foreach(string col in GroupBy)
                {
                    groupBy += formatearColumnaSQL(col) + ",";
                }
                groupBy = groupBy.Trim(',');
                groupBy = $"GROUP BY {groupBy}";
            }
            return groupBy;
        }
        private string buildQuerySQL()
        {
            if (RawQuery != null) return RawQuery;
            string select = buildSelectSQL(),
                where = buildWhereSQL(),
                joins = buildJoinsSQL(),
                limit = buildLimitSQL(),
                orderBy = buildOrderBySQL(),
                groupBy = buildGroupBySQL();
            if (where != "") where = "WHERE " + where;
            string query = $"SELECT {limit} {select} FROM {formatearTablaSQL(Tabla)} {joins} {where} {orderBy} {groupBy}";
            return System.Text.RegularExpressions.Regex.Replace(query, @"\s+", " ");
        }
        private string updateSQL(Dictionary<string,object> dic)
        {
            string set = "";
            foreach (string key in dic.Keys)
            {
                set += $"[{key}] = {formatValueForSQL(dic[key])},";
            }
            set = set.Trim(',');
            return $"UPDATE {formatearColumnaSQL(Tabla)} SET {set} WHERE {buildWhereSQL()}";
        }
        
        private string deleteSQL()
           => $"DELETE FROM {formatearColumnaSQL(Tabla)} WHERE {buildWhereSQL()}";

        private string countSQL()
        => $"SELECT COUNT(*) AS [Count] FROM ({buildQuerySQL()}) AS [QueryBuilder_Count]";
       
        
    }
}

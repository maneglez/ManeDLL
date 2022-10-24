using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD
{
    /// <summary>
    /// Generador de consultas 1.0.0 By ManeG
    /// </summary>
    /// <remarks>Conexiones compatibles:
    /// <list type="bullet">
    /// <item>SQL Server</item>
    /// </list>
    /// </remarks>
    [Serializable]
    public partial class QueryBuilder 
    {
        #region Atributos de la clase
        internal string[] Columnas;
        internal List<Join> Joins;
        internal List<Where> Wheres;
        internal Dictionary<string, object> SelectSubquery;
        internal string Tabla;
        internal string RawQuery;
        internal string[] GroupBy;
        internal OrderBy Order;
        internal int Limit;
        internal string NombreConexion;
        #endregion

        #region Constructores
        /// <summary>
        /// Query builder
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        public QueryBuilder(string tabla)
        {
            construct();
            this.Tabla = tabla;
        }

        /// <summary>
        /// Query builder
        /// </summary>
        public QueryBuilder()
        {
            construct();
        }
        /// <summary>
        /// Inicializa los atributos
        /// </summary>
        private void construct()
        {
            Wheres = new List<Where>();
            Joins = new List<Join>();
            Columnas = new string[] { };
            Tabla = "";
            Limit = 0;
            SelectSubquery = new Dictionary<string, object>();
        }
        #endregion

        #region Acciones Finales (No retornan un Objeto de la misma clase)
        /// <summary>
        /// Obtiene las columnas del QueryBuilder Actual
        /// </summary>
        /// <returns></returns>
        public string[] getCurrentColumnsNames()
        {
            var cols = new List<string>();
            foreach (var item in Columnas)
            {
                if (item.ToLower().Contains(" as "))
                {
                    var aux = item.Replace(" AS ", " as ")
                        .Replace(" As ", " as ")
                        .Replace(" aS ", " as ")
                        .Replace(" as ", ";")
                        .Split(';');
                    if (aux.Length > 0)
                        cols.Add(aux[0]);
                }
                else cols.Add(item);
            }
            return cols.ToArray();
        }
        public string[] getCurrentColumnsAlias()
        {
            var cols = new List<string>();
            foreach (var item in Columnas)
            {

                if (item.ToLower().Contains(" as "))
                {
                    var aux = item.Replace(" AS ", " as ")
                        .Replace(" As ", " as ")
                        .Replace(" aS ", " as ")
                        .Replace(" as ", ";")
                        .Split(';');
                    if (aux.Length > 0)
                        cols.Add(aux[aux.Length - 1]);
                }
                else if (item.Contains("."))
                {
                    var aux = item.Split('.');
                    cols.Add(aux[aux.Length - 1]);
                }
                else cols.Add(item);
            }
            return cols.ToArray();
        }

        /// <summary>
        /// Eliminar uno o varios registros
        /// </summary>
        /// <param name="connName">Nombre de la conexión</param>
        public void delete(string connName = "")
        {
            if (Wheres.Count == 0) throw new QueryBuilderExeption("No puede ejecutar un delete sin condición");
            if (Tabla == "") throw new QueryBuilderExeption("No se eligió una tabla");
            switch (Bd.getTipoBd(connName))
            {
                case TipoDeBd.SqlServer:
                    Bd.executeNonQuery(deleteSQL(), connName);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Cuenta total de registros devueltos por la consulta
        /// </summary>
        /// <param name="connName">Nombre de la conexion a utilizar</param>
        /// <returns>El numero de registros devueltos por la consulta</returns>
        /// <exception cref="QueryBuilderExeption"></exception>
        public int count(string connName = "")
        {
            if (Tabla == null || Tabla == "") throw new QueryBuilderExeption("No se ha especificado una tabla");
            object result = null;
            switch (Bd.getTipoBd(connName))
            {
                case TipoDeBd.SqlServer:
                    result = Bd.getFirstValue(countSQL(), connName);
                    break;
            }
            return result == null ? 0 : Convert.ToInt32(result);
        }

        /// <summary>
        /// Ejecuta un insert en la tabla actual
        /// </summary>
        /// <param name="objeto">Objeto que representa las columnas y valores ej: new {Nombre = "Nombre",Edad = "23"}</param>
        /// <param name="connName">Nombre de la conexión</param>
        /// <returns>El identificador del registro insertado</returns>
        public string insert(object objeto, string connName = "")
        {
            if (Tabla == "") throw new QueryBuilderExeption("No se eligió una tabla");
            switch (Bd.getTipoBd(connName))
            {
                case TipoDeBd.SqlServer:
                    object result = Bd.getFirstValue(insertSQL(objeto), connName);
                    if (result == DBNull.Value)
                        return "";
                    else return result.ToString();
                default:
                    break;
            }
            return "";

        }
        /// <summary>
        /// Inserta un registro
        /// </summary>
        /// <param name="dic">Clave valor que hace referencia a las columnas y valores que se insertaran</param>
        /// <param name="connName">Nombre de la conexion</param>
        /// <returns></returns>
        public object insert(Dictionary<string, object> dic, string connName)
        {
            if (Tabla == "") throw new QueryBuilderExeption("No se eligió una tabla");
            switch (Bd.getTipoBd(connName))
            {
                case TipoDeBd.SqlServer:
                    object result = Bd.getFirstValue(insertSQL(dic), connName);
                    if (result == DBNull.Value || result == null)
                        return null;
                    else
                        return result;
                default:
                    break;
            }
            return null;
        }
        /// <summary>
        /// Ejecuta un update en la tabla actual
        /// </summary>
        /// <param name="objeto">Objeto que representa las columnas y valores ej: new {Nombre = "Nombre",Edad = "23"}</param>
        /// <param name="connName">Nombre de la conexión</param>
        /// <returns>El numero total de registros afectados</returns>
        public int update(object objeto, string connName = "")
        {
            return update(Common.ObjectToKeyValue(objeto), connName);
        }
        /// <summary>
        /// Ejecuta un update en la tabla actual
        /// </summary>
        /// <param name="dic">Clave valor que representa el update</param>
        /// <param name="connName">nombre de la conexion</param>
        /// <returns>El numero de registros afectados</returns>
        /// <exception cref="QueryBuilderExeption"></exception>
        public int update(Dictionary<string, object> dic, string connName = "")
        {
            if (dic == null) return 0;
            if (Wheres.Count == 0) throw new QueryBuilderExeption("No puede ejecutar un update sin condición");
            if (Tabla == "") throw new QueryBuilderExeption("No se eligió una tabla");
            switch (Bd.getTipoBd(connName))
            {
                case TipoDeBd.SqlServer:
                    return Bd.executeNonQuery(updateSQL(dic), connName);
                default:
                    break;
            }
            return 0;
        }

        /// <summary>
        /// Crea una copia del query actual;
        /// </summary>
        /// <returns></returns>
        public QueryBuilder copy()
        {
            QueryBuilder q;
            using (var ms = new System.IO.MemoryStream())
            {
                System.Runtime.Serialization.IFormatter formatter =
                    new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                q = (QueryBuilder)formatter.Deserialize(ms);
            }
            return q;
        }

        /// <summary>
        /// Obtiene el primer valor del primer registro de la consulta
        /// </summary>
        /// <param name="connName">Nombre de la conexion</param>
        /// <returns>Retorna nulo si no encuentra nada</returns>
        public object getScalar(string connName = "") => Bd.getFirstValue(
            getQuery(Bd.getTipoBd(connName)),
            connName);

        /// <summary>
        /// Genera una cadena de consulta en formato SQL
        /// </summary>
        /// <returns>Consulta SQL</returns>
        public string getQuery(TipoDeBd tipo = TipoDeBd.SqlServer)
        {
            switch (tipo)
            {
                case TipoDeBd.SqlServer:
                    return buildQuerySQL();
                default:
                    break;
            }
            return "";
        }

        /// <summary>
        /// Genera una cadena de consulta en formato SQL
        /// </summary>
        /// <returns>Consulta SQL</returns>
        public string getQuery(string nombreConexion = "")
            => getQuery(Bd.getTipoBd(nombreConexion));

        /// <summary>
        /// Retorna un data table con el resultado de la consulta actual
        /// </summary>
        /// <param name="connName">Nombre de la conexión</param>
        /// <returns></returns>
        public DataTable get(string connName = "") => Bd.executeQuery(
            getQuery(Bd.getTipoBd(connName)),
            connName);
        /// <summary>
        /// Determina si la consulta retorna registros
        /// </summary>
        /// <param name="connName">Nombre de la conexion</param>
        /// <returns>Verdadero si se retorna almenos 1 registro falso si retorna 0 registros</returns>
        public bool exists(string connName)
        {
            limit(1);
            return Bd.exists(getQuery(connName), connName);
        }

        #endregion

        #region Estructura basica de consulta SQL
        /// <summary>
        /// Establece el nombre de la tabla
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <returns></returns>
        public QueryBuilder from(string tabla)
        {
            this.Tabla = tabla;
            return this;
        }

        /// <summary>
        /// Establece las columnas que se seleccionarán
        /// </summary>
        /// <param name="columnas">Nombres de columnas por defecto "*"</param>
        /// <returns></returns>
        public QueryBuilder select(params string[] columnas)
        {
            Columnas = columnas;
            return this;
        }

        public QueryBuilder select(QueryBuilder query, string alias)
        {
            if (query == null || string.IsNullOrEmpty(alias)) throw new ArgumentNullException("query");
            SelectSubquery.Add(alias, query);
            return this;
        }
        public QueryBuilder selectRaw(string RawQuery, string alias)
        {
            SelectSubquery.Add(alias, RawQuery);
            return this;
        }

        public QueryBuilder select(Dictionary<string, QueryBuilder> SelectSubquery)
        {
            if (SelectSubquery == null) throw new ArgumentNullException("SelectSubquery");
            foreach (var item in SelectSubquery.Keys)
            {
                this.SelectSubquery.Add(item, SelectSubquery[item]);
            }
            return this;
        }

        /// <summary>
        /// Indica si quiere limitar la cantidad de registros retornados
        /// </summary>
        /// <param name="limit">Cantidad de registros</param>
        /// <returns></returns>
        public QueryBuilder limit(int limit)
        {
            this.Limit = limit;
            return this;
        }

        /// <summary>
        /// Orden
        /// </summary>
        /// <param name="columna">columna a ordenar</param>
        /// <param name="orden">orden (asc | desc) por defecto asc</param>
        /// <returns></returns>
        public QueryBuilder orderBy(string columna, OrderDireccion orden = OrderDireccion.Asendente)
        {
            Order = new OrderBy()
            {
                Columna = columna,
                Orden = orden

            };
            return this;
        }

        /// <summary>
        /// Orden
        /// </summary>
        /// <param name="columnas">Columnas a ordenar</param>
        /// <returns></returns>
        public QueryBuilder groupBy(params string[] columnas)
        {
            this.GroupBy = columnas;
            return this;
        }

        /// <summary>
        /// La consulta se va a ejecutar tal y como se escribe
        /// </summary>
        /// <param name="query">Consulta SQL</param>
        /// <returns></returns>
        public QueryBuilder rawQuery(string query)
        {
            RawQuery = query;
            return this;
        }


        #endregion

        #region Joins

        private QueryBuilder commonJoin(string tabla, string col1, string col2, string operador, string tipo)
        {
            Joins.Add(new Join()
            {
                Tabla = tabla,
                Columna1 = col1,
                Columna2 = col2,
                Operador = operador,
                Tipo = tipo
            });
            return this;
        }
        private QueryBuilder commonJoin(string tabla, string col1, string col2, Func<QueryBuilder, QueryBuilder> func, string operador, string tipo)
        {
            Joins.Add(new Join()
            {
                Tabla = tabla,
                Columna1 = col1,
                Columna2 = col2,
                Operador = operador,
                ExtraCondicion = func.Invoke(new QueryBuilder()),
                Tipo = tipo
            });
            return this;
        }

        private QueryBuilder commonJoin(QueryBuilder consulta, string alias, string col1, string col2, string operador, string tipo, Func<QueryBuilder, QueryBuilder> func = null)
        {

            Joins.Add(new Join
            {
                Consulta = consulta,
                AliasDeConsulta = alias,
                Columna1 = col1,
                Columna2 = col2,
                Operador = operador,
                Tipo = tipo,
                ExtraCondicion = func == null ? null : func.Invoke(new QueryBuilder()),
            });
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="consulta"></param>
        /// <param name="alias"></param>
        /// <param name="col1"></param>
        /// <param name="col2"></param>
        /// <returns></returns>
        public QueryBuilder join(QueryBuilder consulta, string alias, string col1, string col2)
            => commonJoin(consulta, alias, col1, col2, "=", "INNER");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="consulta"></param>
        /// <param name="alias"></param>
        /// <param name="col1"></param>
        /// <param name="col2"></param>
        /// <param name="otrasCondiciones"></param>
        /// <returns></returns>
        public QueryBuilder join(QueryBuilder consulta, string alias, string col1, string col2, Func<QueryBuilder, QueryBuilder> otrasCondiciones)
            => commonJoin(consulta, alias, col1, col2, "=", "INNER", otrasCondiciones);
        /// <summary>
        /// Genera un join con otra tabla
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="col1">Columna 1</param>
        /// <param name="col2">Columna 2</param>
        /// <returns></returns>
        public QueryBuilder join(string tabla, string col1, string col2)
            => join(tabla, col1, col2, "=");

        /// <summary>
        /// Genera un join con otra tabla
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="col1">Columna 1</param>
        /// <param name="operador">operador</param>
        /// <param name="col2">Columna 2</param>
        /// <returns></returns>
        public QueryBuilder join(string tabla, string col1, string col2, string operador = "=")
            => commonJoin(tabla, col1, col2, operador, "INNER");

        /// <summary>
        /// Genera un join con otra tabla
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="col1">Columna 1</param>
        /// <param name="operador">operador</param>
        /// <param name="col2">Columna 2</param>
        /// <returns></returns>
        public QueryBuilder leftJoin(string tabla, string col1, string col2, string operador = "=")
            => commonJoin(tabla, col1, col2, operador, "LEFT");

        /// <summary>
        /// Genera un join con otra tabla
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="col1">Columna 1</param>
        /// <param name="operador">operador</param>
        /// <param name="col2">Columna 2</param>
        /// <returns></returns>
        public QueryBuilder rightJoin(string tabla, string col1, string col2, string operador = "=") => commonJoin(tabla, col1, col2, operador, "RIGHT");

        /// <summary>
        /// Genera un join con otra tabla
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="col1">Columna 1</param>
        /// <param name="operador">operador entre col1 y col +2</param>
        /// <param name="func">Condicioens where extra</param>
        /// <param name="col2">Columna 2</param>
        /// <returns></returns>
        public QueryBuilder join(string tabla, string col1, string col2, Func<QueryBuilder, QueryBuilder> func, string operador = "=") => commonJoin(tabla, col1, col2, func, operador, "INNER");
        /// <summary>
        /// Genera un join con otra tabla
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="col1">Columna 1</param>
        /// <param name="operador">operador entre col1 y col +2</param>
        /// <param name="func">Condicioens where extra</param>
        /// <param name="col2">Columna 2</param>
        /// <returns></returns>
        public QueryBuilder leftJoin(string tabla, string col1, string col2, Func<QueryBuilder, QueryBuilder> func, string operador = "=")
            => commonJoin(tabla, col1, col2, func, operador, "LEFT");

        /// <summary>
        /// Genera un join con otra tabla
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="col1">Columna 1</param>
        /// <param name="operador">operador entre col1 y col +2</param>
        /// <param name="func">Condicioens where extra</param>
        /// <param name="col2">Columna 2</param>
        /// <returns></returns>
        public QueryBuilder rightJoin(string tabla, string col1, string col2, Func<QueryBuilder, QueryBuilder> func, string operador = "=")
            => commonJoin(tabla, col1, col2, func, operador, "RIGHT");

        #endregion

        #region Wheres

        private QueryBuilder commonWhere(string col, string operador, object valor, string booleano, TipoWhere tipo)
        {
            Wheres.Add(new Where
            {
                Columna = col,
                Operador = operador,
                Valor = valor,
                Boleano = booleano,
                Tipo = tipo
            });
            return this;
        }
        private QueryBuilder commonWhere(string col, string operador, string booleano, Func<QueryBuilder, QueryBuilder> func)
        {
            var q = func.Invoke(new QueryBuilder(this.Tabla));
            this.Wheres.Add(new Where()
            {
                Columna = col,
                Valor = q,
                Boleano = booleano,
                Operador = operador,
                Tipo = TipoWhere.WhereGroup
            });
            return this;
        }

        /// <summary>
        /// Condicion de comparacion = entre la columna y valor o columna
        /// </summary>
        /// <param name="col1">Columna</param>
        /// <param name="valor">Valor</param>
        /// <returns></returns>
        public QueryBuilder where(string col1, object valor)
            => where(col1, "=", valor);

        /// <summary>
        /// Condicion where
        /// </summary>
        /// <param name="col1">Columna </param>
        /// <param name="operador">Operador</param>
        /// <param name="valor">Valor</param>
        /// <returns></returns>
        public QueryBuilder where(string col1, string operador, object valor)
            => commonWhere(col1, operador, valor, "AND", TipoWhere.Where);

        /// <summary>
        /// Compara si 2 columnas son iguales
        /// </summary>
        /// <param name="col1">columna 1</param>
        /// <param name="col2">columna 2</param>
        /// <returns></returns>
        public QueryBuilder whereColumn(string col1, string col2)
        => whereColumn(col1, "=", col2);

        /// <summary>
        /// Compara 2 columnas
        /// </summary>
        /// <param name="col1">columna 1</param>
        /// <param name="operador">operador de comparacion</param>
        /// <param name="col2">columna 2</param>
        /// <returns></returns>
        public QueryBuilder whereColumn(string col1, string operador, string col2)
            => commonWhere(col1, operador, col2, "", TipoWhere.WhereColumn);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col1"></param>
        /// <param name="valor1"></param>
        /// <param name="valor2"></param>
        /// <returns></returns>
        public QueryBuilder whereBetween(string col1, object valor1, object valor2)
                    => commonWhere(col1, "BETWEEN", new object[] { valor1, valor2 }, "AND", TipoWhere.WhereBetween);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col1"></param>
        /// <param name="valor1"></param>
        /// <param name="valor2"></param>
        /// <returns></returns>
        public QueryBuilder orWhereBetween(string col1, object valor1, object valor2)
            => commonWhere(col1, "BETWEEN", new object[] { valor1, valor2 }, "OR", TipoWhere.WhereBetween);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public QueryBuilder where(Func<QueryBuilder, QueryBuilder> func)
            => commonWhere("", "", "AND", func);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public QueryBuilder where(string col, Func<QueryBuilder, QueryBuilder> func)
        => where(col, "=", func);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="func"></param>
        /// <param name="operador"></param>
        /// <returns></returns>
        public QueryBuilder where(string col, string operador, Func<QueryBuilder, QueryBuilder> func)
        => commonWhere(col, operador, "AND", func);

        /// <summary>
        /// Condicion where con el operador or
        /// </summary>
        /// <param name="col1">Columna </param>
        /// <param name="operador">Operador</param>
        /// <param name="Valor">Valor </param>
        /// <returns></returns>
        public QueryBuilder orWhere(string col1, string operador, object Valor)
        => commonWhere(col1, operador, Valor, "OR", TipoWhere.Where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public QueryBuilder orWhere(Func<QueryBuilder, QueryBuilder> func)
            => commonWhere("", "", "OR", func);
        //{
        //    QueryBuilder q = func.Invoke(new QueryBuilder());
        //    Where w = new Where()
        //    {
        //        Valor = q,
        //        Boleano = "OR",
        //        Tipo = TipoWhere.WhereGroup
        //    };
        //    this.Wheres.Add(w);
        //    return this;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="valores"></param>
        /// <returns></returns>
        public QueryBuilder whereIn(string columna, string[] valores)
        {
            var w = new Where()
            {
                Columna = columna,
                Operador = "IN",
                Valor = valores,
                Boleano = "AND",
                Tipo = TipoWhere.WhereIn
            };
            Wheres.Add(w);
            return this;
        }
        public QueryBuilder whereIn(string columna, int[] valores)
        {
            var w = new Where()
            {
                Columna = columna,
                Operador = "IN",
                Valor = valores,
                Boleano = "AND",
                Tipo = TipoWhere.WhereIn
            };
            Wheres.Add(w);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="valores"></param>
        /// <returns></returns>
        public QueryBuilder whereNotIn(string columna, Array valores)
        {
            var w = new Where()
            {
                Columna = columna,
                Operador = "NOT IN",
                Valor = valores,
                Boleano = "AND",
                Tipo = TipoWhere.WhereIn
            };
            Wheres.Add(w);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public QueryBuilder whereIn(string columna, Func<QueryBuilder, QueryBuilder> func)
        {
            var q = func.Invoke(new QueryBuilder());
            var w = new Where()
            {
                Columna = columna,
                Operador = "IN",
                Valor = q,
                Boleano = "AND",
                Tipo = TipoWhere.WhereIn
            };
            Wheres.Add(w);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public QueryBuilder whereNotIn(string columna, Func<QueryBuilder, QueryBuilder> func)
        {
            var q = func.Invoke(new QueryBuilder());
            var w = new Where()
            {
                Columna = columna,
                Operador = "NOT IN",
                Valor = q,
                Boleano = "AND",
                Tipo = TipoWhere.WhereIn
            };
            Wheres.Add(w);
            return this;
        }

        private QueryBuilder whereIsNull(string columna)
        {
            throw new NotImplementedException();
        }

        private QueryBuilder whereIsNotNull(string columna)
        {
            throw new NotImplementedException();
        }



        #endregion

        #region SubClases y Enums
        [Serializable]
        internal class Where
        {
            public string Columna, Operador, Boleano;
            public object Valor;
            public TipoWhere Tipo;
            public Where()
            {
                Columna = Operador = Boleano = "";
                Tipo = TipoWhere.Where;
            }

        }
        [Serializable]
        internal class OrderBy
        {
            public string Columna;
            public OrderDireccion Orden = OrderDireccion.Asendente;
        }

        /// <summary>
        /// Dirección de ordenamiento del ORDER BY
        /// </summary>
        public enum OrderDireccion
        {
            /// <summary>
            /// Orden Asendente
            /// </summary>
            Asendente,
            /// <summary>
            /// Orden Descendente
            /// </summary>
            Descendente
        }
        internal enum TipoWhere
        {
            Where,
            WhereGroup,
            WhereIn,
            WhereNull,
            WhereColumn,
            WhereBetween
        }
        [Serializable]
        internal class Join
        {
            public string Tabla, Columna1, Columna2, Operador, Tipo, AliasDeConsulta;
            public QueryBuilder ExtraCondicion;
            public QueryBuilder Consulta;
            public Join()
            {
                Tabla = Columna1 = Columna2 = Operador = Tipo = AliasDeConsulta = "";
            }
        }
        #endregion

        #region helpers
        /// <summary>
        /// Obtener el valor del límite actual
        /// </summary>
        /// <returns></returns>
        public int CurrentLimit => Limit;
        #endregion

    }
}

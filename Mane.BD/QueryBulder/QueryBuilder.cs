﻿using Mane.BD.QueryBulder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
   
    public partial class QueryBuilder
    {
        #region Atributos de la clase
        internal string[] Columnas;
        internal List<JoinClass> Joins;
        internal List<WhereClass> Wheres;
        internal List<CaseClass> _SelectCase;
        internal Dictionary<string, object> _SelectSubquery;
        internal string Tabla;
        internal string _RawQuery;
        internal string[] _GroupBy;
        internal OrderByClass Order;
        internal int _Limit;
        internal string NombreConexion;
        internal PaginateClass Pagination;
        internal bool _Distinct;
        #endregion

        #region Constructores
        /// <summary>
        /// Query builder
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        public QueryBuilder(string tabla)
        {
            Construct();
            this.Tabla = tabla;
        }

        /// <summary>
        /// Query builder
        /// </summary>
        public QueryBuilder()
        {
            Construct();
        }
        /// <summary>
        /// Inicializa los atributos
        /// </summary>
        private void Construct()
        {
            Wheres = new List<WhereClass>();
            Joins = new List<JoinClass>();
            Columnas = new string[] { };
            Tabla = "";
            _Limit = 0;
            _SelectSubquery = new Dictionary<string, object>();
            _SelectCase = new List<CaseClass>();
        }
        #endregion

        #region Acciones Finales (No retornan un Objeto de la misma clase)
        /// <summary>
        /// Obtiene el primer valor del primer registro de la consulta
        /// </summary>
        /// <param name="connName">Nombre de la conexion</param>
        /// <returns>Retorna nulo si no encuentra nada</returns>
        public object GetScalar(string connName = "")
        {
            connName = VerifyConnection(connName);
            return Bd.ExecuteEscalar(GetBuilder().BuildQuery(), connName);
        }
        public object GetScalar(string connName, object defaultValue = null)
        {
            connName = VerifyConnection(connName);
            var result = Bd.ExecuteEscalar(GetBuilder().BuildQuery(), connName);
            return result ?? defaultValue;
        }

        /// <summary>
        /// Genera una cadena de consulta en formato SQL
        /// </summary>
        /// <returns>Consulta SQL</returns>
        public string GetQuery(TipoDeBd tipo = TipoDeBd.SqlServer)
        {
            return GetBuilder(tipo)?.BuildQuery();
        }


        /// <summary>
        /// Genera una cadena de consulta en formato SQL
        /// </summary>
        /// <returns>Consulta SQL</returns>
        public string GetQuery(string NombreConexion = "")
        {
            NombreConexion = VerifyConnection(NombreConexion);
            return GetBuilder(NombreConexion).BuildQuery();
        }


        /// <summary>
        /// Retorna un data table con el resultado de la consulta actual
        /// </summary>
        /// <param name="NombreConexion">Nombre de la conexión</param>
        /// <returns></returns>
        public DataTable Get(string NombreConexion = "")
        {
            NombreConexion = VerifyConnection(NombreConexion);
            return Bd.ExecuteQuery(
             GetBuilder(NombreConexion).BuildQuery(),
             NombreConexion);
        }
        public DataTable ExecuteProcedure(string procedureName, object[] parametros = null, string ConnName = "")
        {
            ConnName = VerifyConnection(ConnName);
            return Bd.ExecuteQuery(
                GetBuilder(ConnName).BuildExecProcedure(procedureName, parametros),
                ConnName
                );
        }


        #endregion

        #region Joins
        private QueryBuilder CommonJoin(string tabla, string col1, string col2, string operador, string tipo)
        {
            Joins.Add(new JoinClass()
            {
                Tabla = tabla,
                Columna1 = col1,
                Columna2 = col2,
                Operador = operador,
                Tipo = tipo
            });
            return this;
        }
        private QueryBuilder CommonJoin(string tabla, string col1, string col2, Func<QueryBuilder, QueryBuilder> func, string operador, string tipo)
        {
            Joins.Add(new JoinClass()
            {
                Tabla = tabla,
                Columna1 = col1,
                Columna2 = col2,
                Operador = operador,
                ExtraCondicion = func.Invoke(new QueryBuilder()) as QueryBuilder,
                Tipo = tipo
            });
            return this;
        }
        private QueryBuilder CommonJoin(QueryBuilder consulta, string alias, string col1, string col2, string operador, string tipo, Func<QueryBuilder, QueryBuilder> func = null)
        {

            Joins.Add(new JoinClass
            {
                Consulta = consulta as QueryBuilder,
                AliasDeConsulta = alias,
                Columna1 = col1,
                Columna2 = col2,
                Operador = operador,
                Tipo = tipo,
                ExtraCondicion = func == null ? null : func.Invoke(new QueryBuilder()) as QueryBuilder,
            });
            return this;
        }
        #endregion

        #region Wheres
        private QueryBuilder CommonWhere(string col, string operador, object valor, string booleano, TipoWhere tipo)
        {
            Wheres.Add(new WhereClass
            {
                Columna = col,
                Operador = operador,
                Valor = valor,
                Boleano = booleano,
                Tipo = tipo
            });
            return this;
        }
        private QueryBuilder CommonWhere(string col, string operador, string booleano, Func<QueryBuilder, QueryBuilder> func)
        {
            var q = func.Invoke(new QueryBuilder(this.Tabla));
            this.Wheres.Add(new WhereClass()
            {
                Columna = col,
                Valor = q,
                Boleano = booleano,
                Operador = operador,
                Tipo = TipoWhere.WhereGroup
            });
            return this;
        }
        private QueryBuilder CommonWhere(object valor, string operador, string booleano, Func<QueryBuilder, QueryBuilder> func)
        {
            var q = func.Invoke(new QueryBuilder(this.Tabla));
            this.Wheres.Add(new WhereClass()
            {
                Query = q,
                Valor = valor,
                Boleano = booleano,
                Operador = operador,
                Tipo = TipoWhere.WhereGroup
            });
            return this;
        }
        #endregion

        #region Case Functions
        public QueryBuilder Case(Func<CaseClass, CaseClass> func)
        {
            _SelectCase.Add(func.Invoke(new CaseClass()));
            return this;
        }
        public QueryBuilder Case(string column, Func<CaseClass, CaseClass> func)
        {
            _SelectCase.Add(func.Invoke(new CaseClass(column)));
            return this;
        }
        #endregion

        #region SubClases y Enums
       
        internal class WhereClass
        {
            public string Columna, Operador, Boleano;
            public object Valor;
            public TipoWhere Tipo;
            public QueryBuilder Query;
            public WhereClass()
            {
                Columna = Operador = Boleano = "";
                Tipo = TipoWhere.Where;
            }
            public WhereClass Copy()
            {
                return new WhereClass
                {
                    Columna = Columna,
                    Operador = Operador,
                    Boleano = Boleano,
                    Valor = Valor,
                    Tipo = Tipo,
                    Query = Query?.Copy()
                };
            }

        }
        internal class OrderByClass
        {
            public string[] Columnas;
            public OrderDireccion Orden = OrderDireccion.Asendente;
            public OrderByClass Copy()
            {
                return new OrderByClass
                {
                    Columnas = Columnas?.ToArray(),
                    Orden = Orden
                };
            }
        }
        internal class PaginateClass
        {
            public PaginateClass(int page, int paginate)
            {
                Page = page;
                Paginate = paginate;
            }

            public int Page { get; set; }
            public int Paginate { get; set; }
            public PaginateClass Copy()
            {
                return new PaginateClass(Page, Paginate);
            }
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
        internal class JoinClass
        {
            public string Tabla, Columna1, Columna2, Operador, Tipo, AliasDeConsulta;
            public QueryBuilder ExtraCondicion;
            public QueryBuilder Consulta;
            public JoinClass()
            {
                Tabla = Columna1 = Columna2 = Operador = Tipo = AliasDeConsulta = "";
            }
            public JoinClass Copy()
            {
                return new JoinClass()
                {
                    Tabla = Tabla,
                    Columna1 = Columna1,
                    Columna2 = Columna2,
                    Operador = Operador,
                    Tipo = Tipo,
                    AliasDeConsulta = AliasDeConsulta,
                    ExtraCondicion = ExtraCondicion?.Copy(),
                    Consulta = Consulta?.Copy()
                };
            }
        }
        #endregion

        #region helpers

        public QueryBuilder Connection(string connectionName)
        {
            this.NombreConexion = connectionName;
            return this;
        }
        /// <summary>
        /// Establece y/o recupera el nombre de la conexion del querybuilder actual
        /// </summary>
        /// <param name="NombreConexion"></param>
        /// <returns></returns>
        private string VerifyConnection(string NombreConexion)
        {
            if (!string.IsNullOrWhiteSpace(NombreConexion))
                this.NombreConexion = NombreConexion;
            else if (!string.IsNullOrWhiteSpace(Bd.DefaultConnectionName) && string.IsNullOrWhiteSpace(NombreConexion))
                this.NombreConexion = Bd.DefaultConnectionName;
            return this.NombreConexion;
        }
        /// <summary>
        /// Crea una copia del query actual;
        /// </summary>
        /// <returns></returns>
        public QueryBuilder Copy()
        {
            QueryBuilder q = new QueryBuilder
            {
                Tabla = Tabla,
                _Distinct = _Distinct,
                _Limit = _Limit,
                _RawQuery = _RawQuery,
                NombreConexion = NombreConexion,
                Columnas = Columnas?.ToArray(),
                _GroupBy = _GroupBy?.ToArray(),
                Pagination = Pagination?.Copy(),
                Order = Order?.Copy()
            };
            q.Joins.AddRange(from x in Joins select x.Copy());
            q.Wheres.AddRange(from x in Wheres select x.Copy());
            q._SelectCase.AddRange(from x in _SelectCase select x.Copy());
            foreach (var item in _SelectSubquery)
            {
                if (item.Value is QueryBuilder qb)
                    q._SelectSubquery.Add(item.Key, qb?.Copy());
                else q._SelectSubquery.Add(item.Key, item.Value);
            }
            
            //Esto es lo que creo que hace que truene(creo)
            //using (var ms = new System.IO.MemoryStream())
            //{
            //    System.Runtime.Serialization.IFormatter formatter =
            //        new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //    formatter.Serialize(ms, this);
            //    ms.Seek(0, System.IO.SeekOrigin.Begin);
            //    q = (QueryBuilder)formatter.Deserialize(ms);
            //}
            return q;
        }
        /// <summary>
        /// Obtener el valor del límite actual
        /// </summary>
        /// <returns></returns>
        public int CurrentLimit => _Limit;
        /// <summary>
        /// Obtiene las columnas del QueryBuilder Actual
        /// </summary>
        /// <returns></returns>
        public string[] GetCurrentColumnsNames()
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
        public string[] GetCurrentColumnsAlias()
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
            foreach (var item in _SelectSubquery.Keys)
            {
                cols.Add(item);
            }
            return cols.ToArray();
        }
        #endregion

    }
}

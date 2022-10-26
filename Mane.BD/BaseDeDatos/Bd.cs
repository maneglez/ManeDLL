using Mane.BD.Executors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;



namespace Mane.BD
{
   
    /// <summary>
    /// Base de datos. By ManeG
    /// </summary>
    /// <remarks>Conexiones compatibles:
    /// <list type="bullet">
    /// <item>SQL Server</item>
    /// </list>
    /// </remarks>
    public  static partial class Bd
    {
        public static event EventHandler<BdExeptionEventArgs> OnException;
        /// <summary>
        /// Prueba una conexión a base de datos
        /// </summary>
        /// <param name="conexion">Conexión a probar</param>
        /// <returns>Verdadero si logra conectar, falso si no</returns>
        public static bool TestConection(Conexion conexion)
        {
            conexion.TimeOut = 5;
            switch (conexion.TipoDeBaseDeDatos)
            {
                case TipoDeBd.SqlServer:
                    return new SQLServer(conexion.CadenaDeConexion).TestConnection();
                case TipoDeBd.SQLite:
                    return new SQLite(conexion.CadenaDeConexion).TestConnection();
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Determina si una consulta retorna datos
        /// </summary>
        /// <param name="query">consulta</param>
        /// <param name="nombreConexion">nombre de la conexion</param>
        /// <returns>Verdadero si existieron registros</returns>
        public static bool Exists(string query,string nombreConexion = "")
        {
            return ExecuteQuery(query, nombreConexion).Rows.Count > 0;
        }

        /// <summary>
        /// Nombre de la conexión que se utilizará por defecto cuando no se pase un nombre a las funciones
        /// que ejecutan consultas.
        /// </summary>
        public static string DefaultConnectionName { get; set; } = "";

        /// <summary>
        /// Contiene todas las conexiónes
        /// </summary>
        public static ConexionCollection Conexiones = new ConexionCollection();

        /// <summary>
        /// El último codigo de error de conexión o consuta
        /// </summary>
        public static int LastErrorCode;

        /// <summary>
        /// El ultimo error de BD
        /// </summary>
        public static string LastErrorDescription;

        /// <summary>
        /// Ejecuta una consulta en la BD SAP o satelite
        /// </summary>
        /// <param name="query">Consulta</param>
        /// <param name="nombreConexion">Nombre de la conexión</param>
        /// <returns>DataTable con el resultado de la consulta</returns>
        public static DataTable ExecuteQuery(string query, string nombreConexion = "")
        {
           return GetExecutor(nombreConexion, query).ExecuteQuery();
        }
        internal static void bdExceptionHandler(DbException e,string query = "")
        {
        
            LastErrorCode = e.ErrorCode;
            LastErrorDescription = e.Message;
            var ex = new BdException("Error de base de datos: " + e.Message,query);
            var EvID = e.Data["HelpLink.EvtID"]?.ToString();//2 significa error de conexion 17142 significa servidor pausado
            if(EvID == "2" || EvID == "17142") ex.IsConnectionError = true;
            if (OnException == null)
                throw ex;
            else OnException.Invoke(null, new BdExeptionEventArgs(ex));
        }
        private static void bdExceptionHandler(Exception e,string query = "")
        {
            LastErrorDescription = e.Message;
            var ex = new BdException(e.Message,query);
            if (OnException == null)
                throw ex;
            else OnException.Invoke(null, new BdExeptionEventArgs(ex));
        }

        /// <summary>
        /// Ejecuta una consulta del tipo INSERT,DELETE,UPDATE
        /// </summary>
        /// <param name="query">Consulta</param>
        /// <param name="nombreConexion">Nombre de la conexión que se utilizará</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string query, string nombreConexion = "")
        {
            return GetExecutor(nombreConexion, query).ExecuteNonQuery();
        }
        /// <summary>
        /// Retorna el primer valor de la tabla resultante de ejecutar una consulta
        /// </summary>
        /// <param name="query">Consulta</param>
        /// <param name="nombreConexion">Nombre de la conexion a utilizar</param>
        /// <returns>Retorna nulo si no encuentra un valor</returns>
        public static object ExecuteEscalar(string query,string nombreConexion = "")
        {
            return GetExecutor(nombreConexion, query).ExecuteEscalar();
        }


        
        /// <summary>
        /// Obtiene el tipo de BD de la conexión
        /// </summary>
        /// <param name="connName">Nombre de la conexión</param>
        /// <returns></returns>
        public static TipoDeBd GetTipoBd(string connName)
        {
            var con = Conexiones.Find(connName);
            if (con == null) bdExceptionHandler(new Exception($"La conexión {connName} no existe."));
            return con.TipoDeBaseDeDatos;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToDateTimeSqlFormat(DateTime d) => d.ToString("yyyy-MM-dd HH:mm:ss.fff");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToDateSqlFormat(DateTime d) => d.ToString("yyyy-MM-dd");
        /// <summary>
        /// Obtiene la clase que se encarga de ejecutar la consulta
        /// </summary>
        /// <param name="nombreConexion"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static IBdExecutor GetExecutor(string nombreConexion)
        {
            var con = Conexiones.Find(nombreConexion);
            if (con == null) bdExceptionHandler(new Exception($"La conexión \"{nombreConexion}\" no existe."));
            switch (con.TipoDeBaseDeDatos)
            {
                case TipoDeBd.SqlServer:
                    return new SQLServer(con.CadenaDeConexion);
                case TipoDeBd.SQLite:
                    return new SQLite(con.CadenaDeConexion);
                default:
                    throw new NotImplementedException();
            }
        }
        private static IBdExecutor GetExecutor(string nombreConexion,string query)
        {
            var ex = GetExecutor(nombreConexion);
            ex.Query = query;
            return ex;
        }

        /// <summary>
        /// Genera un nuevo query builder
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <returns></returns>
        public static QueryBuilder Query(string tabla)
        {
            return new QueryBuilder(tabla);
        }
        /// <summary>
        /// consulta en raw
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static QueryBuilder RawQuery(string query)
        {
            return new QueryBuilder().RawQuery(query) as QueryBuilder;
        }
        /// <summary>
        /// Colección de Conexiones
        /// </summary>
        public class ConexionCollection : List<Conexion>
        {
            /// <summary>
            /// Agrega una nueva conexión
            /// </summary>
            /// <param name="con"></param>
           new public void Add(Conexion con)
            {
                if (con.Nombre == "") con.Nombre = $"Conexion{this.Count}";
                foreach(Conexion c in this)
                {
                    if (c.Nombre == con.Nombre) throw new Exception("El nombre de la conexión ya existe");
                }
                base.Add(con);
            }
            /// <summary>
            /// Obtiene la conexion por nombre
            /// </summary>
            /// <param name="nombreConexion"></param>
            /// <returns>Obtine la primera conexion que coincide con el nombre proporcionado o nulo si no existe</returns>
            public Conexion Find(string nombreConexion)
            {
                if (Count == 0) return null;
                if (string.IsNullOrEmpty(nombreConexion) && string.IsNullOrEmpty(DefaultConnectionName))
                        return Conexiones[0];
                return Find(c => c.Nombre == nombreConexion);
            }

        }
    }

}

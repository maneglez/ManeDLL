using Mane.BD.Executors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;

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
    public static partial class Bd
    {
        public static event EventHandler<BdExeptionEventArgs> OnException;
        public static bool AutoDisconnect
        {
            get => autoDisconnect; set
            {
                if (value == autoDisconnect) return;
                if (!value)
                {
                    foreach (var c in Conexiones)
                    {
                        c.Executor.AutoDisconnect = value;
                    }
                }
                autoDisconnect = value;
            }
        }        /// <summary>
                 /// <returns>Verdadero si logra conectar, falso si no</returns>
        public static bool TestConection(Conexion conexion)
        {
            conexion.TimeOut = 10;
            return conexion.Executor.TestConnection();
        }

        /// <summary>
        /// Determina si una consulta retorna datos
        /// </summary>
        /// <param name="query">consulta</param>
        /// <param name="nombreConexion">nombre de la conexion</param>
        /// <returns>Verdadero si existieron registros</returns>
        public static bool Exists(string query, string nombreConexion = "")
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
        private static bool autoDisconnect = true;

        /// <summary>
        /// Ejecuta una consulta en la BD SAP o satelite
        /// </summary>
        /// <param name="query">Consulta</param>
        /// <param name="nombreConexion">Nombre de la conexión</param>
        /// <returns>DataTable con el resultado de la consulta</returns>
        public static DataTable ExecuteQuery(string query, string nombreConexion = "")
        {
            DataTable dt = null;
            using(var executor = GetExecutor(nombreConexion, query))
            {
                dt = executor.ExecuteQuery();
            }
            return dt ?? new DataTable();
        }
        private static IBdExecutor GetExecutor(string nombreConexion, string query = "")
        {
            var ex = (Conexiones.Find(nombreConexion)?.NewExecutor()) ?? throw new BdException($"La conexion {nombreConexion} no existe");
            ex.Query = query;
            return ex;
        }
        internal static void bdExceptionHandler(DbException e, string query = "")
        {

            LastErrorCode = e.ErrorCode;
            LastErrorDescription = e.Message;
            var ex = new BdException("Error de base de datos: " + e.Message, query);
            var EvID = e.Data["HelpLink.EvtID"]?.ToString();//2 significa error de conexion 17142 significa servidor pausado
            if (EvID == "2" || EvID == "17142") ex.IsConnectionError = true;
            if (OnException == null)
                throw ex;
            else OnException.Invoke(null, new BdExeptionEventArgs(ex));
        }
        internal static void bdExceptionHandler(Exception e, string query = "")
        {
            LastErrorDescription = e.Message;
            var ex = new BdException(e.Message, query);
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
            int result = 0;
            using(var executor = GetExecutor(nombreConexion, query))
            {
                result = executor.ExecuteNonQuery();
            }
            return result;
        }
        /// <summary>
        /// Retorna el primer valor de la tabla resultante de ejecutar una consulta
        /// </summary>
        /// <param name="query">Consulta</param>
        /// <param name="nombreConexion">Nombre de la conexion a utilizar</param>
        /// <returns>Retorna nulo si no encuentra un valor</returns>
        public static object ExecuteEscalar(string query, string nombreConexion = "")
        {
            object val = null;
            using (var exec = GetExecutor(nombreConexion, query))
                val = exec.ExecuteEscalar();
            if (val == DBNull.Value)
                return null;
            return val;
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
        public static string ToDateTimeSqlFormat(this DateTime d) => d.ToString("yyyy-MM-dd HH:mm:ss.fff");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToDateSqlFormat(this DateTime d) => d.ToString("yyyy-MM-dd");


        /// <summary>
        /// Genera un nuevo query builder
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <returns></returns>
        public static QueryBuilder Query(string tabla = "")
        {
            return string.IsNullOrWhiteSpace(tabla) ? new QueryBuilder() : new QueryBuilder(tabla);
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
                Bd.AutoDisconnect = true;// establecer en verdadero cada que se agrega una conexion
                if (con.Nombre == "") con.Nombre = $"Conexion{this.Count}";
                foreach (Conexion c in this)
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
                if (string.IsNullOrWhiteSpace(nombreConexion) && string.IsNullOrWhiteSpace(DefaultConnectionName))
                    return Conexiones[0];
                else if (string.IsNullOrWhiteSpace(nombreConexion) && !string.IsNullOrWhiteSpace(DefaultConnectionName))
                    nombreConexion = DefaultConnectionName;
                return Find(c => c.Nombre == nombreConexion);
            }
            public bool TestAllConections()
            {
                foreach (var c in this)
                    if (!TestConection(c)) return false;
                return true;
            }

        }
        #region Persistencia de conexiones en archivo json
        /// <summary>
        /// Indica la contraseña para encriptar y desencriptar las credenciales de las conexiones a base de datos.
        /// Si no se establece la contraseña, no se encriptarán las contraseñas
        /// </summary>
        public static string JsonEncryptPassword { get; set; }
        /// <summary>
        /// Carga una colección de conexiones desde un json
        /// </summary>
        /// <param name="fileName">Archivo de conexiones</param>
        /// <returns>Colección de conexiones o coleccion vacía si no existe el archivo</returns>
        public static ConexionCollection LoadConnectionsFromFile(string fileName)
        {
            if (!File.Exists(fileName))
                return new ConexionCollection();
            var json = "";
            using (var sr = new StreamReader(fileName))
                json = sr.ReadToEnd();
            if (string.IsNullOrWhiteSpace(json))
                return new ConexionCollection();
            var conns = JsonConvert.DeserializeObject<ConexionCollection>(json);
            //Decriptar contraseñas si La clave de encriptado está seteada
            if (!string.IsNullOrWhiteSpace(JsonEncryptPassword))
                foreach (var c in conns)
                    c.Contrasena = Helpers.Crypto.Decriptar(c.Contrasena, JsonEncryptPassword);
            return conns;
        }
        /// <summary>
        /// Carga colección de conexiones desde archivo ManeBdConnections.json ubicado en la carpeta base del ejecutable
        /// </summary>
        /// <returns></returns>
        public static ConexionCollection LoadConnectionsFromFile()
        {
            return LoadConnectionsFromFile(DefaultConectionsFileName);
        }
        /// <summary>
        /// Guarda las conexiones en un archivo json
        /// </summary>
        /// <param name="conexiones">Lista de conexiones</param>
        /// <param name="fileName">nombre del archivo json</param>
        public static void SaveConnectionsToFile(ConexionCollection conexiones, string fileName)
        {
            //Encriptar contraseñas si La clave de encriptado está seteada
            if (!string.IsNullOrWhiteSpace(JsonEncryptPassword))
                foreach (var c in conexiones)
                    c.Contrasena = Helpers.Crypto.Encriptar(c.Contrasena, JsonEncryptPassword);
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            var json = JsonConvert.SerializeObject(conexiones, Formatting.Indented);
            using (var f = File.Create(fileName)) { }
            using (var sw = new StreamWriter(fileName))
                sw.Write(json);
        }
        /// <summary>
        /// Guarda las conexiones en el archivo ManeBdConnections.json ubicado en la carpeta base del ejecutable
        /// </summary>
        /// <param name="conexiones">Lista de conexiones</param>
        public static void SaveConnectionsToFile(ConexionCollection conexiones)
        {
            SaveConnectionsToFile(conexiones, DefaultConectionsFileName);
        }
        /// <summary>
        /// Guarda la colección de conexiones actual en el archivo por defecto
        /// </summary>
        public static void SaveConnectionsToFile()
        {
            SaveConnectionsToFile(Conexiones, DefaultConectionsFileName);
        }
        private static string defaultConectionsFileName;
        public static string DefaultConectionsFileName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(defaultConectionsFileName))
                    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ManeBdConnections.json");
                else return defaultConectionsFileName;
            }
            set => defaultConectionsFileName = value;
        }

        #endregion

    }



}

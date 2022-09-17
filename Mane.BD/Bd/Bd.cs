using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;



namespace Mane.BD
{
    #region Enums
    /// <summary>
    /// Tipos de bases de datos
    /// </summary>
    public enum TipoDeBd
    {
        /// <summary>
        /// Servidor SQL
        /// </summary>
        SqlServer
    }

    #endregion

    #region Clase Bd
    /// <summary>
    /// Base de datos. By ManeG
    /// </summary>
    /// <remarks>Conexiones compatibles:
    /// <list type="bullet">
    /// <item>SQL Server</item>
    /// </list>
    /// </remarks>
    public static class Bd
    {
        public static event EventHandler<BdExeptionEventArgs> OnException;
        private static bool TestConectionSQL(Conexion conexion)
        {
            var con = new SqlConnection(conexion.CadenaDeConexion);
            bool result = true;
            try
            {
                con.Open();
            }
            catch(SqlException e)
            {
                result = false;
                LastErrorCode = e.ErrorCode;
                LastErrorDescription = e.Message;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return result;
        }

        /// <summary>
        /// Prueba una conexión a base de datos
        /// </summary>
        /// <param name="conexion">Conexión a probar</param>
        /// <returns>Verdadero si logra conectar, falso si no</returns>
        public static bool TestConection(Conexion conexion)
        {
            switch (conexion.TipoDeBaseDeDatos)
            {
                case TipoDeBd.SqlServer:
                    return TestConectionSQL(conexion);
                default:
                    break;
            }
            return false;
        }

        /// <summary>
        /// Determina si una consulta retorna datos
        /// </summary>
        /// <param name="query">consulta</param>
        /// <param name="nombreConexion">nombre de la conexion</param>
        /// <returns>Verdadero si existieron registros</returns>
        public static bool exists(string query,string nombreConexion = "")
        {
            return executeQuery(query, nombreConexion).Rows.Count > 0;
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
        public static DataTable executeQuery(string query, string nombreConexion = "")
        {
            DataTable dt = new DataTable();
            switch (getTipoBd(nombreConexion))
            {
                case TipoDeBd.SqlServer:
                    return executeQueryForSQLServer(query, nombreConexion);
                default:
                    break;
            }
            return dt;
        }
        private static DataTable executeQueryForSQLServer(string query, string nombreConexion = "")
        {
            DataTable dt = new DataTable();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(getConnString(nombreConexion));
                cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection.Open();
                var reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            catch (SqlException e) {
                bdExceptionHandler(e, query);
            }
            catch (Exception e) {
                bdExceptionHandler(e, query);
            }
            finally
            {
                cmd?.Connection?.Close();
                cmd?.Connection?.Dispose();
                cmd?.Dispose();
                conn?.Dispose();
            }
            return dt;
        }
        private static void bdExceptionHandler(SqlException e,string query = "")
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
        public static int executeNonQuery(string query, string nombreConexion = "")
        {
            switch (getTipoBd(nombreConexion))
            {
                case TipoDeBd.SqlServer:
                    return executeNonQueryForSQLServer(query, nombreConexion);
                default:
                    break;
            }
            return 0;
        }
        /// <summary>
        /// Retorna el primer valor de la tabla resultante de ejecutar una consulta
        /// </summary>
        /// <param name="query">Consulta</param>
        /// <param name="nombreConexion">Nombre de la conexion a utilizar</param>
        /// <returns>Retorna nulo si no encuentra un valor</returns>
        public static object getFirstValue(string query,string nombreConexion = "")
        {
            switch (getTipoBd(nombreConexion))
            {
                case TipoDeBd.SqlServer:
                    return getFirstValueForSQLServer(query,nombreConexion);
                default:
                    break;
            }
            return null;
            
        }
        private static object getFirstValueForSQLServer(string query,string nombreConexion)
        {
            
           
            object result = null;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(getConnString(nombreConexion));
                cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection.Open();
                result = cmd.ExecuteScalar();
            }
            catch(SqlException sqlE)
            {
                bdExceptionHandler(sqlE, query);
            }
            catch (Exception e)
            {
                bdExceptionHandler(e, query);
            }
            finally
            {
                cmd?.Connection?.Close();
                cmd?.Connection?.Dispose();
                cmd?.Dispose();
                conn?.Dispose();
            }
            return result;
        }
        private static int executeNonQueryForSQLServer(string query, string nombreConexion = "")
        {
            int result = 0;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(getConnString(nombreConexion));
                cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Connection.Open();
               result = cmd.ExecuteNonQuery();
            }
            catch (SqlException e) {
                bdExceptionHandler(e, query);
                    }
            catch (Exception e)
            {
                bdExceptionHandler(e, query);
            
            }
            finally
            {
                cmd?.Connection?.Close();
                cmd?.Connection?.Dispose();
                cmd?.Dispose();
                conn?.Dispose();
            }
            return result;
        }

        private static string getConnString(string nombreConexion)
        {
            if (Conexiones.Count == 0)
                throw new Exception("No se tiene configurada ni una conexión a base de datos");
            if (nombreConexion == "")
                if (DefaultConnectionName == "")
                    return Conexiones[0].CadenaDeConexion;
                else nombreConexion = DefaultConnectionName;
            Conexion conexion = Conexiones.Find(c => c.Nombre == nombreConexion);
            if (conexion == null) throw new Exception($"La conexión \"{nombreConexion}\" no existe");
            return conexion.CadenaDeConexion;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string toDateTimeSqlFormat(DateTime d) => d.ToString("yyyy-MM-dd HH:mm:ss.fff");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string toDateSqlFormat(DateTime d) => d.ToString("yyyy-MM-dd");
        /// <summary>
        /// Obtiene el tipo de BD de la conexión
        /// </summary>
        /// <param name="connName">Nombre de la conexión</param>
        /// <returns></returns>
        public static TipoDeBd getTipoBd(string connName)
        {
            if (Conexiones.Count == 0) throw new QueryBuilderExeption("No existen conexiones a base de datos");
            Conexion con;
            if (connName == "")
            {
                if(DefaultConnectionName == "")
                con = Conexiones[0];
                else con = Conexiones.Find(DefaultConnectionName);
            }
            else con = Conexiones.Find(connName);
            if (con == null) throw new QueryBuilderExeption($"La conexión {connName} no existe.");
            return con.TipoDeBaseDeDatos;
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
        public static QueryBuilder rawQuery(string query)
        {
            return new QueryBuilder().rawQuery(query);
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
              return this.Find(c => c.Nombre == nombreConexion);
                
            }
        }
    }
    #endregion

  

 


}

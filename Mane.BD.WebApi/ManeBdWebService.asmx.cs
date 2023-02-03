using Mane.BD.Executors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace Mane.BD.WebApi
{
    /// <summary>
    /// Descripción breve de ApiWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]

    public class ApiWebService : WebService
    {
        
        public Usuario User;
        private static ConexionModel.ModeloCollection Conexiones;
        private static Dictionary<string, PermisosConexionModel> PermisoConexion;
        private void ServiceExceptionHandler(Exception ex, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            LogStr.Add(ex.ToString());
            ServiceExceptionHandler(ex?.Message, statusCode);            
        }
        private bool VerificarConsulta(string query,string connName)
        {
            try
            {
                bool verificar()
                {
                    var permiso = PermisoConexion[connName];
                    var lower = query.ToLower();
                    if (lower.Contains("truncate table")) return false;
                    if (permiso.PUpdate == 1 && permiso.PDelete == 1 && permiso.PInsert == 1) return true;
                    if (lower.Contains("update ") && permiso.PUpdate == 0) return false;
                    if (lower.Contains("delete from") && permiso.PDelete == 0) return false;
                    if (lower.Contains("insert into") && permiso.PInsert == 0) return false;
                    return true;
                }
                if (!verificar())
                {
                    ServiceExceptionHandler("Consulta no permitida", HttpStatusCode.Forbidden);
                    return false;
                }
                else return true;

            }
            catch (Exception e)
            {
                ServiceExceptionHandler(e);
                return false;
            }
        }
        private void VerificarConexiones()
        {
            if (Bd.Conexiones.Count > 0) return;
            var c = new Conexion
            {
                Servidor = HostingEnvironment.MapPath(@"~\bin\data\database.db"),
                TipoDeBaseDeDatos = TipoDeBd.SQLite,
                Nombre = NombreConexion.Local
            };
            Bd.Conexiones.Add(c);
            Conexiones = ConexionModel.All();
            PermisoConexion = new Dictionary<string, PermisosConexionModel>();
            foreach (var con in Conexiones)
            {
                PermisoConexion.Add(con.Nombre, con.Permisos());
                Bd.Conexiones.Add(con.ToConexion());
            }
        }
        private void ServiceExceptionHandler(string message, HttpStatusCode statusCode)
        {
            Context.Response.StatusDescription = message;
            Context.Response.StatusCode = (int)statusCode;
            Context.ApplicationInstance.CompleteRequest();
        }
        private bool VerifyUser()
        {
            if(User == null)
            {
                ServiceExceptionHandler("Usuario o contrasena incorrectos", HttpStatusCode.Unauthorized);
                return false;
            }
            try
            {
                VerificarConexiones();
                if (UsuarioModel.Query().Where("UserName", User.UserName).Where("Password", User.Password).Exists())
                    return true;
                ServiceExceptionHandler("Usuario o contrasena incorrectos", HttpStatusCode.Unauthorized);
                return false;
            }
            catch (Exception e)
            {
                ServiceExceptionHandler(e);
                return false;
            }
        }
        private static DataTable ParsetTable(DataTable dataTable = null)
        {
            if (dataTable == null)
                dataTable = new DataTable("tablita");
            else dataTable.TableName = "tablita";
            return dataTable;
        }
        [SoapHeader("User")]
        [WebMethod]
        public DataTable ExecuteQuery(string Query,string ConnName)
        {
            if (!VerifyUser())
                return ParsetTable();
            try
            {
                var conn = FindConnection(ConnName);
                if (conn == null) return ParsetTable();
                if (!VerificarConsulta(Query, ConnName))
                    return ParsetTable();
                
                var executor = conn.Executor;
                executor.Query = Query;
                return ParsetTable(executor.ExecuteQuery());
            }
            catch (Exception e)
            {
                ServiceExceptionHandler(e);
            }
            return ParsetTable();
        }
        [SoapHeader("User")]
        [WebMethod]
        public int ExecuteNonQuery(string Query, string ConnName)
        {
            if (!VerifyUser())
                return 0;
            try
            {
                var conn = FindConnection(ConnName);
                if (conn == null) return 0;
                if (!VerificarConsulta(Query, ConnName))
                    return 0;
                var executor = conn.Executor;
                executor.Query = Query;
                return executor.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ServiceExceptionHandler(e);
            }
            return 0;
        }
        [SoapHeader("User")]
        [WebMethod]
        public object ExecuteEscalar(string Query, string ConnName)
        {
            if (!VerifyUser())
                return null;
            try
            {
                var conn = FindConnection(ConnName);
                if (conn == null) return null;
                if (!VerificarConsulta(Query, ConnName))
                    return 0;
                var executor = conn.Executor;
                executor.Query = Query;
                return executor.ExecuteEscalar();
            }
            catch (Exception e)
            {
                ServiceExceptionHandler(e);
            }
            return null;
        }
        private Conexion FindConnection(string ConnName)
        {
            var conn = Bd.Conexiones.Find(ConnName);
            try
            {
                if (conn == null)
                {
                    var connM = ConexionModel.Query().Where("Nombre", ConnName).First();
                    if (connM == null)
                        throw new Exception($"La conexion con el nombre {ConnName} no se encuentra registrada");
                    conn = connM.ToConexion();
                    Bd.Conexiones.Add(conn);
                }
            }
            catch (Exception e)
            {
                ServiceExceptionHandler(e);
            }
            return conn;
        }
        [SoapHeader("User")]
        [WebMethod]
        public bool TestConnection(string ConnName)
        {
            if (!VerifyUser()) return false;
            var conn = FindConnection(ConnName);
            if (conn == null) return false;
            return conn.Executor.TestConnection();
        }
    }
}

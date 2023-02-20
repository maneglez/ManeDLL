using Mane.BD.BaseDeDatos.Executors.WebApiExecutor;
using Mane.BD.Executors;
using System;
using System.CodeDom;
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
        private WebApiResponse ServiceExceptionHandler(Exception ex, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            LogStr.Add(ex.ToString());
           return ServiceExceptionHandler(ex?.ToString(), statusCode);            
        }
        private void VerificarConsulta(string query, string connName)
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
                throw new Exception("Consulta no permitida");
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
        private WebApiResponse ServiceExceptionHandler(string message, HttpStatusCode statusCode)
        {
            return new WebApiResponse(null,statusCode,message);
        }
        private void VerifyUser()
        {
            if (User == null)
            {
                throw new Exception("Usuario o contraseña incorrectos");
            }
            VerificarConexiones();
            if (!UsuarioModel.Query().Where("UserName", User.UserName).Where("Password", User.Password).Exists())
                throw new Exception("Usuario o contraseña incorrectos");
        }
        private static DataTable ParseTable(DataTable dataTable = null)
        {
            if (dataTable == null)
                dataTable = new DataTable("tablita");
            else dataTable.TableName = "tablita";
            return dataTable;
        }
        [SoapHeader("User")]
        [WebMethod]
        public WebApiResponse ExecuteQuery(string Query,string ConnName)
        {
            try
            {
                VerifyUser(); 
            }
            catch (Exception e)
            {
                return ServiceExceptionHandler(e.Message, HttpStatusCode.Unauthorized);
            }
            try
            {
                var conn = FindConnection(ConnName);
                try
                {
                    VerificarConsulta(Query, ConnName);
                }
                catch (Exception e)
                {
                    return ServiceExceptionHandler(e.Message, HttpStatusCode.Forbidden);
                }
                
                var executor = conn.Executor;
                executor.Query = Query;
                var response = new WebApiResponse(null);
                response.DataTable = ParseTable(executor.ExecuteQuery());
                return response;
            }
            catch (Exception e)
            {
               return ServiceExceptionHandler(e);
            }
        }
        [SoapHeader("User")]
        [WebMethod]
        public WebApiResponse ExecuteNonQuery(string Query, string ConnName)
        {
            try
            {
                VerifyUser();
            }
            catch (Exception e)
            {
                return ServiceExceptionHandler(e.Message, HttpStatusCode.Unauthorized);
            }
            try
            {
                var conn = FindConnection(ConnName);
                try
                {
                    VerificarConsulta(Query, ConnName);
                }
                catch (Exception e)
                {
                    return ServiceExceptionHandler(e.Message, HttpStatusCode.Forbidden);
                }
                var executor = conn.Executor;
                executor.Query = Query;
                return new WebApiResponse(executor.ExecuteNonQuery());
            }
            catch (Exception e)
            {
               return ServiceExceptionHandler(e);
            }
        }
        [SoapHeader("User")]
        [WebMethod]
        public WebApiResponse ExecuteEscalar(string Query, string ConnName)
        {
            try
            {
                VerifyUser();
            }
            catch (Exception e)
            {
                return ServiceExceptionHandler(e.Message, HttpStatusCode.Unauthorized);
            }
            try
            {
                var conn = FindConnection(ConnName);
                try
                {
                    VerificarConsulta(Query, ConnName);
                }
                catch (Exception e)
                {
                    return ServiceExceptionHandler(e.Message, HttpStatusCode.Forbidden);
                }
                var executor = conn.Executor;
                executor.Query = Query;
                var result = executor.ExecuteEscalar();
                return new WebApiResponse(result == DBNull.Value ? null : result);
            }
            catch (Exception e)
            {
               return ServiceExceptionHandler(e);
            }
            
        }
        private Conexion FindConnection(string ConnName)
        {
            var conn = Bd.Conexiones.Find(ConnName);
                if (conn == null)
                {
                    var connM = ConexionModel.Query().Where("Nombre", ConnName).First();
                    if (connM == null)
                        throw new Exception($"La conexion con el nombre {ConnName} no se encuentra registrada");
                    conn = connM.ToConexion();
                    Bd.Conexiones.Add(conn);
                }

            return conn;
        }
        [SoapHeader("User")]
        [WebMethod]
        public WebApiResponse TestConnection(string ConnName)
        {
            try
            {
                VerifyUser();
            }
            catch (Exception e)
            {
                return new WebApiResponse(false, HttpStatusCode.Unauthorized, e.Message);
            }
            var conn = FindConnection(ConnName);
            if (conn == null) return new WebApiResponse(false,HttpStatusCode.OK,$"La conexión {ConnName} no se encuentra registrada");
            return new WebApiResponse(conn.Executor.TestConnection());
        }
    }
}

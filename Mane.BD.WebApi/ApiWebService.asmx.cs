using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
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
        private void ServiceExceptionHandler(Exception ex, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            ServiceExceptionHandler(ex?.Message, statusCode);            
        }
        private void ServiceExceptionHandler(string message, HttpStatusCode statusCode)
        {
            Context.Response.Status = message;
            Context.Response.StatusCode = (int)statusCode;
            Context.ApplicationInstance.CompleteRequest();
        }
        private bool VerifyUser(Usuario User)
        {
            try
            {
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
        [SoapHeader("Usuario")]
        [WebMethod]
        public DataTable ExecuteQuery(string Query,string ConnName)
        {
            if(!VerifyUser())
            try
            {
                var conn = FindConnection(ConnName);
                if (conn == null) return new DataTable();
                var executor = conn.Executor;
                executor.Query = Query;
                return executor.ExecuteQuery();
            }
            catch (Exception e)
            {
                ServiceExceptionHandler(e);
            }
            return new DataTable();
        }
        [SoapHeader("Usuario")]
        [WebMethod]
        public int ExecuteNonQuery(string Query, string ConnName)
        {
            try
            {
                var conn = FindConnection(ConnName);
                if (conn == null) return 0;
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
        [SoapHeader("Usuario")]
        [WebMethod]
        public object ExecuteEscalar(string Query, string ConnName)
        {
            try
            {
                var conn = FindConnection(ConnName);
                if (conn == null) return null;
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
    }
}

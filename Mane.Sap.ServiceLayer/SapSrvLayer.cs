using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;

namespace Mane.Sap.ServiceLayer
{
    /// <summary>
    /// Clase para ejecutar consultas con service layer SAP
    /// </summary>
    public class SapSrvLayer
    {
        /// <summary>
        /// Arregla error de SSL, entre otros errores
        /// </summary>
        public static void FixSslError()
        {
            if (SSLFixed) return;
            ServicePointManager.ServerCertificateValidationCallback +=
            (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.Expect100Continue = false;
            SSLFixed = true;
        }
        private static bool SSLFixed = false;
        private static string LastError;
        /// <summary>
        /// Obtiene el ultimo error generado
        /// </summary>
        /// <returns></returns>
        public static string GetLastError() => LastError;
        /// <summary>
        /// Crea un nuevo documento para insertar en SAP
        /// </summary>
        /// <returns></returns>
      //  public static IDocumentoCreate NewDocumento() => new Documento();
        /// <summary>
        /// Lista de conexiones disponibles para utilizar en SAP
        /// </summary>
        public static ConexionSrvLayerCollection Conexiones { get; set; } = new ConexionSrvLayerCollection();
        /// <summary>
        /// Ejecuta una consulta con metodo GET
        /// </summary>
        /// <param name="query">Consulta</param>
        /// <param name="connName">Nombre de la conexion, por defecto toma la del indice 0</param>
        /// <returns></returns>
        public static SLResponse GET(string query, string connName = "")
        {
            return ExecuteQuery(query, SLMethod.GET, connName);
        }
        /// <summary>
        /// Ejecuta una consulta con metodo POST
        /// </summary>
        /// <param name="query">consulta</param>
        /// <param name="body">Objeto que se convertirá a json y se utilizará como cuerpo</param>
        /// <param name="connName">Nombre de la conexion por defecto toma la primera</param>
        /// <returns></returns>
        public static SLResponse POST(string query,object body, string connName = "")
        {
            return ExecuteQuery(query, SLMethod.POST, connName,body);
        }
        /// <summary>
        /// Ejecuta una consulta con metodo PATCH
        /// </summary>
        /// <param name="query">consulta</param>
        /// <param name="body">Objeto que se convertirá a json y se utilizará como cuerpo</param>
        /// <param name="connName">Nombre de la conexion por defecto toma la primera conexión</param>
        /// <returns></returns>
        public static SLResponse PATCH(string query, object body, string connName = "")
        {
            return ExecuteQuery(query, SLMethod.PATCH, connName, body);
        }

        public static SLResponse UpdateObject(BoObjectTypes tipoObjeto,object objectID,Dictionary<string,object> values,string connName = "")
        {
            
            return PATCH(Helper.GetObjectQuery(tipoObjeto,objectID), values, connName);
        }

        /// <summary>
        /// Ejecuta una consulta
        /// </summary>
        /// <param name="query"></param>
        /// <param name="method"></param>
        /// <param name="connName"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Cuando la consulta o la conexión falla</exception>
        public static SLResponse ExecuteQuery(string query, SLMethod method,string connName = "",object body = null)
        {
            var con = FindConection(connName);
            var response = con.ExecuteQuery(query, method,body);
            if (response == null)
            {
                LastError = con.GetLastError();
                throw new Exception(LastError);
            }
            return response;
        }

        private static ConexionSrvLayer FindConection(string connName)
        {
            try
            {
                ConexionSrvLayer con = null;
                if (string.IsNullOrEmpty(connName))
                {
                    if (Conexiones.Count == 0)
                        throw new Exception("No hay conexiones disponibles");
                    else
                        con = Conexiones.First();
                }
                else
                {
                    con = Conexiones.Find(connName);
                    if (con == null)
                        throw new Exception($"La conexion '{connName}' no existe");
                }
                return con;
            }
            catch (Exception e)
            {
                LastError = e.Message;
                throw e;
                //if (OnException == null)
                //    throw e;
                //else OnException.Invoke(e, null);
            }
           
        }
        /// <summary>
        /// Se desencadena cuando ocurre una excepción
        /// </summary>
        public static event EventHandler OnException;
    }
    
}

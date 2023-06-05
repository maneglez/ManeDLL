using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Mane.Sap.ServiceLayer
{
    /// <summary>
    /// Conexion Service Layer
    /// </summary>
    public class ConexionSrvLayer
    {
        /// <summary>
        /// Versión de la API de Services Layer (por defecto "v1")
        /// </summary>
        public string ApiVersion { get; set; } = "v1";
        /// <summary>
        /// Identificador de conexion
        /// </summary>
        public string Nombre { get; set; } = "";
        /// <summary>
        /// Nombre o ip del servidor
        /// </summary>
        public string Server { get; set; } = "localhost";
        /// <summary>
        /// Puerto, Por defecto 50000
        /// </summary>
        public int Port { get; set; } = 50000;
        public string Protocolo { get; set; } = "https";
        /// <summary>
        /// Usuario SAP
        /// </summary>
        public string User { get; set; } = "";
        /// <summary>
        /// Contraseña SAP
        /// </summary>
        public string Password { get; set; } = "";
        /// <summary>
        /// Base de Datos SAP
        /// </summary>
        public string CompanyDB { get; set; } = "";
        /// <summary>
        /// Tiempo de espera en segundos
        /// </summary>
        public int TimeOut { get; set; }
        /// <summary>
        /// Indica si la conexión está establecida
        /// </summary>
        public bool Connected
        {
            get
            {
                if (string.IsNullOrEmpty(_SessionID)) return false;
                if ((DateTime.Now - LastConection).TotalMinutes >= 30)
                {
                    _SessionID = "";
                    return false;
                }
                return true;
            }
        }
        private string URL => $"{Protocolo}://{Server}" + (Port > 0 ? $":{Port}" : "") + $"/b1s/{ApiVersion}/";
        private string LoginJson => $@"{{""CompanyDB"":""{CompanyDB}"",""Password"": ""{Password}"",""UserName"": ""{User}""}}";
        private string _SessionID;
        /// <summary>
        /// Identificador de la sesión
        /// </summary>
        public string SessionID => _SessionID;
        /// <summary>
        /// Route ID de la Sesión
        /// </summary>
        public string RouteID => _RouteID;
        private DateTime LastConection;
        private string LastError;
        private string _RouteID;

        /// <summary>
        /// Obtiene el ultimo error generado
        /// </summary>
        /// <returns>cadena</returns>
        public string GetLastError() => LastError;

        /// <summary>
        /// Abre una conexión y Ejecuta una Consulta
        /// </summary>
        /// <param name="query">Consulta</param>
        /// <param name="method">Metodo</param>
        /// <param name="body">Cuerpo o json de la consulta</param>
        /// <returns></returns>
        public SLResponse ExecuteQuery(string query, SLMethod method, object body = null)
        {
            if (!Connected)
                Connect();
            
            IRestResponse resp = null;
            var cli = new RestClient(URL + query);
            var req = new RestRequest();
            req.Method = (Method)method;
            req.Timeout = TimeOut * 1000;
            req.AddParameter("B1SESSION", SessionID, ParameterType.Cookie);
            req.AddParameter("ROUTEID", RouteID, ParameterType.Cookie);
            if (body != null)
            {
                req.AddHeader("Content-Type", "application/json");
                req.AddJsonBody(body);
            }
            try
            {
                resp = cli.Execute(req);
            }
            catch (Exception e)
            {
                LastError = e.Message;
                throw;
            }
            return new SLResponse(resp);
        }

        /// <summary>
        /// Establece una conexión si no existe una conexión abierta
        /// </summary>
        /// <exception cref="Exception">Lanza excepción si no logra conectar</exception>
        public void Connect()
        {
            SapSrvLayer.FixSslError();
            if (Connected) return;
            IRestResponse resp = null;
            var cli = new RestClient(URL + "Login");
            var req = new RestRequest(Method.POST);
                req.Timeout = TimeOut * 1000;
                req.AddHeader("Content-Type", "application/json");
                req.AddJsonBody(LoginJson);
                try
                {
                    resp = cli.Execute(req);
                }
                catch (Exception e)
                {
                    LastError = e.Message;
                    throw e;
                }
            if (resp.StatusCode != HttpStatusCode.OK)
            {
                _SessionID = "";
                if (string.IsNullOrEmpty(resp.Content))
                    LastError = resp.ErrorException?.Message;
                else
                    LastError = resp.Content;
                throw new Exception($"Error al conectar vía Services Layer: " + LastError);
            }
            var jsonOb = JObject.Parse(resp.Content);
            _SessionID = jsonOb["SessionId"].ToString();
            _RouteID = resp.Cookies.ToList().Find(c => c.Name == "ROUTEID")?.Value;
            LastConection = DateTime.Now;
        }
        /// <summary>
        /// Finaliza una conexion
        /// </summary>
        public void Disconnect()
        {
            ExecuteQuery("Logout", SLMethod.POST);
            _SessionID = "";
        }




    }
}

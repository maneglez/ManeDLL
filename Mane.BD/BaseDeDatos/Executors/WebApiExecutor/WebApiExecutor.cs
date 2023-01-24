using Mane.BD.Executors;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD.BaseDeDatos.Executors.WebApiExecutor
{
    internal class WebApiExecutor : IBdExecutor
    {
        private static List<WebApiToken> Tokens = new List<WebApiToken>();

        private WebApiToken CurrentToken => Tokens.Find(t => t.ConnectionName == ConnectionName);
        private Conexion Conexion;

        public WebApiExecutor(Conexion conexion)
        {
            Conexion = conexion;
        }

        public string Query { get; set; }
        public string ConnString { get; set; }
        public string ConnectionName { get; set; }
        public bool AutoDisconnect { get; set; }
        private WebApiResponse POST(string query,object body)
        {
            var cli = new RestClient(Conexion.CadenaDeConexion + query);
            var req = new RestRequest(Method.POST);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Accept", "application/json");
            req.AddHeader("Authorization", "Bearer " + CurrentToken?.Token);
            req.AddJsonBody(body);
            cli.Timeout = Conexion.TimeOut;
            var resp = cli.Execute(req);
            if (!resp.IsSuccessful)
                throw new Exception(resp.ErrorMessage + resp.Content);
            return resp.Content.JsonToObject<WebApiResponse>();
        }
       
        public void Connect()
        {
           if(CurrentToken == null)
            {
                var resp = POST("login", new { name = Conexion.Usuario, password = Conexion.Contrasena });
                Tokens.Add(new WebApiToken(Conexion.Nombre,resp.GetDataValue("user_token").ToString()));
            }
        }

        public void Disconnect()
        {
            //throw new NotImplementedException();
        }

        public object ExecuteEscalar()
        {
            var dt = ExecuteQuery();
            if (dt.Rows.Count == 0) return null;
            return dt.Rows[0][0];
        }

        public int ExecuteNonQuery()
        {
            Connect();
            var q = Query.ToLower();
            var tipo = "";
            if (q.Contains("delete "))
                tipo = "delete";
            else if (q.Contains("update "))
                tipo = "update";
            if (string.IsNullOrEmpty(tipo))
                throw new Exception("La consulta debe de ser del tipo delete o update: " + Query);
           return Convert.ToInt32(POST("execute/query", new WebApiQuery(Query, tipo, Conexion.Nombre)).data);
        }

        public DataTable ExecuteQuery()
        {
            return POST("execute/query", new WebApiQuery(Query, "select", Conexion.Nombre)).data.JsonToObject<DataTable>();
           
        }

        public bool TestConnection()
        {
            try
            {
                Connect();
            }
            catch (Exception)
            {
                return false;
            }
            return CurrentToken == null;
        }
    }
}

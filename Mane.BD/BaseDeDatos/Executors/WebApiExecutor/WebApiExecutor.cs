using Mane.BD.Executors;
using Mane.BD.QueryBulder.Builders;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;

namespace Mane.BD.BaseDeDatos.Executors.WebApiExecutor
{
    internal class WebApiExecutor : IBdExecutor
    {
        private static List<WebApiToken> Tokens = new List<WebApiToken>();

        private WebApiToken CurrentToken
        {
            get => Tokens.Find(t => t.ConnectionName == Conexion.Nombre); set
            {
                if (CurrentToken == null)
                    Tokens.Add(value);
                else CurrentToken.Token = value.Token;
            }
        }
        private Conexion Conexion;

        public WebApiExecutor(Conexion conexion)
        {
            Conexion = conexion;
        }

        public string Query { get; set; }
        public string ConnString { get; set; }
        public string ConnectionName { get; set; }
        public bool AutoDisconnect { get; set; }
        private WebApiResponse POST(string query, object body)
        {
            var cli = new RestClient(Conexion.Servidor + query);
            var req = new RestRequest(Method.POST);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Accept", "application/json");
            req.AddHeader("Authorization", "Bearer " + CurrentToken?.Token);
            req.AddJsonBody(body);
            cli.Timeout = Conexion.TimeOut * 1000;
            var resp = cli.Execute(req);
            if (!resp.IsSuccessful)
                throw new Exception(resp.ErrorMessage + resp.Content);
            return WebApiResponse.Parse(resp.Content);
        }

        public void Connect()
        {
            if (CurrentToken == null)
            {
                var resp = POST("login", new { name = Conexion.Usuario, password = Conexion.Contrasena });
                CurrentToken = new WebApiToken(Conexion.Nombre, resp.GetDataValue("user_token").ToString());
            }
        }

        public void Disconnect()
        {
            //throw new NotImplementedException();
        }

        public object ExecuteEscalar()
        {
            DataTable dt;
            var lower = Query.ToLower();
            if (lower.Contains("insert into"))//Ajustar la consulta quintando la parte que consulta el id insertado
            {
                switch (Conexion.SubTipoDeBD)
                {
                    case TipoDeBd.SqlServer:
                        Query = Query.Replace(new BuilderSQL(null).SelectLastInsertedIndexQuery, "");
                        break;
                    case TipoDeBd.SQLite:
                        Query = Query.Replace(new BuilderSQLite(null).SelectLastInsertedIndexQuery, "");
                        break;
                    case TipoDeBd.Hana:
                        Query = Query.Replace(new BuilderHana(null).SelectLastInsertedIndexQuery, "");
                        break;
                    default:
                        break;
                }
                Query = Query.Trim().TrimEnd(new char[] { ';' });
                dt = POST("execute/query", new WebApiQuery(Query, "insert_id", Conexion.Nombre)).data.JsonToObject<DataTable>();

            }
            else
                dt = ExecuteQuery();
            if (dt == null) return null;
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
            else if (q.Contains("insert into"))
                tipo = "insert";
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
            return CurrentToken != null;
        }
    }
}

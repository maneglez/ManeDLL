using Mane.BD.Executors;
using System;
using System.Data;

namespace Mane.BD.BaseDeDatos.Executors.WebApiExecutor
{
    internal class WebApiExecutor : IBdExecutor
    {
        private Conexion Conexion;
        WebServiceBd.ApiWebService Client;
        public WebApiExecutor(Conexion conexion)
        {
            Conexion = conexion;
            if (Conexion.TimeOut < 30) Conexion.TimeOut = 30;
            Client = new WebServiceBd.ApiWebService
            {
                Timeout = Conexion.TimeOut * 1000,
                UsuarioValue = new WebServiceBd.WebApiUser
                {
                    UserName = conexion.Usuario,
                    Password = conexion.Contrasena
                },
                Url = conexion.Servidor
            };

        }

        public string Query { get; set; }
        public string ConnString { get; set; }
        public bool AutoDisconnect { get; set; }


        public void Connect()
        {

        }

        public void Disconnect()
        {
            //throw new NotImplementedException();
        }

        public object ExecuteEscalar()
        {
            try
            {
                return ValidarResponse<object>(Client.ExecuteEscalar(Query, Conexion.Nombre));
            }
            catch (Exception e)
            {
                Bd.bdExceptionHandler(e, Query);
            }
            return null;
        }

        public int ExecuteNonQuery()
        {
            try
            {
                return ValidarResponse<int>(Client.ExecuteNonQuery(Query, Conexion.Nombre));

            }
            catch (Exception e)
            {
                Bd.bdExceptionHandler(e, Query);
            }
            return 0;
        }

        public DataTable ExecuteQuery()
        {
            try
            {
                var resp = Client.ExecuteQuery(Query, Conexion.Nombre);
                if (resp.EstatusCode == WebServiceBd.HttpStatusCode.OK)
                    return resp.DataTable;
                else throw new Exception($"{(int)resp.EstatusCode} - {resp.Message}");
            }
            catch (Exception e)
            {
                Bd.bdExceptionHandler(e, Query);
            }
            return new DataTable();
        }

        public bool TestConnection()
        {
            try
            {
                var response = Client.TestConnection(Conexion.Nombre);
                var result = ValidarResponse<bool>(response);
                if (!result)
                {
                    Bd.LastErrorCode = (int)response.EstatusCode;
                    Bd.LastErrorDescription = response.Message;
                }
                return result;
            }
            catch (Exception e)
            {
                Bd.bdExceptionHandler(e, Query);
            }
            return false;
        }
        private T ValidarResponse<T>(WebServiceBd.WebApiResponse resp)
        {
            if (resp.EstatusCode == WebServiceBd.HttpStatusCode.OK)
                return (T)resp.Data;
            else throw new Exception($"{(int)resp.EstatusCode} - {resp.Message}");
        }
        public void Dispose()
        {
            Disconnect();
            GC.SuppressFinalize(this);
        }
    }
}

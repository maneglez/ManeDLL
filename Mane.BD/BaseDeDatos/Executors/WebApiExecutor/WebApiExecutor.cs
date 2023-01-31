using Mane.BD.Executors;
using Mane.BD.QueryBulder.Builders;
using System;
using System.Collections.Generic;
using System.Data;

namespace Mane.BD.BaseDeDatos.Executors.WebApiExecutor
{
    internal class WebApiExecutor : IBdExecutor
    {


        private Conexion Conexion;
        WebServiceBd.BdWebService Client;
        public WebApiExecutor(Conexion conexion)
        {
            Conexion = conexion;
            Client = new WebServiceBd.BdWebService
            {
                Timeout = Conexion.TimeOut * 1000,
                UsuarioValue = new WebServiceBd.Usuario
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
            return Client.ExecuteEscalar(Query, Conexion.Nombre); 
        }

        public int ExecuteNonQuery()
        {
            return Client.ExecuteNonQuery(Query, Conexion.Nombre); ;
        }

        public DataTable ExecuteQuery()
        {
            return Client.ExecuteQuery(Query, Conexion.Nombre); ;
        }

        public bool TestConnection()
        {            
            return Client.TestConnection(Conexion.Nombre);
        }
    }
}

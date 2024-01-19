using Mane.Sap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CommonModules
{
    public class Conection 
    {
        public string ServerNameSql  { get; set; }
        public string ServerNameSap  { get; set; }
        public string Driver  { get; set; }
        public int Port  { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public string SapUser { get; set; }
        public string SapPassword { get; set; }
        public string SapSqlServerType { get; set; }
        public string DbName { get; set; }

        internal BD.Conexion ToDbConnection()
        {
            return new BD.Conexion
            {
                Nombre = Configuration.ConnectionName,
                NombreBD = DbName,
                Servidor = ServerNameSql,
                Puerto = Port,
                Usuario = DbUser,
                Contrasena = DbPassword,
                Driver = Driver,
                TipoDeBaseDeDatos = SapSqlServerType.ToLower().Contains("hana") ? BD.TipoDeBd.Hana : BD.TipoDeBd.SqlServer
            };
        }
        internal ConexionSap ToSapConnection()
        {
            return new ConexionSap
            {
                Server = ServerNameSap,
                DbCompany = DbName,
                User = SapUser,
                Password = SapPassword,
                TipoServidor = (TipoServidorSap)Enum.Parse(typeof(TipoServidorSap), SapSqlServerType),
                Nombre = Configuration.ConnectionName
            };
        }

    }
}

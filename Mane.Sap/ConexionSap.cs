using System.IO;
using System.Xml.Serialization;
using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;

namespace Mane.Sap
{
    public class ConexionSap : IConexionSap
    {
        private string password;
        private TipoServidorSap tipoServidor;
        private string nombre;
        private string server;
        private string licenseServer;
        private string sLDServer;
        private string dbUser;
        private string dbPassword;
        private string dbCompany;
        private string user;

        public ConexionSap()
        {
            Nombre = Server = LicenseServer = SLDServer = DbUser = DbPassword = DbCompany = User = Password = "";
            tipoServidor = TipoServidorSap.dst_MSSQL;
            AlternativeUsers = new List<SapUser>();
        }
        public ConexionSap Copy()
        {
            return new ConexionSap
            {
                Nombre = Nombre,
                Server = Server,
                LicenseServer = LicenseServer,
                SLDServer = SLDServer,
                DbUser = DbUser,
                DbPassword = DbPassword,
                DbCompany = DbCompany,
                User = User,
                Password = Password,
                TipoServidor = TipoServidor,
                AlternativeUsers = AlternativeUsers.ToList()
            };
        }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Server { get => server; set => server = value; }
        public string LicenseServer { get => licenseServer; set => licenseServer = value; }
        public string SLDServer { get => sLDServer; set => sLDServer = value; }
        public string DbUser { get => dbUser; set => dbUser = value; }
        public string DbPassword { get => dbPassword; set => dbPassword = value; }
        public string DbCompany { get => dbCompany; set => dbCompany = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }

        public TipoServidorSap TipoServidor { get => tipoServidor; set => tipoServidor = value; }
        public List<SapUser> AlternativeUsers { get; set; }

        public void Serialize(string xmlPath)
        {
            //Serializar
            string xml = "";
            XmlSerializer serializer = new XmlSerializer(GetType());
            using (StringWriter sw = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sw))
                {
                    serializer.Serialize(writer, this);
                    xml = sw.ToString();
                }
            }
            //Escribir en archivo

            string dir = Path.GetDirectoryName(xmlPath);
            Directory.CreateDirectory(dir);
            if (!File.Exists(xmlPath))
            {
                using (FileStream fs = File.Create(xmlPath)) { }
            }
            using (StreamWriter sw = new StreamWriter(xmlPath, true))
                sw.Write(xml);
        }

        /// <summary>
        /// Deserializa un objeto de conexión ubicado en la ruta
        /// </summary>
        /// <param name="xmlPath">Ruta hacia al xml De la conexion</param>
        /// <returns>Conexion deserializada</returns>
        public static ConexionSap Deserilize(string xmlPath)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(ConexionSap));
            ConexionSap con = null;
                using (StreamReader sr = new StreamReader(xmlPath))
                    con = (ConexionSap)serializer.Deserialize(sr);
            return con;
        }

    }


}

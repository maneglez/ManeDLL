using Mane.BD;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Sap
{
    public static partial class Sap
    {
        private static ConexionSap ConexionActual;
        private const string NombreConexionBd = "Mane.Sap_Connection";
        public static bool AutoDisconect { get; set; } = true;
        private static Company comp;
        public static Company Company => comp;
        public static ConexionSapCollection Conexiones = new ConexionSapCollection();
        public static string LastError { get; private set; }
        public static string NewObjectKey { get; private set; }
        /// <summary>
        /// Inicia una conexión con SAP
        /// </summary>
        /// <param name="nombreConexion">Nombre de la conexión</param>
        /// <returns></returns>
        public static bool connect(string nombreConexion = "")
        {
            try
            {
                if (comp == null) comp = new Company();
                if (comp.Connected && ConexionActual?.Nombre == nombreConexion) return true;
                else
                {
                    disconnect();
                }
                if (Conexiones.Count == 0) throw new Exception("No hay ni una conexion configurada");
                ConexionSap con;
                if (nombreConexion == "") con = Conexiones[0];
                else
                {
                    con = Conexiones.Find(nombreConexion);
                    if (con == null) throw new Exception($"La conexion {nombreConexion} no existe");
                }
                ConexionActual = con;
                comp.Server = con.Server;
                if (con.LicenseServer != "")
                    comp.LicenseServer = con.LicenseServer;
                if (con.SLDServer != "")
                    comp.SLDServer = con.SLDServer;
                comp.CompanyDB = con.DbCompany;
                comp.DbUserName = con.DbUser;
                comp.DbPassword = con.DbPassword;
                comp.UserName = con.User;
                comp.Password = con.Password;
                comp.DbServerType = (BoDataServerTypes)con.tipoServidor;
                comp.language = BoSuppLangs.ln_Spanish_La;
                comp.UseTrusted = false;
                if (comp.Connect() != 0) throw new Exception(comp.GetLastErrorDescription());
            }
            catch (Exception e)
            {
                LastError = e.Message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// Prueba una conexión a SAP
        /// </summary>
        /// <param name="con">Conexion a SAP</param>
        /// <returns></returns>
        public static bool TestConnection(ConexionSap con)
        {
            try
            {
                if (comp == null) comp = new Company();
                if (comp.Connected) disconnect();

                comp.Server = con.Server;
                if (con.LicenseServer != "")
                    comp.LicenseServer = con.LicenseServer;
                if (con.SLDServer != "")
                    comp.SLDServer = con.SLDServer;
                comp.CompanyDB = con.DbCompany;
                comp.DbUserName = con.DbUser;
                comp.DbPassword = con.DbPassword;
                comp.UserName = con.User;
                comp.Password = con.Password;
                comp.DbServerType = (BoDataServerTypes)con.tipoServidor;
                comp.language = BoSuppLangs.ln_Spanish_La;
                comp.UseTrusted = false;
                if (comp.Connect() != 0) throw new Exception(comp.GetLastErrorDescription());
                disconnect();

            }
            catch (Exception e)
            {
                LastError = e.Message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// Obtiene el DocNum a partir del doc Entry
        /// </summary>
        /// <param name="tabla">Nombre de la tabla de documento</param>
        /// <param name="docEntry">DocEntry</param>
        /// <returns></returns>
        public static int GetDocNum(string tabla,int docEntry = 0)
        {
            if (string.IsNullOrEmpty(NewObjectKey) && docEntry == 0) return 0;
            if (docEntry == 0) docEntry = Convert.ToInt32(NewObjectKey);
            var c = ConexionActual?.ToBdCon();
            c.Nombre = NombreConexionBd;
            Bd.Conexiones.Add(c);
            int docNum = Convert.ToInt32(Bd.Query(tabla).Where("DocEntry", docEntry).GetScalar(NombreConexionBd));
            Bd.Conexiones.Remove(c);
            return docNum;
        }
        /// <summary>
        /// Finaliza la conexión con sap
        /// </summary>
        public static void disconnect()
        {
            if (comp == null) return;
            if (!comp.Connected) return;
            comp.Disconnect();
            GC.Collect();
        }
        /// <summary>
        /// Colección de conexiones a SAP
        /// </summary>
        public class ConexionSapCollection : List<ConexionSap>
        {
            /// <summary>
            /// Agrega una nueva conexión
            /// </summary>
            /// <param name="con"></param>
            new public void Add(ConexionSap con)
            {
                if (con.Nombre == "") con.Nombre = $"Conexion{this.Count}";
                else
                    foreach (ConexionSap c in this)
                    {
                        if (c.Nombre == con.Nombre) throw new Exception("El nombre de la conexión ya existe");
                    }
                base.Add(con);
                
            }
            /// <summary>
            /// Obtiene la conexion por nombre
            /// </summary>
            /// <param name="nombreConexion"></param>
            /// <returns>Obtine la primera conexion que coincide con el nombre proporcionado</returns>
            public ConexionSap Find(string nombreConexion)
            {
                return this.Find(c => c.Nombre == nombreConexion);

            }

        }

    }

    public class ConexionSap
    {
        public string Nombre,
            Server,
            LicenseServer,
            SLDServer,
            DbUser,
            DbPassword,
            DbCompany,
            User,
            Password;
        public TipoServidorSap tipoServidor;
        public ConexionSap()
        {
            Nombre = Server = LicenseServer = SLDServer = DbUser = DbPassword = DbCompany = User = Password = "";
            tipoServidor = TipoServidorSap.dst_MSSQL;
        }
        /// <summary>
        /// Genera una objeto de clase Mane.BD.Bd.Conexion a partir de una clase ConexionSap
        /// </summary>
        /// <returns>Mane.BD.Bd.Conexion</returns>
        public Conexion ToBdCon()
        {
            var c = new Conexion();
            c.Servidor = Server;
            c.NombreBD = DbCompany;
            c.TipoDeBaseDeDatos = TipoDeBd.SqlServer;
            c.Usuario = DbUser;
            c.Contrasena = DbPassword;
            return c;
        }
    }

   
}

using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Sap
{
    public static class Sap
    {
        private static Company comp;
        public static CompanyCollection Conexiones = new CompanyCollection();
        public static string LastError { get; private set; }
        public static string NewObjectKey { get; private set; }
        public static bool connect(string nombreConexion = "")
        {
            try
            {
                if (comp == null) comp = new Company();
                if (comp.Connected) disconnect();
                if (Conexiones.Count == 0) throw new Exception("No hay ni una conexion configurada");
                ConexionSap con;
                if (nombreConexion == "") con = Conexiones[0];
                else
                {
                    con = Conexiones.Find(nombreConexion);
                    if (con == null) throw new Exception($"La conexion {nombreConexion} no existe");
                }
                comp.Server = con.Server;
                if (con.LicenseServer != "")
                    comp.LicenseServer = con.LicenseServer;
                if (con.SLDServer != "")
                    comp.SLDServer = con.SLDServer;
                comp.DbUserName = con.DbUser;
                comp.DbPassword = con.DbPassword;
                comp.UserName = con.User;
                comp.Password = con.Password;
                comp.DbServerType = (BoDataServerTypes)con.tipoServidor;
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
        /// Copia un documento a otro
        /// </summary>
        /// <param name="BaseEntry">DocEntry del documento del que se desea copiar</param>
        /// <param name="BaseType">Tipo de documento del que se desea copiar</param>
        /// <param name="TargetType">Tipo de documento destino</param>
        /// <param name="thisLinesOnly">(Opcional) indicar cuales lineas copiar</param>
        /// <returns>Verdadero si jaló falso si no jaló</returns>
        public static bool CopyDocuments(int BaseEntry, BoObjectTypes BaseType, BoObjectTypes TargetType, int[] thisLinesOnly = null)
        {
            try
            {
                if (!connect()) throw new Exception("Error de conexion: " + LastError);
                Documents docTarget = comp.GetBusinessObject(TargetType),
                    docBase = comp.GetBusinessObject(BaseType);
                if (!docBase.GetByKey(BaseEntry)) throw new Exception($"No se encontró el documento con la entrada {BaseEntry}");
                var lnBase = docBase.Lines; // Lineas de entrega
                var lnTarget = docTarget.Lines;// lineas de devolucion

                if (thisLinesOnly == null)
                {

                    for (int i = 0; i < lnBase.Count; i++)
                    {
                        lnBase.SetCurrentLine(i);
                        lnTarget.SetCurrentLine(i);
                        lnTarget.BaseType = (int)docBase.DocObjectCode;
                        lnTarget.BaseLine = lnBase.LineNum;
                        lnTarget.BaseEntry = lnBase.DocEntry;
                        lnTarget.Add();
                    }
                }
                else
                {
                    for (int i = 0; i < thisLinesOnly.Length; i++)
                    {
                        lnBase.SetCurrentLine(thisLinesOnly[i]);
                        lnTarget.SetCurrentLine(i);
                        lnTarget.BaseType = (int)docBase.DocObjectCode;
                        lnTarget.BaseLine = lnBase.LineNum;
                        lnTarget.BaseEntry = lnBase.DocEntry;
                        lnTarget.Add();
                    }
                }
                if (docTarget.Add() != 0) throw new Exception(comp.GetLastErrorDescription());
                NewObjectKey = comp.GetNewObjectKey();
            }
            catch (Exception ex)
            {
                disconnect();
                LastError = ex.Message;
                return false;
            }
            disconnect();
            return true;
        }

        public static void disconnect()
        {
            if (comp == null) return;
            if (!comp.Connected) return;
            comp.Disconnect();
            GC.Collect();
        }
        public class CompanyCollection : List<ConexionSap>
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
    }
    public enum TipoServidorSap
    {
        dst_MSSQL = 1,
        dst_DB_2 = 2,
        dst_SYBASE = 3,
        dst_MSSQL2005 = 4,
        dst_MAXDB = 5,
        dst_MSSQL2008 = 6,
        dst_MSSQL2012 = 7,
        dst_MSSQL2014 = 8,
        dst_HANADB = 9,
        dst_MSSQL2016 = 10,
        dst_MSSQL2017 = 11,
        dst_MSSQL2019 = 0xF
    }
}

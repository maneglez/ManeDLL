using Mane.BD.BaseDeDatos.Executors.WebApiExecutor;
using Mane.BD.Executors;
using System.Data.SQLite;
using System;
using System.Data.Odbc;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace Mane.BD
{
    public class Conexion
    {
        private IBdExecutor executor;
        private string servidor;
        private string nombre;
        private string nombreBD;
        private string usuario;
        private string contrasena;
        private string driver;
        [JsonIgnore]
        public IBdExecutor Executor
        {
            get
            {
                if (executor == null)
                {
                    switch (TipoDeBaseDeDatos)
                    {
                        case TipoDeBd.SqlServer:
                            executor = new SQLServerExecutor(CadenaDeConexion, Bd.AutoDisconnect);
                            break;
                        case TipoDeBd.SQLite:
                            executor = new SQLiteExecutor(CadenaDeConexion, Bd.AutoDisconnect);
                            break;
                        case TipoDeBd.Hana:
                            executor = new HanaExecutor(CadenaDeConexion, Bd.AutoDisconnect);
                            break;
                        case TipoDeBd.ApiWeb:
                            executor = new WebApiExecutor(this);
                            break;
                        default:
                            break;
                    }
                }

                return executor;
            }
        }
        /// <summary>
        /// Cadena De conexión generada apartir de los atributos de la clase
        /// </summary>
        [JsonIgnore]
        public string CadenaDeConexion
        {
            get
            {
                switch (TipoDeBaseDeDatos)
                {
                    case TipoDeBd.SqlServer:
                        try
                        {
                            var sb = new SqlConnectionStringBuilder
                            {
                                DataSource = ServerName(),
                                InitialCatalog = NombreBD,
                                UserID = Usuario,
                                Password = Contrasena,
                                ConnectTimeout = TimeOut
                            };
                            return sb.ConnectionString;
                        }
                        catch (Exception)
                        {

                            return "";
                        }
                    case TipoDeBd.SQLite:
                        var b = new SQLiteConnectionStringBuilder
                        {
                            DataSource = ServerName()
                        };
                        if (!string.IsNullOrEmpty(Contrasena))
                            b.Password = Contrasena;
                        return b.ConnectionString;
                    case TipoDeBd.Hana:
                        var ob = new OdbcConnectionStringBuilder();
                        ob.Driver = Driver;
                        ob.Add("SERVERNODE", ServerName());
                        ob.Add("UID", Usuario);
                        ob.Add("PWD", Contrasena);
                        ob.Add("DATABASENAME", "NDB");
                        ob.Add("DATABASE", NombreBD);
                        return ob.ConnectionString;
                    default: return "";
                }
            }
        }
        /// <summary>
        /// Tipo de base de datos
        /// </summary>
        public TipoDeBd TipoDeBaseDeDatos { get; set; } = TipoDeBd.SqlServer;
        /// <summary>
        /// Tipo de base de datos (solo aplica para web api)
        /// </summary>
        public TipoDeBd SubTipoDeBD { get; set; } = TipoDeBd.SqlServer;

        /// <summary>
        /// Nombre de la conexión
        /// </summary>
        public string Nombre { get => nombre == null ? "" : nombre; set => nombre = value; }
        /// <summary>
        /// Nombre del servidor
        /// </summary>
        public string Servidor
        {
            get => servidor;

            set => servidor = value == null ? "" : value;
        }
        /// <summary>
        /// Nombre de la base de datos
        /// </summary>
        public string NombreBD { get => nombreBD; set => nombreBD = value == null ? "" : value; }
        /// <summary>
        /// Usuario de la base de datos
        /// </summary>
        public string Usuario { get => usuario; set => usuario = value == null ? "" : value; }
        /// <summary>
        /// Contraseña de la base de datos
        /// </summary>
        public string Contrasena { get => contrasena; set => contrasena = value == null ? "" : value; }
        /// <summary>
        /// Especificar solo para conexiones ODBC
        /// </summary>
        public string Driver { get => driver; set => driver = value == null ? "" : value; }
        /// <summary>
        /// Tiempo maximo de espera para la conexion y ejecucion de comando, por defecto 15 segundos
        /// </summary>
        public int TimeOut { get; set; }
        private void Construct()
        {
            Nombre = Servidor = NombreBD = Usuario = Contrasena = "";
            Driver = IntPtr.Size == 4 ? "HDBODBC32" : "HDBODBC";
            TimeOut = 15;
        }
        /// <summary>
        /// Puerto
        /// </summary>
        public int Puerto { get; set; }
        public bool UseSSL { get; set; }
        /// <summary>
        /// Modelo de conexión a base de datos
        /// </summary>
        public Conexion()
        {
            Construct();

        }
        public string ServerName() => Puerto != 0 ? servidor + ":" + Puerto : servidor;

        public Conexion Copy()
        {
            return new Conexion
            {
                Nombre = Nombre,
                Servidor = Servidor,
                Usuario = Usuario,
                Contrasena = Contrasena,
                NombreBD = NombreBD,
                Puerto = Puerto,
                TipoDeBaseDeDatos = TipoDeBaseDeDatos,
                SubTipoDeBD = SubTipoDeBD,
                Driver = Driver,
                TimeOut = TimeOut,
                UseSSL = UseSSL
            };
        }


    }
}

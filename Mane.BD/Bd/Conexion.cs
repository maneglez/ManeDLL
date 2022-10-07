using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD
{
    public class Conexion
    {
        /// <summary>
        /// Cadena De conexión generada apartir de los atributos de la clase
        /// </summary>
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
                                DataSource = Servidor,
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
                        
                    default: return "";
                }
            }
        }
        /// <summary>
        /// Tipo de base de datos
        /// </summary>
        public TipoDeBd TipoDeBaseDeDatos { get; set; } = TipoDeBd.SqlServer;
        /// <summary>
        /// Nombre de la conexión
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Nombre del servidor
        /// </summary>
        public string Servidor { get; set; }
        /// <summary>
        /// Nombre de la base de datos
        /// </summary>
        public string NombreBD { get; set; }
        /// <summary>
        /// Usuario de la base de datos
        /// </summary>
        public string Usuario { get; set; }
        /// <summary>
        /// Contraseña de la base de datos
        /// </summary>
        public string Contrasena { get; set; }
        /// <summary>
        /// Tiempo maximo de espera para la conexion y ejecucion de comando, por defecto 15 segundos
        /// </summary>
        public int TimeOut { get; set; }
        private void Construct()
        {
            Nombre = Servidor = NombreBD = Usuario = Contrasena = "";
            TimeOut = 15;
        }
        /// <summary>
        /// Puerto
        /// </summary>
        public int Puerto { get; set; }
        /// <summary>
        /// Modelo de conexión a base de datos
        /// </summary>
        public Conexion()
        {
            Construct();

        }





    }
}

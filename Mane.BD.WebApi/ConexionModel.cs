using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mane.BD.WebApi
{
    public class ConexionModel : Modelo<ConexionModel>
    {
        protected override string ConnName => NombreConexion.Local;
        protected override string NombreTabla => "Conexiones";
        public string Nombre { get; set; }
        public string Servidor { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string NombreBD { get; set; }
        public TipoDeBd TipoDeBD { get; set; }
        public int Puerto { get; set; }
        public string Driver { get; set; }

        public Conexion ToConexion()
        {
            return new Conexion
            {
                Nombre = Nombre,
                Servidor = Servidor,
                Puerto = Puerto,
                Driver = Driver,
                TipoDeBaseDeDatos = TipoDeBD,
                Usuario = Usuario,
                Contrasena = Password,
                NombreBD = NombreBD
            };
        }
    }
}
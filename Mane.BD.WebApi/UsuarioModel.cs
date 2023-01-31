using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mane.BD.WebApi
{
    internal class UsuarioModel : Modelo<UsuarioModel>
    {
        protected override string NombreTabla => "Usuarios";
        protected override string ConnName => NombreConexion.Local;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
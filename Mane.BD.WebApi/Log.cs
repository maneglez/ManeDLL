using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mane.BD.WebApi
{
    public class Log : Modelo<Log>
    {
        protected override string ConnName => NombreConexion.Local;
        protected override string NombreTabla => "Log";
        public DateTime Fecha { get; set; }
        public string Detalle { get; set; }
    }
}
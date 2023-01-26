using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD.BaseDeDatos.Executors.WebApiExecutor
{
    internal class WebApiQuery
    {
        public WebApiQuery(string consulta, string tipo, string conexion)
        {
            Consulta = consulta;
            Tipo = tipo;
            Conexion = conexion;
        }

        public string Consulta { get; set; }
        public string Tipo { get; set; }
        public string Conexion { get; set; }
    }
}

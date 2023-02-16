using System;
using System.IO;
using System.Windows.Forms;

namespace Mane.Helpers
{
    public static class Log
    {
        private static string RutaLogDef;
        public static string RutaLogPorDefecto
        {
            get
            {
                if (string.IsNullOrEmpty(RutaLogDef))
                    return Path.Combine(Application.StartupPath, "Log");
                else return RutaLogDef;
            }
            set { RutaLogDef = value; }
        }
        /// <summary>
        /// Genera o agrega una lína al archivo de log
        /// </summary>
        /// <param name="texto">Cadena a registrar</param>
        /// <param name="dir">Carpeta donde se guardará el archivo por defecto se tomará el establecido en la propiedad:
        /// "RutaLogPorDefecto"</param>
        public static void Add(string texto, string dir = "")
        {
            var hoy = DateTime.Now;
            var nombreArchivo = "log_" + hoy.Year + "_" + hoy.Month + "_" + hoy.Day + ".txt";
            texto = hoy + " - " + texto + Environment.NewLine;
            string ruta;
            if (string.IsNullOrEmpty(dir))
                ruta = Path.Combine(RutaLogPorDefecto, nombreArchivo);
            else
                ruta = Path.Combine(dir, nombreArchivo);
            Archivos.EscribirEnArchivo(texto, ruta);
        }
        public static void Add(object obj,string dir = "")
        {
            Add(obj?.ToString(), dir);
        }
    }
}

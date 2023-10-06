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
                if (string.IsNullOrWhiteSpace(RutaLogDef))
                    return "Log";
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
            texto = hoy + $"[{Environment.MachineName}] - " + texto + Environment.NewLine;
            string ruta;
            if (string.IsNullOrWhiteSpace(dir))
                ruta = Path.Combine(RutaLogPorDefecto, nombreArchivo);
            else
                ruta = Path.Combine(dir, nombreArchivo);
            EscribirEnArchivo(texto, ruta);
        }
        public static void Add(object obj, string dir = "")
        {
            Add(obj?.ToString(), dir);
        }
        public static void Add(Exception exception, string dir = "")
        {
            Add(exception?.ToString(), dir);
        }


        /// <summary>
        /// Guarda una cadena en un archivo
        /// </summary>
        /// <param name="data">cadena de texto</param>
        /// <param name="path">ruta donde desea guardar el archivo</param>
        /// <param name="sobreescribir">Si existe el archivo lo reempazará por defecto no lo reemplaza</param>
        private static void EscribirEnArchivo(string data, string path, bool sobreescribir = false)
        {
            try
            {
                string dir = Path.GetDirectoryName(path);
                Directory.CreateDirectory(dir);
                if (!File.Exists(path) || sobreescribir)
                {
                    using (FileStream fs = File.Create(path)) { }
                }
                using (StreamWriter sw = new StreamWriter(path, true))
                    sw.Write(data);
            }
            catch (UnauthorizedAccessException)
            {
                //  MessageBox.Show($"Error al editar o crear el archivo {Path.GetDirectoryName(path)}, por favor ejecute el programa con privilegios de administrador y vuelva a intentarlo.");
            }
            catch (Exception e)
            {
                //  MessageBox.Show(e.Message);
            }
        }
    }
}

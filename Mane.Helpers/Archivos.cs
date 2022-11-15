using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mane.Helpers
{
    public static class Archivos
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
        /// Guarda una cadena en un archivo
        /// </summary>
        /// <param name="data">cadena de texto</param>
        /// <param name="path">ruta donde desea guardar el archivo</param>
        /// <param name="sobreescribir">Si existe el archivo lo reempazará por defecto no lo reemplaza</param>
        public static void EscribirEnArchivo(string data, string path, bool sobreescribir = false)
        {
            try
            {
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                if(!File.Exists(path) || sobreescribir)
                {
                    using (FileStream fs = File.Create(path)) { }
                }
                    using (StreamWriter sw = new StreamWriter(path))
                        sw.Write(data);

            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show($"Error al crear la carpeta {Path.GetDirectoryName(path)}, por favor ejecute el programa con privilegios de administrador y vuelva a intentarlo.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// Genera o agrega una lína al archivo de log
        /// </summary>
        /// <param name="sLog">Cadena a registrar</param>
        /// <param name="dir">Carpeta donde se guardará el archivo por defecto se tomará el establecido en la propiedad:
        /// "RutaLogPorDefecto"</param>
        public static void Log(string sLog, string dir = "")
        {
            var hoy = DateTime.Now;
            var nombreArchivo = "log_" + hoy.Year + "_" + hoy.Month + "_" + hoy.Day + ".txt";
            sLog = hoy + " - " + sLog + Environment.NewLine;
            var ruta = "";
            if (string.IsNullOrEmpty(dir))
                ruta = Path.Combine(RutaLogPorDefecto, nombreArchivo);
            else
                ruta = Path.Combine(dir, nombreArchivo);
            EscribirEnArchivo(sLog, ruta);
        }
    }
}

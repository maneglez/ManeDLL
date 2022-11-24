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
        /// Lee todo el texto de un archivo
        /// </summary>
        /// <param name="ruta">Ruta del archivo</param>
        /// <returns>string con los datos del archivo</returns>
        public static string LeerArchivo(string ruta)
        {
            string data = "";
            using (var sr = new StreamReader(ruta))
                data = sr.ReadToEnd();
            return data;
        }
        
    }
}

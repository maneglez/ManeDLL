using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
                CrearCarpeta(dir);
                if (!File.Exists(path) || sobreescribir)
                {
                    using (FileStream fs = File.Create(path)) { }
                }
                using (StreamWriter sw = new StreamWriter(path, true))
                    sw.Write(data);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show($"Error al editar o crear el archivo {Path.GetDirectoryName(path)}, por favor ejecute el programa con privilegios de administrador y vuelva a intentarlo.");
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
            if (!File.Exists(ruta)) return "";
            using (var sr = new StreamReader(ruta))
                data = sr.ReadToEnd();
            return data;
        }

        /// <summary>
        /// Crea una carpeta
        /// </summary>
        /// <param name="carpeta"></param>
        public static void CrearCarpeta(string carpeta)
        {
            try
            {
                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);
            }
            catch (Exception)
            {
                MessageBox.Show($"No se pudo crear el directorio '{carpeta}', ejecute la aplicación como administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Obtiene el directorio en el que se encuentra el .Exe de la aplicación
        /// </summary>
        /// <returns>Devuelve el directirio físico de la aplicación</returns>
        public static string DirectorioBase()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static string DirectorioBase(params string[] subDirectorios)
        {
            var dirs = new List<string>
            {
                DirectorioBase()
            };
            dirs.AddRange(subDirectorios);
            return Path.Combine(dirs.ToArray());
        }

    }
}

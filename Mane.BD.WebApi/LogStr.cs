using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace Mane.BD.WebApi
{
    public class LogStr
    {
        /// <summary>
        /// Agrega una línea al archivo de registro de errores ubicado en el startup path
        /// </summary>
        /// <param name="sLog">Cadena a escribir</param>
        public static void Add(string sLog)
        {
            try
            {
                string Path = HostingEnvironment.MapPath("~/Log");
                try
                {
                    if (!Directory.Exists(Path))
                        Directory.CreateDirectory(Path);


                }
                catch (DirectoryNotFoundException)
                {
                    // throw new Exception(ex.Message);
                }
                string nombre = "log_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".txt";
                string cadena = "";
                cadena += DateTime.Now + " - " + sLog + Environment.NewLine;
                using (StreamWriter sw = new StreamWriter(Path + "/" + nombre, true))
                    sw.Write(cadena);
            }
            catch (Exception)
            {


            }
        }
    }
}
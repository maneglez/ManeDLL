using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Mane.Helpers
{
    public enum ErrorSerializacion
    {
        Ninguno,
        ArchivoNoEncontrado,
        ExepcionDesconocida
    }
    public static class Serializacion
    {
        public static ErrorSerializacion ErrorSerializacion { get; private set; }
        public static string MsgError;
        /// <summary>
        /// Convierte un Xml a un T objeto
        /// </summary>
        /// <typeparam name="T">Tipo de objeto</typeparam>
        /// <param name="xmlPath">ruta del XML</param>
        /// <returns>T objeto</returns>
        public static T XMLToObject<T>(string xmlPath) where T : class
        {
            try
            {
                if (!File.Exists(xmlPath))
                {
                    ErrorSerializacion = ErrorSerializacion.ArchivoNoEncontrado;
                    return null;
                }
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StreamReader sr = new StreamReader(xmlPath))
                    return (T)serializer.Deserialize(sr);
            }
            catch (Exception ex)
            {
                MsgError = ex.Message;
                ErrorSerializacion = ErrorSerializacion.ExepcionDesconocida;
                return null;
            }
        }
        /// <summary>
        /// Convierte un objeto a XML
        /// </summary>
        /// <param name="objeto">Objeto a Serializar</param>
        /// <param name="xmlPath">Ruta de guardado</param>
        /// <param name="reemplazar"></param>
        public static void ObjectToXML(object objeto, string xmlPath, bool reemplazar = false)
        {
            Archivos.EscribirEnArchivo(ObjectToXML(objeto), xmlPath, reemplazar);
        }
        /// <summary>
        /// Convierte un objeto a una cadena XML
        /// </summary>
        /// <param name="objeto">Objeto a Serializar</param>
        /// <returns>Cadena con el XML resultante</returns>
        public static string ObjectToXML(object objeto)
        {

            string xml = "";
            XmlSerializer serializer = new XmlSerializer(objeto.GetType());
            using (StringWriter sw = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sw))
                {
                    serializer.Serialize(writer, objeto);
                    xml = sw.ToString();
                }
            }
            return xml;
        }
        public static string ObjectToJson(object obj)
        {
           return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}

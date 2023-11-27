using System;
using System.IO;
using System.Text;
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
        /// <returns>T objeto o nulo si el archivo no existe o ocurrió un error al deserializar</returns>
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
        /// Convierte un Xml a un T objeto
        /// </summary>
        /// <typeparam name="T">Tipo de objeto</typeparam>
        /// <param name="xmlPath">ruta del XML</param>
        /// <returns>T objeto o nulo si el archivo no existe o ocurrió un error al deserializar</returns>
        public static T XMLStringToObject<T>(string xmlString) where T : class
        {
            try
            {
                T returnVal = default;
                using(var str = new MemoryStream(Encoding.Unicode.GetBytes(xmlString ?? "")))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    returnVal = (T)serializer.Deserialize(str);
                }
                return returnVal;
            }
            catch (Exception ex)
            {
                MsgError = ex.Message;
                MsgBox.error(ex.ToString());
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
                using (var writer = new XmlTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 4;
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

        public static T JsonToObject<T>(string jsonString)
        {
            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}

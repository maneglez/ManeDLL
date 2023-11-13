using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Mane.CFDI
{
    public class Addenda
    {
        /// <summary>
        /// Crea una nueva cadena con el XML + la addenda especificada
        /// </summary>
        /// <param name="rutaXml">ruta del xml original</param>
        /// <param name="objetoAddenda">Addenda</param>
        /// <returns>Cadena con la addenda adjunta</returns>
        /// <exception cref="Exception">Lanza exepción si el xml no es un cfdi válido o la versión no es soportada</exception>
        public static string Attach(string rutaXml, object objetoAddenda)
        {
            if (Cfdi.GetVersionCfdi(rutaXml) == CfdiVersion.Desconocida)
                throw new Exception("El archivo no es un CFDI o la versión no es compatible");
            var doc = new XmlDocument();
            doc.Load(rutaXml);
            var nodoComprobante = doc.GetElementsByTagName("cfdi:Comprobante").Item(0);
            var addendas = doc.GetElementsByTagName("cfdi:Addenda");
            XmlNode nodoAddenda = null;
            if(addendas.Count == 0)
            {
                nodoAddenda = doc.CreateElement("cfdi", "Addenda", nodoComprobante.NamespaceURI);
                nodoAddenda = nodoComprobante.AppendChild(nodoAddenda);
            }
            else
            {
                nodoAddenda = addendas.Item(0);
            }

            //Convertir el objeto addenda a xml
            string xml = "";
            if(objetoAddenda is string)
            {
                xml = (string)objetoAddenda;
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(objetoAddenda.GetType());
                using (StringWriter sw = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sw))
                    {
                        serializer.Serialize(writer, objetoAddenda);
                        xml = sw.ToString();
                    }
                    
                    if (xml.StartsWith("<?xml"))
                    {
                        var aux = new XmlDocument();
                        aux.LoadXml(xml);
                        xml = aux.FirstChild.NextSibling.OuterXml;
                    }
                }
            }
           
            //adjuntarlo al xml actual de la addenda
            nodoAddenda.InnerXml += xml;
            return doc.InnerXml;
        }
    }
}

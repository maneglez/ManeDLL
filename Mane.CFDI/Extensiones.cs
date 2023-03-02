using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Mane.CFDI
{
    public static class Extensiones
    { 
        public static T ToObject<T>(this XmlElement element) where T : class
        {
            T result = null;
            try
            {
                var ser = new XmlSerializer(typeof(T));
                using (var s = GenerateStreamFromString(element.OuterXml))
                {
                   result = (T)ser.Deserialize(s);
                }
            }
            catch (Exception)
            {

            }
            return result;
        }
        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

       
    }
}

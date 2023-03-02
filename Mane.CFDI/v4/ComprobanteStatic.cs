using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mane.CFDI.v4
{
    public partial class Comprobante
    {
        public static Comprobante CargarDesdeArchivo(string fileName)
        {
            Comprobante c = null;
            var ser = new XmlSerializer(typeof(Comprobante));
            using (StreamReader sr = new StreamReader(fileName))
                c = (Comprobante)ser.Deserialize(sr);
            c?.Complemento?.CargarComplementos();
            return c;
        }
    }
}

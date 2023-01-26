using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace Mane.CFDI
{
    public enum CfdiVersion
    {
        Desconocida,
        Cfdi3_3,
        Cfdi4_0
    }
    public static class Cfdi
    {
        public static CfdiVersion Version(string xmlPath)
        {
            try
            {
                var xml = new XmlDocument();
                xml.Load(xmlPath);
                var node = xml.FirstChild;
                if (node?.Name != "cfdi:Comprobante")
                {
                    node = node?.NextSibling;
                    if (node?.Name != "cfdi:Comprobante")
                    {
                        return CfdiVersion.Desconocida;
                    }
                }
               var version = node.Attributes["Version"].Value;
                switch (version)
                {
                    case "3.3": return CfdiVersion.Cfdi3_3;
                    case "4.0": return CfdiVersion.Cfdi4_0;
                    default: return CfdiVersion.Desconocida;
                }
            }
            catch (Exception)
            {

            }
            return CfdiVersion.Desconocida;
        }
    }
}

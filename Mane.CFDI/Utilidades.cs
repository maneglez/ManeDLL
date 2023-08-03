using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mane.CFDI
{
    public static class Utilidades
    {
        //Core recursion function
        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    if (string.IsNullOrEmpty(attribute.Name.NamespaceName))
                        xElement.Add(attribute);

                return xElement;
            }
            else
            {
                var ele = new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    if (string.IsNullOrEmpty(attribute.Name.NamespaceName))
                        ele.Add(attribute);
                return ele;
            }
        }
    }
}

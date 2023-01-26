using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Mane.CFDI.CFDI4
{
    public class Cfdi4Base : XmlObject
    {
        public override dynamic ConvertirATipo(Type t, string value)
        {
            var result = base.ConvertirATipo(t, value);
            if (result != null) return result;
            switch (t.Name)
            {
                case "Emisor": return new Emisor(GetNode("cfdi:Emisor"), nsmgr);
                case "Receptor": return new Receptor(GetNode("cfdi:Receptor"), nsmgr);
                case "Concepto": return new Concepto(GetNode("cfdi:Concepto"), nsmgr);
                case "Traslado": return new Traslado(GetNode("cfdi:Traslado"), nsmgr);
                case "Retencion": return new Retencion(GetNode("cfdi:Retencion"), nsmgr);
                case "Conceptos": return new Conceptos(GetNode("cfdi:Conceptos")?.ChildNodes, nsmgr);
                case "Traslados": return new Traslados(GetNode("cfdi:Traslados")?.ChildNodes, nsmgr);
                case "Retenciones": return new Retenciones(GetNode("cfdi:Retenciones")?.ChildNodes, nsmgr);
                case "ImpuestosConcepto": return new ImpuestosConcepto(GetNode("cfdi:Impuestos"), nsmgr);
                case "Impuestos": return new Impuestos(GetNode("cfdi:Impuestos"), nsmgr);
                case "Complemento": return new Complemento(GetNode("cfdi:Complemento"), nsmgr);
                case "TimbreFiscalDigital": return new TimbreFiscalDigital(GetNode("tfd:TimbreFiscalDigital"), nsmgr);
                case "Pagos":
                    XmlNode nodePago = null;
                    foreach (XmlNode n in node.ChildNodes)
                    {
                        if (n.Name.Contains("Pago"))
                        {
                            nodePago = n;
                            break;
                        }
                    }
                    if (nodePago == null) return null;
                    return new Pagos(nodePago.ChildNodes, nsmgr);
                case "Pago": return new Pago(GetNode(node.Name.Remove(-1)), nsmgr);
                default: return null;
            }
        }
    }

    public class CFDI4SubObj : Cfdi4Base
    {
        public CFDI4SubObj()
        {

        }
        public CFDI4SubObj(XmlNode node, XmlNamespaceManager nsmgr)
        {
            this.node = node;
            this.nsmgr = nsmgr;
            SetAttributes();
        }
    }
    public class CFDI4SubObjList<T> : List<T> where T : CFDI4SubObj, new()
    {

        public CFDI4SubObjList(XmlNodeList list, XmlNamespaceManager nsmgr)
        {
            this.nodes = list;
            this.nsmgr = nsmgr;
            if (nodes == null) return;
            foreach (var node in nodes)
            {
                var obj = Activator.CreateInstance(typeof(T), node, nsmgr) as T;
                Add(obj);
            }
        }

        public CFDI4SubObjList()
        {
        }

        protected XmlNamespaceManager nsmgr;
        protected XmlNodeList nodes;
        new public string ToString()
        {
            return typeof(T).Name;
        }


    }
}

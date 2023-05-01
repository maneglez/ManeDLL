using System.Xml;

namespace Mane.CFDI
{
    public class Retencion : Interfaces.IComprobanteImpuestosRetencion
    {
        public decimal Importe { get; set; }
        public string Impuesto { get; set; }
    }
}



using System.Xml;

namespace Mane.CFDI
{
    public class Traslado : Interfaces.IComprobanteImpuestosTraslado
    {
        public decimal Base { get; set; }
        public decimal Importe { get; set; }
        public bool ImporteSpecified { get; set; }
        public string Impuesto { get; set; }
        public decimal TasaOCuota { get; set; }
        public bool TasaOCuotaSpecified { get; set; }
        public string TipoFactor { get; set; }
    }

}


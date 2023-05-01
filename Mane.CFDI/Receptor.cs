using Mane.CFDI.Interfaces;
using System.Xml;

namespace Mane.CFDI
{
    public class Receptor : IReceptor
    {
        public string DomicilioFiscalReceptor { get; set; }
        public string Nombre { get; set; }
        public string NumRegIdTrib { get; set; }
        public string RegimenFiscalReceptor { get; set; }
        public string ResidenciaFiscal { get; set; }
        public string Rfc { get; set; }
        public string UsoCFDI { get; set; }
    }
}



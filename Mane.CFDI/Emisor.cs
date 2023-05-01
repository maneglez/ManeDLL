using Mane.CFDI.Interfaces;
using System.Xml;

namespace Mane.CFDI
{
    public class Emisor : IEmisor
    {
        public string FacAtrAdquirente { get; set; }
        public string Nombre { get; set; }
        public string RegimenFiscal { get; set; }
        public string Rfc { get; set; }
    }
}



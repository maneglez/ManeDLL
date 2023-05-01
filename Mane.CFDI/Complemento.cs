using System.Xml;
using Mane.CFDI.Complementos.Pago;
using Mane.CFDI.Complementos.TimbreFiscalDigital;
using Mane.CFDI.Interfaces;

namespace Mane.CFDI
{
    public class Complemento : IComplemento
    {
        public ITimbreFiscalDigital TimbreFiscalDigital { get; set; }
        public IPagos Pagos { get; set; }

        public Complemento()
        {
            TimbreFiscalDigital = new TimbreFiscalDigital();
            Pagos = new Pagos();
        }
        public XmlElement[] Any { get; set; }
    }
}



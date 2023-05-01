using Mane.CFDI.Interfaces;

namespace Mane.CFDI.Interfaces
{
    public interface IComplemento
    {
        IPagos Pagos { get; set; }
        ITimbreFiscalDigital TimbreFiscalDigital { get; set; }
        System.Xml.XmlElement[] Any { get; set; }
    }
}
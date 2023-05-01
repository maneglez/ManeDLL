using Mane.CFDI.Interfaces;
namespace Mane.CFDI
{
    public class InformacionGlobal : IComprobanteInformacionGlobal
    {
        public short Año { get; set; }
        public string Meses { get; set; }
        public string Periodicidad { get; set; }
    }
}
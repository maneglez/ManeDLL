using System;
using System.Xml;

namespace Mane.CFDI.Complementos.TimbreFiscalDigital
{


    public class TimbreFiscalDigital : Interfaces.ITimbreFiscalDigital
    {
        public DateTime FechaTimbrado { get; set; }
        public string Leyenda { get; set; }
        public string NoCertificadoSAT { get; set; }
        public string RfcProvCertif { get; set; }
        public string SelloCFD { get; set; }
        public string SelloSAT { get; set; }
        public string UUID { get; set; }
        public string Version { get; set; }
    }
}
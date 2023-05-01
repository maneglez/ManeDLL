using Mane.CFDI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CFDI.Complementos.Pago
{
    public class Pago : IPago
    {
        public Pago()
        {
            DoctoRelacionado = new List<IPagoDoctoRelacionado>();
            ImpuestosP = new PagoImpuestosP();
        }
        public string CadPago { get; set; }
        public byte[] CertPago { get; set; }
        public string CtaBeneficiario { get; set; }
        public string CtaOrdenante { get; set; }
        public List<IPagoDoctoRelacionado> DoctoRelacionado { get; set; }
        public DateTime FechaPago { get; set; }
        public string FormaDePagoP { get; set; }
        public IPagoImpuestosP ImpuestosP { get; set; }
        public string MonedaP { get; set; }
        public decimal Monto { get; set; }
        public string NomBancoOrdExt { get; set; }
        public string NumOperacion { get; set; }
        public string RfcEmisorCtaBen { get; set; }
        public string RfcEmisorCtaOrd { get; set; }
        public byte[] SelloPago { get; set; }
        public string TipoCadPago { get; set; }
        public bool TipoCadPagoSpecified { get; set; }
        public decimal TipoCambioP { get; set; }
        public bool TipoCambioPSpecified { get; set; }
    }
}

using Mane.CFDI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CFDI.Complementos.Pago
{
    public class PagoDoctoRelacionadoImpuestosDRRetencionDR : IPagoDoctoRelacionadoImpuestosDRRetencionDR
    {
        public decimal BaseDR { get; set; }
        public decimal ImporteDR { get; set; }
        public string ImpuestoDR { get; set; }
        public decimal TasaOCuotaDR { get; set; }
        public string TipoFactorDR { get; set; }
    }
}

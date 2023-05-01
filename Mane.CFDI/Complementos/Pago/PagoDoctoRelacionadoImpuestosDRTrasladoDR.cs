using Mane.CFDI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CFDI.Complementos.Pago
{
    public class PagoDoctoRelacionadoImpuestosDRTrasladoDR : IPagoDoctoRelacionadoImpuestosDRTrasladoDR
    {
        public decimal BaseDR { get; set; }
        public decimal ImporteDR { get; set; }
        public bool ImporteDRSpecified { get; set; }
        public string ImpuestoDR { get; set; }
        public decimal TasaOCuotaDR { get; set; }
        public bool TasaOCuotaDRSpecified { get; set; }
        public string TipoFactorDR { get; set; }
    }
}

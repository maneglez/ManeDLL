using Mane.CFDI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CFDI.Complementos.Pago
{
    public class PagoDoctoRelacionadoImpuestosDR : IPagoDoctoRelacionadoImpuestosDR
    {
        public PagoDoctoRelacionadoImpuestosDR()
        {
            RetencionesDR = new List<IPagoDoctoRelacionadoImpuestosDRRetencionDR>();
            TrasladosDR = new List<IPagoDoctoRelacionadoImpuestosDRTrasladoDR>();
        }
        public List<IPagoDoctoRelacionadoImpuestosDRRetencionDR> RetencionesDR { get; set; }
        public List<IPagoDoctoRelacionadoImpuestosDRTrasladoDR> TrasladosDR { get; set; }
    }
}

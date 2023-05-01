using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mane.CFDI.Interfaces;

namespace Mane.CFDI.Complementos.Pago
{
    public class PagoImpuestosPRetencionP : IPagoImpuestosPRetencionP
    {
        public decimal ImporteP { get; set; }
        public string ImpuestoP { get; set; }
    }
}

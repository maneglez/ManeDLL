using Mane.CFDI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CFDI.Complementos.Pago
{
    public class PagoImpuestosP : IPagoImpuestosP
    {
        public List<IPagoImpuestosPRetencionP> RetencionesP { get; set; }
        public List<IPagoImpuestosPTrasladoP> TrasladosP { get; set; }
        public PagoImpuestosP()
        {
            RetencionesP = new List<IPagoImpuestosPRetencionP>();
            TrasladosP = new List<IPagoImpuestosPTrasladoP>();
        }
    }
}

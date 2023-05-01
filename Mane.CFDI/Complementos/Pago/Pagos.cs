using Mane.CFDI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CFDI.Complementos.Pago
{
    public class Pagos : IPagos
    {
        public List<IPago> Pago { get; set; }
        public IPagosTotales Totales { get; set; }
        public string Version { get; set; }
        public Pagos()
        {
            Pago = new List<IPago>();
            Totales = new PagosTotales();
        }
    }
}

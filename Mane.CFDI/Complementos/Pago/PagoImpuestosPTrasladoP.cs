using Mane.CFDI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CFDI.Complementos.Pago
{
    public class PagoImpuestosPTrasladoP : IPagoImpuestosPTrasladoP
    {
        public decimal BaseP { get; set; }
        public decimal ImporteP { get; set; }
        public bool ImportePSpecified { get; set; }
        public string ImpuestoP { get; set; }
        public decimal TasaOCuotaP { get; set; }
        public bool TasaOCuotaPSpecified { get; set; }
        public string TipoFactorP { get; set; }
    }
}

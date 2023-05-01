using Mane.CFDI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CFDI
{
    public class ConceptoRetencion : IComprobanteConceptoImpuestosRetencion
    {
        public decimal Base { get; set; }
        public decimal Importe { get; set; }
        public string Impuesto { get; set; }
        public decimal TasaOCuota { get; set; }
        public string TipoFactor { get; set; }
    }
}

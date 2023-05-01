using Mane.CFDI.Interfaces;
using System.Collections.Generic;
using System.Xml;

namespace Mane.CFDI
{
    public class ImpuestosConcepto : IComprobanteConceptoImpuestos
    {
        public List<IComprobanteConceptoImpuestosRetencion> Retenciones { get; set; }
        public List<IComprobanteConceptoImpuestosTraslado> Traslados { get; set; }

        public ImpuestosConcepto()
        {
            Retenciones = new List<IComprobanteConceptoImpuestosRetencion>();
            Traslados = new List<IComprobanteConceptoImpuestosTraslado>();
        }
    }
}



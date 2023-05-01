using Mane.CFDI.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace Mane.CFDI
{
    public class Impuestos : IComprobanteImpuestos
    {
        public Impuestos()
        {
            Retenciones = new List<IComprobanteImpuestosRetencion>();
            Traslados = new List<IComprobanteImpuestosTraslado>();
        }
        public List<IComprobanteImpuestosRetencion> Retenciones { get; set; }
        public decimal TotalImpuestosRetenidos { get; set; }
        public bool TotalImpuestosRetenidosSpecified { get; set; }
        public decimal TotalImpuestosTrasladados { get; set; }
        public bool TotalImpuestosTrasladadosSpecified { get; set; }
        public List<IComprobanteImpuestosTraslado> Traslados { get; set; }
    }
}



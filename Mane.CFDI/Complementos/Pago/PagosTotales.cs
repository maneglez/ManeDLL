using Mane.CFDI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CFDI.Complementos.Pago
{
    public class PagosTotales : IPagosTotales
    {
        public decimal MontoTotalPagos { get; set; }
        public decimal TotalRetencionesIEPS { get; set; }
        public bool TotalRetencionesIEPSSpecified { get; set; }
        public decimal TotalRetencionesISR { get; set; }
        public bool TotalRetencionesISRSpecified { get; set; }
        public decimal TotalRetencionesIVA { get; set; }
        public bool TotalRetencionesIVASpecified { get; set; }
        public decimal TotalTrasladosBaseIVA0 { get; set; }
        public bool TotalTrasladosBaseIVA0Specified { get; set; }
        public decimal TotalTrasladosBaseIVA16 { get; set; }
        public bool TotalTrasladosBaseIVA16Specified { get; set; }
        public decimal TotalTrasladosBaseIVA8 { get; set; }
        public bool TotalTrasladosBaseIVA8Specified { get; set; }
        public decimal TotalTrasladosBaseIVAExento { get; set; }
        public bool TotalTrasladosBaseIVAExentoSpecified { get; set; }
        public decimal TotalTrasladosImpuestoIVA0 { get; set; }
        public bool TotalTrasladosImpuestoIVA0Specified { get; set; }
        public decimal TotalTrasladosImpuestoIVA16 { get; set; }
        public bool TotalTrasladosImpuestoIVA16Specified { get; set; }
        public decimal TotalTrasladosImpuestoIVA8 { get; set; }
        public bool TotalTrasladosImpuestoIVA8Specified { get; set; }
    }
}

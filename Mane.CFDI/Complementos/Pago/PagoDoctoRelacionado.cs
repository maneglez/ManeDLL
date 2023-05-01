using Mane.CFDI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CFDI.Complementos.Pago
{
    public class PagoDoctoRelacionado : IPagoDoctoRelacionado
    {
        public PagoDoctoRelacionado()
        {
            ImpuestosDR = new PagoDoctoRelacionadoImpuestosDR();
        }
        public decimal EquivalenciaDR { get; set; }
        public bool EquivalenciaDRSpecified { get; set; }
        public string Folio { get; set; }
        public string IdDocumento { get; set; }
        public decimal ImpPagado { get; set; }
        public decimal ImpSaldoAnt { get; set; }
        public decimal ImpSaldoInsoluto { get; set; }
        public IPagoDoctoRelacionadoImpuestosDR ImpuestosDR { get; set; }
        public string MonedaDR { get; set; }
        public string NumParcialidad { get; set; }
        public string ObjetoImpDR { get; set; }
        public string Serie { get; set; }
    }
}

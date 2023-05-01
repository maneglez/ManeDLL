using Mane.CFDI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CFDI
{
    public class CuentaATerceros : IComprobanteConceptoACuentaTerceros
    {
        public string DomicilioFiscalACuentaTerceros { get; set; }
        public string NombreACuentaTerceros { get; set; }
        public string RegimenFiscalACuentaTerceros { get; set; }
        public string RfcACuentaTerceros { get; set; }
    }
}

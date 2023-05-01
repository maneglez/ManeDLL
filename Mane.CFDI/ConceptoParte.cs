using Mane.CFDI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CFDI
{
    internal class ConceptoParte : IComprobanteConceptoParte
    {
        public decimal Cantidad { get; set; }
        public string ClaveProdServ { get; set; }
        public string Descripcion { get; set; }
        public decimal Importe { get; set; }
        public bool ImporteSpecified { get; set; }
        public List<IComprobanteConceptoParteInformacionAduanera> InformacionAduanera { get; set; }
        public string NoIdentificacion { get; set; }
        public string Unidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public bool ValorUnitarioSpecified { get; set; }
    }
}

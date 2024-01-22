using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Helpers.Common.Clases
{
    public class Documento : IDocumento
    {
        public Documento()
        {
            Lines = new List<ILineaDocumento>();
        }
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public DateTime DocDate { get; set; }
        public string Comments { get; set; }
        public List<ILineaDocumento> Lines { get; set; }
        public string ShipToCode { get; set; }
        public string PayToCode { get; set; }
        public DateTime DocDueDate { get; set; }
        public string Currency { get; set; }
        public double DocTotal { get; set; }
        public double VatSum { get; set; }
    }
}

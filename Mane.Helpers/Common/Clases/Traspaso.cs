using System;
using System.Collections.Generic;



namespace Mane.Helpers.Common
{
    public class Traspaso : ITraspaso
    {
        public string FromWhs { get; set; }
        public string ToWhs { get; set; }
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public DateTime DocDate { get; set; }
        public string Comments { get; set; }
        public List<ILineaDocumento> Lines { get; set; }
    }
}

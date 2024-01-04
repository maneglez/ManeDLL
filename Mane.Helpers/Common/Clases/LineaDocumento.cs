using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Helpers.Common.Clases
{
    public class LineaDocumento : ILineaDocumento
    {
        public LineaDocumento()
        {
            BinAllocations = new List<IBinAllocation>();
            SerialNumbers = new List<ISerialNumbers>();
            BatchNumbers = new List<IBatchNumbers>();
        }
        public string FromWhs { get; set; }
        public string ToWhs { get; set; }
        public int LineNum { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public List<ISerialNumbers> SerialNumbers { get; set; }
        public List<IBatchNumbers> BatchNumbers { get; set; }
        public List<IBinAllocation> BinAllocations { get; set; }
        public int DocEntry { get; set; }
        public int BaseEntry { get; set; }
        public int BaseLine { get; set; }
        public int BaseType { get; set; }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is LineaDocumento ln)
                return ln.ItemCode == ItemCode && ln.LineNum == LineNum && ln.Quantity == Quantity;
            return false;
        }
    }
}

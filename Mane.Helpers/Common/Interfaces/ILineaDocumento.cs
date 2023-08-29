using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Helpers.Common
{
    public interface ILineaDocumento
    {
        int LineNum { get; set; }
        string ItemCode { get; set; }
        string ItemDescription { get; set; }
        double Price { get; set; }
        double Quantity { get; set; }
        List<ISerialNumbers> SerialNumbers { get; set; }
        List<IBatchNumbers> BatchNumbers { get; set; }
        List<IBinAllocation> BinAllocations { get; set; }
        int DocEntry { get; set; }
        int BaseEntry { get; set; }
        int BaseLine { get; set; }
        int BaseType { get; set; }
    }
}

using System.Collections.Generic;

namespace Mane.Helpers.Common
{
    public interface ILineaDocumento
    {
        string FromWhs { get; set; }
        string ToWhs { get; set; }
        int LineNum { get; set; }
        string ItemCode { get; set; }
        string ItemDescription { get; set; }
        string TaxCode { get; set; }
        string Currency { get; set; }
        double Price { get; set; }
        double DiscPrcnt { get; set; }
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

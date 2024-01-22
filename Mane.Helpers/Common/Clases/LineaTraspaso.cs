using Mane.Helpers.Forms;
using System.Collections.Generic;

namespace Mane.Helpers.Common
{
    public class LineaTraspaso : VinculableConForm, ILineaDocumento
    {
        private double quantity;
        private string itemCode;
        private string itemDescription;

        public LineaTraspaso()
        {
            BinAllocations = new List<IBinAllocation>();
            SerialNumbers = new List<ISerialNumbers>();
            BatchNumbers = new List<IBatchNumbers>();
        }
        public string FromWhs { get; set; }
        public string ToWhs { get; set; }
        public int LineNum { get; set; }
        public string ItemCode
        {
            get => itemCode; set
            {
                itemCode = value;
                NotifyChange();
            }
        }
        public string ItemDescription
        {
            get => itemDescription; set
            {
                itemDescription = value;
                NotifyChange();
            }
        }
        public double Price { get; set; }
        public double Quantity
        {
            get => quantity; set
            {
                quantity = value;
                NotifyChange();
            }
        }
        public List<ISerialNumbers> SerialNumbers { get; set; }
        public List<IBatchNumbers> BatchNumbers { get; set; }
        public List<IBinAllocation> BinAllocations { get; set; }
        public int DocEntry { get; set; }
        public int BaseEntry { get; set; }
        public int BaseLine { get; set; }
        public int BaseType { get; set; }
        public string TaxCode { get; set; }
        public string Currency { get; set; }
        public double DiscPrcnt { get; set; }
    }
}

namespace Mane.Helpers.Common
{
    public class BinAllocations : IBinAllocation
    {
        public BinAllocations()
        {
            SerialAndBatchNumbersBaseLine = -1;
        }
        public int BinAbsEntry { get; set; }
        public double Quantity { get; set; }
        public int SerialAndBatchNumbersBaseLine { get; set; }
        public int BaseLineNumber { get; set; }
        public BinActionType BinActionType { get; set; }
        public string BinCode { get; set; }
        public override bool Equals(object obj)
        {
            if(obj is IBinAllocation bin)
            {
                return bin.BinCode == BinCode &&
                    bin.BinAbsEntry == BinAbsEntry &&
                    bin.Quantity == Quantity
                    && bin.SerialAndBatchNumbersBaseLine == SerialAndBatchNumbersBaseLine
                    && bin.BinActionType == BinActionType;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

namespace Mane.Helpers.Common
{
    public interface IBinAllocation
    {
        int BinAbsEntry { get; set; }
        string BinCode { get; set; }
        double Quantity { get; set; }
        int SerialAndBatchNumbersBaseLine { get; set; }
        int BaseLineNumber { get; set; }
        BinActionType BinActionType { get; set; }
    }
}

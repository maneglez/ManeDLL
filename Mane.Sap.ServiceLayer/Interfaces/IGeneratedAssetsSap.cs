namespace Mane.Sap.ServiceLayer.Interfaces
{
    public interface IGeneratedAssetsSap
    {
        double amount { get; set; }
        double amountSC { get; set; }
        string AssetCode { get; set; }
        int DocEntry { get; set; }
        int LineNumber { get; set; }
        string Remarks { get; set; }
        string SerialNumber { get; set; }
        GeneratedAssetStatusEnum Status { get; set; }
        int VisOrder { get; set; }
    }
}
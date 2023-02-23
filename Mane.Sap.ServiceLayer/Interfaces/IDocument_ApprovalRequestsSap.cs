namespace Mane.Sap.ServiceLayer.Interfaces
{
    public interface IDocument_ApprovalRequestsSap
    {
        BoYesNoEnum ActiveForUpdate { get; set; }
        int ApprovalTemplatesID { get; set; }
        string ApprovalTemplatesName { get; set; }
        string Remarks { get; set; }
    }
}
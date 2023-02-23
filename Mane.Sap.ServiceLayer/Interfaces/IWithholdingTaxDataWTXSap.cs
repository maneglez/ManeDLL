
namespace Mane.Sap.ServiceLayer.Interfaces
{
    public interface IWithholdingTaxDataWTXSap
    {
        double AccumBaseAmount { get; set; }
        double AccumBaseAmountFC { get; set; }
        double AccumBaseAmountSys { get; set; }
        double AccumWTaxAmount { get; set; }
        double AccumWTaxAmountFC { get; set; }
        double AccumWTaxAmountSys { get; set; }
        int BaseDocEntry { get; set; }
        int BaseDocLine { get; set; }
        int BaseDocType { get; set; }
        int BaseDocumentReference { get; set; }
        double BaseNetAmount { get; set; }
        double BaseNetAmountFC { get; set; }
        double BaseNetAmountSys { get; set; }
        string BaseType { get; set; }
        double BaseVatAmount { get; set; }
        double BaseVatAmountFC { get; set; }
        double BaseVatAmountSys { get; set; }
        string Category { get; set; }
        string Criteria { get; set; }
        double ExemptRate { get; set; }
        string GLAccount { get; set; }
        int LineNum { get; set; }
        double Rate { get; set; }
        string RoundingType { get; set; }
        BoStatus Status { get; set; }
        int TargetAbsEntry { get; set; }
        int TargetDocumentType { get; set; }
        double TaxableAmount { get; set; }
        double TaxableAmountFC { get; set; }
        double TaxableAmountinSys { get; set; }
        //UserFields UserFields { get; set; }
        string WithholdingType { get; set; }
        string WTAbsId { get; set; }
        double WTAmount { get; set; }
        double WTAmountFC { get; set; }
        double WTAmountSys { get; set; }
        string WTCode { get; set; }
    }
}

namespace Mane.Sap.ServiceLayer.Interfaces
{
    public interface IWithholdingTaxLinesSap
    {
        int BaseDocEntry { get; set; }
        int BaseDocLine { get; set; }
        int BaseDocType { get; set; }
        string BaseType { get; set; }
        string Category { get; set; }
        string Criteria { get; set; }
        string CSTCodeIncoming { get; set; }
        string CSTCodeOutgoing { get; set; }
        string GLAccount { get; set; }
        int LineNum { get; set; }
        double Rate { get; set; }
        string RoundingType { get; set; }
        double TaxableAmount { get; set; }
        double TaxableAmountFC { get; set; }
        double TaxableAmountinSys { get; set; }
       // UserFields UserFields { get; set; }
        string WithholdingType { get; set; }
        double WTAmount { get; set; }
        double WTAmountFC { get; set; }
        double WTAmountSys { get; set; }
        string WTCode { get; set; }
    }
}
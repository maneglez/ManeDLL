
namespace Mane.Sap.ServiceLayer.Interfaces
{
    //lista
    public interface ITaxJurisdictionsSap
    {
        int DocEntry { get; set; }
        double ExternalCalcTaxAmount { get; set; }
        double ExternalCalcTaxAmountFC { get; set; }
        double ExternalCalcTaxAmountSC { get; set; }
        double ExternalCalcTaxRate { get; set; }
        string JurisdictionCode { get; set; }
        int JurisdictionType { get; set; }
        int LineNumber { get; set; }
        int RowSequence { get; set; }
        double TaxAmount { get; set; }
        double TaxAmountFC { get; set; }
        double TaxAmountSC { get; set; }
        double TaxRate { get; set; }
       // UserFields UserFields { get; set; }
    }
}
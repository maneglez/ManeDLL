using System.Collections.Generic;

namespace Mane.Sap.ServiceLayer.Interfaces
{
    public interface IDocument_LinesAdditionalExpensesSap
    {
        BoYesNoEnum AquisitionTax { get; set; }
        int BaseGroup { get; set; }
        BoYesNoEnum CUSplit { get; set; }
        double DeductibleTaxSum { get; set; }
        double DeductibleTaxSumFC { get; set; }
        double DeductibleTaxSumSys { get; set; }
        string DistributionRule { get; set; }
        string DistributionRule2 { get; set; }
        string DistributionRule3 { get; set; }
        string DistributionRule4 { get; set; }
        string DistributionRule5 { get; set; }
        IEBooks_Doc_DetailsSap EBooksDetails { get; set; }
        double EqualizationTaxFC { get; set; }
        double EqualizationTaxPercent { get; set; }
        double EqualizationTaxSum { get; set; }
        double EqualizationTaxSys { get; set; }
        int ExpenseCode { get; set; }
        double ExternalCalcTaxAmount { get; set; }
        double ExternalCalcTaxAmountFC { get; set; }
        double ExternalCalcTaxAmountSC { get; set; }
        double ExternalCalcTaxRate { get; set; }
        int GroupCode { get; set; }
        int LineNumber { get; set; }
        double LineTotal { get; set; }
        double LineTotalFC { get; set; }
        double LineTotalSys { get; set; }
        double PaidToDate { get; set; }
        double PaidToDateFC { get; set; }
        double PaidToDateSys { get; set; }
        string Project { get; set; }
        string TaxCode { get; set; }
        List<ITaxJurisdictionsSap> TaxJurisdictions { get; set; }
        BoYesNoEnum TaxLiable { get; set; }
        double TaxPaid { get; set; }
        double TaxPaidFC { get; set; }
        double TaxPaidSys { get; set; }
        double TaxPercent { get; set; }
        double TaxSum { get; set; }
        double TaxSumFC { get; set; }
        double TaxSumSys { get; set; }
        double TaxTotalSum { get; set; }
        double TaxTotalSumFC { get; set; }
        double TaxTotalSumSys { get; set; }
        BoAdEpnsTaxTypes TaxType { get; set; }
        string VatGroup { get; set; }
        BoYesNoEnum WTLiable { get; set; }
    }
}
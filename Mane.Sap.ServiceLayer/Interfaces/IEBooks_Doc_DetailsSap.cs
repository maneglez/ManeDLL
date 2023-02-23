namespace Mane.Sap.ServiceLayer.Interfaces
{
    public interface IEBooks_Doc_DetailsSap
    {
        int ExpensesClassificationCategory { get; set; }
        int ExpensesClassificationType { get; set; }
        int IncomeClassificationCategory { get; set; }
        int IncomeClassificationType { get; set; }
        double NetValueFC { get; set; }
        double NetValueLC { get; set; }
        double NetValueSC { get; set; }
        int VatCategory { get; set; }
        int VatClassificationCategory { get; set; }
        int VatClassificationType { get; set; }
        int VATExemptionCause { get; set; }
        double WithheldAmountFC { get; set; }
        double WithheldAmountLC { get; set; }
        double WithheldAmountSC { get; set; }
        int WithheldPercentCategory { get; set; }
    }
}
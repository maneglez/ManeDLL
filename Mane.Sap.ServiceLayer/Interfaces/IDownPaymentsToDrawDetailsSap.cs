

namespace Mane.Sap.ServiceLayer.Interfaces
{
    //Lista
    public interface IDownPaymentsToDrawDetailsSap
    {
        double AmountToDraw { get; set; }
        double AmountToDrawFC { get; set; }
        double AmountToDrawSC { get; set; }
        int DocEntry { get; set; }
        int DocInternalID { get; set; }
        double GrossAmountToDraw { get; set; }
        double GrossAmountToDrawFC { get; set; }
        double GrossAmountToDrawSC { get; set; }
        BoYesNoEnum IsGrossLine { get; set; }
        LineTypeEnum LineType { get; set; }
        int RowNum { get; set; }
        int SeqNum { get; set; }
        double Tax { get; set; }
        BoYesNoEnum TaxAdjust { get; set; }
        double TaxFC { get; set; }
        double TaxSC { get; set; }
        string VatGroupCode { get; set; }
        double VatPercent { get; set; }
    }
}
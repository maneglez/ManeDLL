

namespace Mane.Sap.ServiceLayer.Interfaces
{
    public interface IDocument_SpecialLinesSap
    {
        int AfterLineNumber { get; set; }
        double Freight1 { get; set; }
        double Freight1FC { get; set; }
        double Freight1SC { get; set; }
        double Freight2 { get; set; }
        double Freight2FC { get; set; }
        double Freight2SC { get; set; }
        double Freight3 { get; set; }
        double Freight3FC { get; set; }
        double Freight3SC { get; set; }
        double GrossTotal { get; set; }
        double GrossTotalFC { get; set; }
        double GrossTotalSC { get; set; }
        int LineNum { get; set; }
        string LineText { get; set; }
        BoDocSpecialLineType LineType { get; set; }
        int OrderNumber { get; set; }
        double Subtotal { get; set; }
        double SubtotalFC { get; set; }
        double SubtotalSC { get; set; }
        double TaxAmount { get; set; }
        double TaxAmountFC { get; set; }
        double TaxAmountSC { get; set; }
    }
}
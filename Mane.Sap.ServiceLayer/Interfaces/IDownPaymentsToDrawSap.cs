using System;
using System.Collections.Generic;

namespace Mane.Sap.ServiceLayer.Interfaces
{
    //Lista
    public interface IDownPaymentsToDrawSap
    {
        double AmountToDraw { get; set; }
        double AmountToDrawFC { get; set; }
        double AmountToDrawSC { get; set; }
        string Details { get; set; }
        int DocEntry { get; set; }
        int DocInternalID { get; set; }
        int DocNumber { get; set; }
        List<IDownPaymentsToDrawDetailsSap> DownPaymentsToDrawDetails { get; set; }
        DownPaymentTypeEnum DownPaymentType { get; set; }
        DateTime DueDate { get; set; }
        double GrossAmountToDraw { get; set; }
        double GrossAmountToDrawFC { get; set; }
        double GrossAmountToDrawSC { get; set; }
        BoYesNoEnum IsGrossLine { get; set; }
        string Name { get; set; }
        DateTime PostingDate { get; set; }
        int RowNum { get; set; }
        double Tax { get; set; }
        double TaxFC { get; set; }
        double TaxSC { get; set; }
    }
}
using System;

namespace Mane.Sap.ServiceLayer.Interfaces
{
    public interface IDocument_InstallmentsSap
    {
        DateTime DueDate { get; set; }
        int DunningLevel { get; set; }
        int InstallmentId { get; set; }
        DateTime LastDunningDate { get; set; }
        BoYesNoEnum PaymentOrdered { get; set; }
        double Percentage { get; set; }
        double Total { get; set; }
        double TotalFC { get; set; }
    }
}
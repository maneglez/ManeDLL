using System;

namespace Mane.Sap.ServiceLayer.Interfaces
{
    public interface IDocument_EWayBillDetailsSap
    {
        string BillFromGSTIN { get; set; }
        string BillFromName { get; set; }
        string BillFromStateGSTCode { get; set; }
        string BillToGSTIN { get; set; }
        string BillToName { get; set; }
        string BillToStateGSTCode { get; set; }
        string DispatchFromAddress1 { get; set; }
        string DispatchFromAddress2 { get; set; }
        string DispatchFromPlace { get; set; }
        string DispatchFromStateGSTCode { get; set; }
        string DispatchFromZipCode { get; set; }
        double Distance { get; set; }
        int DocEntry { get; set; }
        string DocumentType { get; set; }
        DateTime EWayBillDate { get; set; }
        DateTime EWayBillExpirationDate { get; set; }
        string EWayBillNo { get; set; }
        int MainHSNEntry { get; set; }
        string ShipToAddress1 { get; set; }
        string ShipToAddress2 { get; set; }
        string ShipToPlace { get; set; }
        string ShipToStateGSTCode { get; set; }
        string ShipToZipCode { get; set; }
        int SubType { get; set; }
        EWBSupplyTypeEnum SupplyType { get; set; }
        EWBTransactionTypeEnum TransactionType { get; set; }
        int TransportationMode { get; set; }
        DateTime TransporterDocDate { get; set; }
        string TransporterDocNo { get; set; }
        int TransporterEntry { get; set; }
        string TransporterID { get; set; }
        int TransporterLineNumber { get; set; }
        string TransporterName { get; set; }
        string VehicleNo { get; set; }
        string VehicleType { get; set; }
    }
}
using System;

namespace Mane.Sap.ServiceLayer.Interfaces
{
    public interface ISerialNumbersSap
    {
        int BaseLineNumber { get; set; }
        string BatchID { get; set; }
        DateTime ExpiryDate { get; set; }
        string InternalSerialNumber { get; set; }
        string ItemCode { get; set; }
        string Location { get; set; }
        DateTime ManufactureDate { get; set; }
        string ManufacturerSerialNumber { get; set; }
        string Notes { get; set; }
        double Quantity { get; set; }
        DateTime ReceptionDate { get; set; }
        int SystemSerialNumber { get; set; }
        int TrackingNote { get; set; }
        int TrackingNoteLine { get; set; }
     // UserFields UserFields { get; set; }
        DateTime WarrantyEnd { get; set; }
        DateTime WarrantyStart { get; set; }
    }
}
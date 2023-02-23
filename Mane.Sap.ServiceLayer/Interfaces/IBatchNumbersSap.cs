using System;

namespace Mane.Sap.ServiceLayer.Interfaces
{
    public interface IBatchNumbersSap
    {
        DateTime AddmisionDate { get; set; }
        int BaseLineNumber { get; set; }
        string BatchNumber { get; set; }
        DateTime ExpiryDate { get; set; }
        string InternalSerialNumber { get; set; }
        string ItemCode { get; set; }
        string Location { get; set; }
        string ManufacturerSerialNumber { get; set; }
        DateTime ManufacturingDate { get; set; }
        string Notes { get; set; }
        double Quantity { get; set; }
        int SystemSerialNumber { get; set; }
        int TrackingNote { get; set; }
        int TrackingNoteLine { get; set; }
    }
}
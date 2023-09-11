using System;

namespace Mane.Helpers.Common
{
    public class SerialNumbers : ISerialNumbers
    {
        public int BaseLineNumber { get; set; }
        public string BatchID { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string InternalSerialNumber { get; set; }
        public string ItemCode { get; set; }
        public string Location { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string ManufacturerSerialNumber { get; set; }
        public string Notes { get; set; }
        public double Quantity { get; set; }
        public DateTime ReceptionDate { get; set; }
        public int SystemSerialNumber { get; set; }
        public int TrackingNote { get; set; }
        public int TrackingNoteLine { get; set; }
        public DateTime WarrantyEnd { get; set; }
        public DateTime WarrantyStart { get; set; }
    }
}

using System;

namespace Mane.Helpers.Common
{
    public class BatchNumbers : IBatchNumbers
    {
        public DateTime AddmisionDate { get; set; }
        public int BaseLineNumber { get; set; }
        public string BatchNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string InternalSerialNumber { get; set; }
        public string ItemCode { get; set; }
        public string Location { get; set; }
        public string ManufacturerSerialNumber { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public string Notes { get; set; }
        public double Quantity { get; set; }
        public int SystemSerialNumber { get; set; }
        public int TrackingNote { get; set; }
        public int TrackingNoteLine { get; set; }
    }
}

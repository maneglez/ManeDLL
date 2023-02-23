namespace Mane.Sap.ServiceLayer.Interfaces
{
    public interface ICCDNumbersSap
    {
        int BaseLineNumber { get; set; }
        string CCDNumber { get; set; }
        int ChildNumber { get; set; }
        string CountryOfOrigin { get; set; }
        int DocumentEntry { get; set; }
        double Quantity { get; set; }
        int SubLineNumber { get; set; }
        int TrackingNote { get; set; }
        int TrackingNoteLine { get; set; }
    }
}
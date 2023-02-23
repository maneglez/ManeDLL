using System;

namespace Mane.Sap.ServiceLayer.Interfaces
{
    public interface ITaxExtensionSap
    {
        DateTime BillOfEntryDate { get; set; }
        string BillOfEntryNo { get; set; }
        string BlockB { get; set; }
        string BlockS { get; set; }
        double BoEValue { get; set; }
        string Brand { get; set; }
        string BuildingB { get; set; }
        string BuildingS { get; set; }
        string Carrier { get; set; }
        string CityB { get; set; }
        string CityS { get; set; }
        BoYesNoEnum ClaimRefund { get; set; }
        string CountryB { get; set; }
        string CountryS { get; set; }
        string County { get; set; }
        string CountyB { get; set; }
        string CountyS { get; set; }
        int DifferentialOfTaxRate { get; set; }
        int DocEntry { get; set; }
        string GlobalLocationNumberB { get; set; }
        string GlobalLocationNumberS { get; set; }
        double GrossWeight { get; set; }
        BoYesNoEnum ImportOrExport { get; set; }
        ImportOrExportTypeEnum ImportOrExportType { get; set; }
        string Incoterms { get; set; }
        BoYesNoEnum IsIGSTAccount { get; set; }
        int MainUsage { get; set; }
        double NetWeight { get; set; }
        string NFRef { get; set; }
        DateTime OriginalBillOfEntryDate { get; set; }
        string OriginalBillOfEntryNo { get; set; }
        string PackDescription { get; set; }
        int PackQuantity { get; set; }
        string PortCode { get; set; }
        int ShipUnitNo { get; set; }
        string State { get; set; }
        string StateB { get; set; }
        string StateS { get; set; }
        string StreetB { get; set; }
        string StreetS { get; set; }
        string TaxId0 { get; set; }
        string TaxId1 { get; set; }
        string TaxId12 { get; set; }
        string TaxId13 { get; set; }
        string TaxId2 { get; set; }
        string TaxId3 { get; set; }
        string TaxId4 { get; set; }
        string TaxId5 { get; set; }
        string TaxId6 { get; set; }
        string TaxId7 { get; set; }
        string TaxId8 { get; set; }
        string TaxId9 { get; set; }
       // UserFields UserFields { get; set; }
        string Vehicle { get; set; }
        string VehicleState { get; set; }
        string ZipCodeB { get; set; }
        string ZipCodeS { get; set; }
    }
}
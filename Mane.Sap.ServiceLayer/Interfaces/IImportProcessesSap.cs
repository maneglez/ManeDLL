using System;

namespace Mane.Sap.ServiceLayer.Interfaces
{
    public interface IImportProcessesSap
    {
        double AdditionalFreightToNavyAuthority { get; set; }
        double AdditionalItemDiscountValue { get; set; }
        int AdditionalItemSequentialNumber { get; set; }
        string AdditionalNumber { get; set; }
        DateTime CustomsClearanceDate { get; set; }
        DateTime DateOfRegistry_DI_DSI_DA { get; set; }
        string DrawbackRegimeConcessionAccountNumber { get; set; }
        string DrawbackSuspensionRegime { get; set; }
        string ImportationDocumentNumber { get; set; }
        string ImportationDocumentTypeCode { get; set; }
        string TypeOfImport { get; set; }
    }
}
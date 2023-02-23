using System;

namespace Mane.Sap.ServiceLayer.Interfaces
{
    public interface IDocument_DocumentReferencesSap
    {
        string AccessKey { get; set; }
        int DocEntry { get; set; }
        string ExternalReferencedDocNumber { get; set; }
        string FiscalDocumentModel { get; set; }
        int FiscalDocumentNumber { get; set; }
        string FiscalDocumentSeries { get; set; }
        string FiscalDocumentSubseries { get; set; }
        DateTime IssueDate { get; set; }
        string IssuerCNPJ { get; set; }
        string IssuerCode { get; set; }
        int LineNumber { get; set; }
        LinkReferenceTypeEnum LinkReferenceType { get; set; }
        string ReferencedAccessKey { get; set; }
        double ReferencedAmount { get; set; }
        int ReferencedDocEntry { get; set; }
        int ReferencedDocNumber { get; set; }
        ReferencedObjectTypeEnum ReferencedObjectType { get; set; }
        string Remark { get; set; }
    }
}
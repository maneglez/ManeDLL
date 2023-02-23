using System;
using System.Collections.Generic;

namespace Mane.Sap.ServiceLayer.Interfaces
{
    //Lista
    public interface IElectronicProtocolsSap
    {
        string Confirmation { get; set; }
        string EBillingIRN { get; set; }
        string EBooksInvoiceType { get; set; }
        string EBooksInvoiceTypeofNegative { get; set; }
        string EBooksMARK { get; set; }
        string EBooksMARKofNegative { get; set; }
        BoYesNoEnum EBooksRelevant { get; set; }
        int EDocType { get; set; }
        string EETBKP { get; set; }
        string EETPKP { get; set; }
        string FechaTimbrado { get; set; }
        string FPAProgressivo { get; set; }
        DateTime FPASendDateSDI { get; set; }
        int FPASequenceNumber { get; set; }
        ElectronicDocGenTypeEnum GenerationType { get; set; }
        int MappingID { get; set; }
        string NoCertificadoSAT { get; set; }
        string PaymentMethod { get; set; }
        ElectronicDocProtocolCodeEnum ProtocolCode { get; set; }
        string ProtocolDescription { get; set; }
        List<IRelatedDocumentsSap> RelatedDocuments { get; set; }
        string RfcProvCertif { get; set; }
        string SelloSAT { get; set; }
        string SignatureDigest { get; set; }
        string SignatureInputMessage { get; set; }
        BoYesNoEnum TestingMode { get; set; }
    }
}
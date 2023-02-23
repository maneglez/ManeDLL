﻿using System;
using System.Collections.Generic;

namespace Mane.Sap.ServiceLayer.Interfaces
{
    interface IDocumentSap
    {
        string AddLegIn { get; set; }
        string Address { get; set; }
        string Address2 { get; set; }
        IAddressExtensionSap AddressExtension { get; set; }
        string AgentCode { get; set; }
        int AnnualInvoiceDeclarationReference { get; set; }
        BoYesNoEnum ApplyCurrentVATRatesForDownPaymentsToDraw { get; set; }
        BoYesNoEnum ApplyTaxOnFirstInstallment { get; set; }
        BoYesNoEnum ArchiveNonremovableSalesQuotation { get; set; }
        DateTime AssetValueDate { get; set; }
        string ATDocumentType { get; set; }
        int AttachmentEntry { get; set; }
        string AuthorizationCode { get; set; }
        DocumentAuthorizationStatusEnum AuthorizationStatus { get; set; }
        double BaseAmount { get; set; }
        double BaseAmountFC { get; set; }
        double BaseAmountSC { get; set; }
        int BaseEntry { get; set; }
        int BaseType { get; set; }
        BoYesNoEnum BillOfExchangeReserved { get; set; }
        int BlanketAgreementNumber { get; set; }
        BoYesNoEnum BlockDunning { get; set; }
        string Box1099 { get; set; }
        string BPChannelCode { get; set; }
        int BPChannelContact { get; set; }
        int BPL_IDAssignedToInvoice { get; set; }
        string BPLName { get; set; }
        DateTime CancelDate { get; set; }
        BoYesNoEnum Cancelled { get; set; }
        CancelStatusEnum CancelStatus { get; set; }
        string CardCode { get; set; }
        string CardName { get; set; }
        int CashDiscountDateOffset { get; set; }
        string CentralBankIndicator { get; set; }
        string CertificationNumber { get; set; }
        int Cig { get; set; }
        DateTime ClosingDate { get; set; }
        ClosingOptionEnum ClosingOption { get; set; }
        string ClosingRemarks { get; set; }
        string Comments { get; set; }
        CommissionTradeTypeEnum CommissionTrade { get; set; }
        BoYesNoEnum CommissionTradeReturn { get; set; }
        BoYesNoEnum Confirmed { get; set; }
        int ContactPersonCode { get; set; }
        string ControlAccount { get; set; }
        BoYesNoEnum CreateOnlineQuotation { get; set; }
        string CreateQRCodeFrom { get; set; }
        DateTime CreationDate { get; set; }
        int Cup { get; set; }
        string CustOffice { get; set; }
        string DANFELgTxt { get; set; }
        int DataVersion { get; set; }
        DateTime DateOfReportingControlStatementVAT { get; set; }
        BoYesNoEnum DeferredTax { get; set; }
        double DiscountPercent { get; set; }
        string DocCurrency { get; set; }
        DateTime DocDate { get; set; }
        DateTime DocDueDate { get; set; }
        int DocEntry { get; set; }
        int DocNum { get; set; }
        BoObjectTypes DocObjectCode { get; set; }
        double DocRate { get; set; }
        DateTime DocTime { get; set; }
        double DocTotal { get; set; }
        double DocTotalFc { get; set; }
        double DocTotalSys { get; set; }
        BoDocumentTypes DocType { get; set; }
        List<IDocument_ApprovalRequestsSap> Document_ApprovalRequests { get; set; }
        List<IDocumentsAdditionalExpensesSap> DocumentAdditionalExpenses { get; set; }
        DocumentDeliveryTypeEnum DocumentDelivery { get; set; }
        List<IDocument_InstallmentsSap> DocumentInstallments { get; set; }
        List<IDocumentLineSap> DocumentLines { get; set; }
        List<IDocument_DocumentReferencesSap> DocumentReferences { get; set; }
        int DocumentsOwner { get; set; }
        List<IDocument_SpecialLinesSap> DocumentSpecialLines { get; set; }
        BoStatus DocumentStatus { get; set; }
        BoDocumentSubType DocumentSubType { get; set; }
        string DocumentTaxID { get; set; }
        double DownPayment { get; set; }
        double DownPaymentAmount { get; set; }
        double DownPaymentAmountFC { get; set; }
        double DownPaymentAmountSC { get; set; }
        double DownPaymentPercentage { get; set; }
        BoSoStatus DownPaymentStatus { get; set; }
        List<IDownPaymentsToDrawSap> DownPaymentsToDraw { get; set; }
        DownPaymentTypeEnum DownPaymentType { get; set; }
        string EDocErrorCode { get; set; }
        string EDocErrorMessage { get; set; }
        int EDocExportFormat { get; set; }
        EDocGenerationTypeEnum EDocGenerationType { get; set; }
        string EDocNum { get; set; }
        int EDocSeries { get; set; }
        EDocStatusEnum EDocStatus { get; set; }
        string ElecCommMessage { get; set; }
        ElecCommStatusEnum ElecCommStatus { get; set; }
        List<IElectronicProtocolsSap> ElectronicProtocols { get; set; }
        DateTime EndDeliveryDate { get; set; }
        DateTime EndDeliveryTime { get; set; }
        string ETaxNumber { get; set; }
        int ETaxWebSite { get; set; }
        IDocument_EWayBillDetailsSap EWayBillDetails { get; set; }
        BoYesNoEnum ExcludeFromTaxReportControlStatementVAT { get; set; }
        DateTime ExemptionValidityDateFrom { get; set; }
        DateTime ExemptionValidityDateTo { get; set; }
        string ExternalCorrectedDocNum { get; set; }
        int ExtraDays { get; set; }
        int ExtraMonth { get; set; }
        string FatherCard { get; set; }
        BoFatherCardTypes FatherType { get; set; }
        string FCI { get; set; }
        string FederalTaxID { get; set; }
        int FinancialPeriod { get; set; }
        string FiscalDocNum { get; set; }
        int FolioNumber { get; set; }
        int FolioNumberFrom { get; set; }
        int FolioNumberTo { get; set; }
        string FolioPrefixString { get; set; }
        int Form1099 { get; set; }
        BoYesNoEnum GroupHandWritten { get; set; }
        int GroupNumber { get; set; }
        int GroupSeries { get; set; }
        GSTTransactionTypeEnum GSTTransactionType { get; set; }
        int GTSChecker { get; set; }
        int GTSPayee { get; set; }
        BoYesNoEnum HandWritten { get; set; }
        int ImportFileNum { get; set; }
        string Indicator { get; set; }
        BoYesNoEnum InsuranceOperation347 { get; set; }
        BoInterimDocTypes InterimType { get; set; }
        int InternalCorrectedDocNum { get; set; }
        BoYesNoEnum InvoicePayment { get; set; }
        BoYesNoEnum IsAlteration { get; set; }
        BoYesNoEnum IsPayToBank { get; set; }
        int IssuingReason { get; set; }
        string JournalMemo { get; set; }
        int LanguageCode { get; set; }
        int LastPageFolioNumber { get; set; }
        int LegalTextFormat { get; set; }
        FolioLetterEnum Letter { get; set; }
        string ManualNumber { get; set; }
        BoYesNoEnum MaximumCashDiscount { get; set; }
        BoYesNoEnum NetProcedure { get; set; }
        int NextCorrectingDocument { get; set; }
        BoYesNoEnum NTSApproved { get; set; }
        string NTSApprovedNumber { get; set; }
        string NumAtCard { get; set; }
        int NumberOfInstallments { get; set; }
        BoYesNoEnum OpenForLandedCosts { get; set; }
        string OpeningRemarks { get; set; }
        DateTime OriginalCreditOrDebitDate { get; set; }
        string OriginalCreditOrDebitNo { get; set; }
        DateTime OriginalRefDate { get; set; }
        string OriginalRefNo { get; set; }
        double PaidToDate { get; set; }
        double PaidToDateFC { get; set; }
        double PaidToDateSys { get; set; }
        BoYesNoEnum PartialSupply { get; set; }
        BoYesNoEnum PaymentBlock { get; set; }
        int PaymentBlockEntry { get; set; }
        int PaymentGroupCode { get; set; }
        string PaymentMethod { get; set; }
        string PaymentReference { get; set; }
        string PayToBankAccountNo { get; set; }
        string PayToBankBranch { get; set; }
        string PayToBankCode { get; set; }
        string PayToBankCountry { get; set; }
        string PayToCode { get; set; }
        string PeriodIndicator { get; set; }
        BoYesNoEnum Pick { get; set; }
        string PickRemark { get; set; }
        BoYesNoEnum PickStatus { get; set; }
        string PointOfIssueCode { get; set; }
        int POS_CashRegister { get; set; }
        int POSCashierNumber { get; set; }
        int POSDailySummaryNo { get; set; }
        string POSEquipmentNumber { get; set; }
        string POSManufacturerSerialNumber { get; set; }
        int POSReceiptNo { get; set; }
        PriceModeDocumentEnum PriceMode { get; set; }
        PrintStatusEnum Printed { get; set; }
        BoYesNoEnum PrintSEPADirect { get; set; }
        int PrivateKeyVersion { get; set; }
        string Project { get; set; }
        int Receiver { get; set; }
        string Reference1 { get; set; }
        string Reference2 { get; set; }
        int RelatedEntry { get; set; }
        int RelatedType { get; set; }
        int Releaser { get; set; }
        BoYesNoEnum RelevantToGTS { get; set; }
        BoYesNoEnum ReopenManuallyClosedOrCanceledDocument { get; set; }
        BoYesNoEnum ReopenOriginalDocument { get; set; }
        string ReportingSectionControlStatementVAT { get; set; }
        DateTime RequriedDate { get; set; }
        BoYesNoEnum Reserve { get; set; }
        BoYesNoEnum ReserveInvoice { get; set; }
        BoYesNoEnum ReuseDocumentNum { get; set; }
        BoYesNoEnum ReuseNotaFiscalNum { get; set; }
        BoYesNoEnum Revision { get; set; }
        BoYesNoEnum RevisionPo { get; set; }
        BoYesNoEnum Rounding { get; set; }
        double RoundingDiffAmount { get; set; }
        double RoundingDiffAmountFC { get; set; }
        double RoundingDiffAmountSC { get; set; }
        int SalesPersonCode { get; set; }
        string SAPPassport { get; set; }
        int Segment { get; set; }
        int SequenceCode { get; set; }
        string SequenceModel { get; set; }
        int SequenceSerial { get; set; }
        int Series { get; set; }
        string SeriesString { get; set; }
        double ServiceGrossProfitPercent { get; set; }
        string ShipFrom { get; set; }
        string ShipPlace { get; set; }
        string ShipState { get; set; }
        string ShipToCode { get; set; }
        BoYesNoEnum ShowSCN { get; set; }
        string SignatureDigest { get; set; }
        string SignatureInputMessage { get; set; }
        int SOIWizardId { get; set; }
        DateTime SpecifiedClosingDate { get; set; }
        DateTime StartDeliveryDate { get; set; }
        DateTime StartDeliveryTime { get; set; }
        BoPayTermDueTypes StartFrom { get; set; }
        BoYesNoEnum Submitted { get; set; }
        string SubSeriesString { get; set; }
        BoDocSummaryTypes SummeryType { get; set; }
        string Supplier { get; set; }
        DateTime TaxDate { get; set; }
        string TaxExemptionLetterNum { get; set; }
        ITaxExtensionSap TaxExtension { get; set; }
        DateTime TaxInvoiceDate { get; set; }
        string TaxInvoiceNo { get; set; }
        double TotalDiscount { get; set; }
        double TotalDiscountFC { get; set; }
        double TotalDiscountSC { get; set; }
        double TotalEqualizationTax { get; set; }
        double TotalEqualizationTaxFC { get; set; }
        double TotalEqualizationTaxSC { get; set; }
        string TrackingNumber { get; set; }
        int TransNum { get; set; }
        int TransportationCode { get; set; }
        DateTime UpdateDate { get; set; }
        DateTime UpdateTime { get; set; }
        BoYesNoEnum UseBillToAddrToDetermineTax { get; set; }
        BoYesNoEnum UseCorrectionVATGroup { get; set; }
        int UserSign { get; set; }
        BoYesNoEnum UseShpdGoodsAct { get; set; }
        DateTime VatDate { get; set; }
        double VatPercent { get; set; }
        string VATRegNum { get; set; }
        double VatSum { get; set; }
        double VatSumFc { get; set; }
        double VatSumSys { get; set; }
        string VehiclePlate { get; set; }
        BoDocWhsUpdateTypes WareHouseUpdateType { get; set; }
        List<IWithholdingTaxDataSap> WithholdingTaxData { get; set; }
        List<IWithholdingTaxDataWTXSap> WithholdingTaxDataWTXCollection { get; set; }
        double WTAmount { get; set; }
        double WTAmountFC { get; set; }
        double WTAmountSC { get; set; }
        double WTApplied { get; set; }
        double WTAppliedFC { get; set; }
        double WTAppliedSC { get; set; }
        double WTExemptedAmount { get; set; }
        double WTExemptedAmountFC { get; set; }
        double WTExemptedAmountSC { get; set; }
        double WTNonSubjectAmount { get; set; }
        double WTNonSubjectAmountFC { get; set; }
        double WTNonSubjectAmountSC { get; set; }
    }
}

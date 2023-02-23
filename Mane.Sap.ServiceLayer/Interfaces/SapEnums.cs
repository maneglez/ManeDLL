using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Sap.ServiceLayer.Interfaces
{
    public enum CommissionTradeTypeEnum
    {
        ct_Empty = 0,
        ct_SalesAgent = 1,
        ct_PurchaseAgent = 2,
        ct_Consignor = 3
    }
    public enum EDocStatusEnum
    {
        edoc_New = 0,
        edoc_Pending = 1,
        edoc_Sent = 2,
        edoc_Error = 3,
        edoc_Ok = 4
    }
    public enum ImportOrExportTypeEnum
    {
        et_IpmortsOrExports = 0,
        et_SEZ_Developer = 1,
        et_SEZ_Unit = 2,
        et_Deemed_ImportsOrExports = 3
    }
    public enum EWBSupplyTypeEnum
    {
        ewb_st_Inward = 0,
        ewb_st_Outward = 1
    }
    public enum EWBTransactionTypeEnum
    {
        ewb_tt_Regular = 0,
        ewb_tt_BillToShipTo = 1,
        ewb_tt_BillFromDispathFrom = 2,
        ewb_tt_CombinationBillAndShip = 3
    }

    public enum ReferencedObjectTypeEnum
    {
        rot_ExternalDocument = -1,
        rot_SalesInvoice = 13,
        rot_SalesCreditNote = 14,
        rot_DeliveryNotes = 15,
        rot_Return = 16,
        rot_SalesOrder = 17,
        rot_PurchaseInvoice = 18,
        rot_PurchaseCreditNote = 19,
        rot_GoodsReceiptPO = 20,
        rot_GoodsReturn = 21,
        rot_PurchaseOrder = 22,
        rot_SalesQuotation = 23,
        rot_IncomingPayments = 24,
        rot_JournalEntry = 30,
        rot_OutgoingPayments = 46,
        rot_ChecksforPayment = 57,
        rot_GoodsReceipt = 59,
        rot_GoodsIssue = 60,
        rot_InventoryTransfer = 67,
        rot_LandedCosts = 69,
        rot_MaterialRevaluation = 162,
        rot_CorrectionPurchaseInvoice = 163,
        rot_CorrectionSalesInvoice = 165,
        rot_ProductionOrder = 202,
        rot_DownPaymentIncoming = 203,
        rot_DownPaymentOutgoing = 204,
        rot_SalesTaxInvoice = 280,
        rot_PurchaseTaxInvoice = 281,
        rot_InternalReconciliation = 321,
        rot_OriginalInvoice = 13001,
        rot_InventoryPosting = 10000071,
        rot_ReturnRequest = 234000031,
        rot_GoodsReturnRequest = 234000032,
        rot_PurchaseQuotation = 540000006,
        rot_InventoryTransferRequest = 1250000001,
        rot_InventoryCounting = 1470000065,
        rot_PurchaseRequest = 1470000113
    }
    public enum LinkReferenceTypeEnum
    {
        lrt_00 = 0,
        lrt_01 = 1,
        lrt_02 = 2,
        lrt_03 = 3,
        lrt_04 = 4,
        lrt_05 = 5,
        lrt_06 = 6,
        lrt_07 = 7,
        lrt_08 = 8,
        lrt_MX_08 = 9,
        lrt_MX_09 = 10
    }
    public enum PriceModeDocumentEnum
    {
        pmdNet = 0,
        pmdGross = 1,
        pmdNetAndGross = 2
    }
    public enum BoSoStatus
    {
        so_Open = 0,
        so_Closed = 1
    }
    public enum BoDocSummaryTypes
    {
        dNoSummary = 0,
        dByItems = 1,
        dByDocuments = 2
    }
    public enum BoDocWhsUpdateTypes
    {
        dwh_No = 0,
        dwh_OrdersFromVendors = 1,
        dwh_CustomerOrders = 2,
        dwh_Consignment = 3,
        dwh_Stock = 4
    }
    public enum BoPayTermDueTypes
    {
        pdt_MonthEnd = 0,
        pdt_HalfMonth = 1,
        pdt_MonthStart = 2,
        pdt_None = 3
    }
    public enum PrintStatusEnum
    {
        psNo = 0,
        psYes = 1,
        psAmended = 2
    }
    public enum BoInterimDocTypes
    {
        boidt_None = 0,
        boidt_ExchangeRate = 1,
        boidt_CashDiscount = 2
    }
    public enum FolioLetterEnum
    {
        fLetterA = 0,
        fLetterB = 1,
        fLetterC = 2,
        fLetterE = 3,
        fLetterM = 4,
        fLetterR = 5,
        fLetterT = 6,
        fLetterX = 7,
        fLetterEMPTY = 8
    }
    public enum GSTTransactionTypeEnum
    {
        gsttrantyp_BillOfSupply = 0,
        gsttrantyp_GSTTaxInvoice = 1,
        gsttrantyp_GSTDebitMemo = 2
    }
    public enum ElecCommStatusEnum
    {
        ecsApproved = 0,
        ecsPendingApproval = 1,
        ecsRejected = 2
    }
    public enum BoFatherCardTypes
    {
        cPayments_sum = 0,
        cDelivery_sum = 1
    }
    public enum ElectronicDocGenTypeEnum
    {
        edgt_NotRelevant = 0,
        edgt_Generate = 1,
        edgt_GenerateLater = 2
    }
    public enum RelatedDocumentTypeEnum
    {
        rdt_Payment = 24,
        rdt_Reconciliation = 321
    }
    public enum ElectronicDocProtocolCodeEnum
    {
        edpc_Invalid = 0,
        edpc_GEN = 1,
        edpc_EET = 2,
        edpc_CFDI = 3,
        edpc_FPA = 4,
        edpc_MTD = 5,
        edpc_EWB = 6,
        edpc_PEPPOL = 7,
        edpc_HOI = 8,
        edpc_MYF = 9,
        edpc_EIS = 10,
        edpc_IIS = 11,
        edpc_IIS_Annual = 12,
        edpc_DIGIPOORT = 13,
        edpc_EBooks = 14,
        edpc_DOX = 15,
        edpc_RTIE = 16,
        edpc_EBilling = 17
    }
    public enum BoAdEpnsTaxTypes
    {
        aext_NormalTax = 0,
        aext_NoTax = 1,
        aext_UseTax = 2
    }
    public enum BoDocSpecialLineType
    {
        dslt_Text = 0,
        dslt_Subtotal = 1
    }
    public enum LineTypeEnum
    {
        ltDocument = 0,
        ltRounding = 1,
        ltVat = 2
    }
    public enum DownPaymentTypeEnum
    {
        dptRequest = 0,
        dptInvoice = 1
    }
    public enum EDocGenerationTypeEnum
    {
        edocGenerate = 0,
        edocGenerateLater = 1,
        edocNotRelevant = 2
    }
    public enum DocumentDeliveryTypeEnum
    {
        ddtNoneSelected = 0,
        ddtCreateOnlineDocument = 1,
        ddtPostToAribaNetwork = 2
    }
    public enum BoDocumentSubType
    {
        bod_None = 0,
        bod_InvoiceExempt = 1,
        bod_DebitMemo = 2,
        bod_Bill = 3,
        bod_ExemptBill = 4,
        bod_PurchaseDebitMemo = 5,
        bod_ExportInvoice = 6,
        bod_GSTTaxInvoice = 7,
        bod_GSTDebitMemo = 8,
        bod_RefundVoucher = 9
    }
    public enum BoDocumentTypes
    {
        dDocument_Items = 0,
        dDocument_Service = 1
    }
    public enum BoAdEpnsDistribMethods
    {
        aedm_None = 0,
        aedm_Quantity = 1,
        aedm_Volume = 2,
        aedm_Weight = 3,
        aedm_Equally = 4,
        aedm_RowTotal = 5
    }
    public enum DocumentAuthorizationStatusEnum
    {
        dasWithout = 0,
        dasPending = 1,
        dasApproved = 2,
        dasRejected = 3,
        dasGenerated = 4,
        dasGeneratedbyAuthorizer = 5,
        dasCancelled = 6
    }
    public enum ClosingOptionEnum
    {
        coByCurrentSystemDate = 1,
        coByOriginalDocumentDate = 2,
        coBySpecifiedDate = 3
    }
    public enum CancelStatusEnum
    {
        csYes = 0,
        csNo = 1,
        csCancellation = 2
    }
    public enum BoYesNoEnum
    {
        tNO = 0,
        tYES = 1
    }
    public enum BoTaxTypes
    {
        tt_Yes = 0,
        tt_No = 1,
        tt_UseTax = 2,
        tt_OffsetTax = 3
    }
    public enum BoTransactionTypeEnum
    {
        botrntComplete = 0,
        botrntReject = 1
    }
    public enum BoItemTreeTypes
    {
        iNotATree = 0,
        iAssemblyTree = 1,
        iSalesTree = 2,
        iProductionTree = 3,
        iTemplateTree = 4,
        iIngredient = 5
    }
    public enum BoDocumentLinePickStatus
    {
        dlps_Picked = 0,
        dlps_NotPicked = 1,
        dlps_ReleasedForPicking = 2,
        dlps_PartiallyPicked = 3
    }
    public enum BoDocLineType
    {
        dlt_Regular = 0,
        dlt_Alternative = 1
    }

    public enum BoStatus
    {
        bost_Open = 0,
        bost_Close = 1,
        bost_Paid = 2,
        bost_Delivered = 3
    }
    public enum BoCorInvItemStatus
    {
        ciis_Was = 0,
        ciis_ShouldBe = 1
    }
    public enum BoExpenseOperationTypeEnum
    {
        bo_ExpOpType_ProfessionalServices = 0,
        bo_ExpOpType_RentingAssets = 1,
        bo_ExpOpType_Others = 2,
        bo_ExpOpType_None = 3
    }
    public enum GeneratedAssetStatusEnum
    {
        gasOpen = 0,
        gasClosed = 1
    }
    public enum BoDocItemType
    {
        dit_Item = 0,
        dit_Resource = 1
    }
    public enum BoObjectTypes
    {
        oChartOfAccounts = 1,
        oBusinessPartners = 2,
        oBanks = 3,
        oItems = 4,
        oVatGroups = 5,
        oPriceLists = 6,
        oSpecialPrices = 7,
        oItemProperties = 8,
        oBusinessPartnerGroups = 10,
        oUsers = 12,
        oInvoices = 13,
        oCreditNotes = 14,
        oDeliveryNotes = 15,
        oReturns = 16,
        oOrders = 17,
        oPurchaseInvoices = 18,
        oPurchaseCreditNotes = 19,
        oPurchaseDeliveryNotes = 20,
        oPurchaseReturns = 21,
        oPurchaseOrders = 22,
        oQuotations = 23,
        oIncomingPayments = 24,
        oJournalVouchers = 28,
        oJournalEntries = 30,
        oStockTakings = 31,
        oContacts = 33,
        oCreditCards = 36,
        oCurrencyCodes = 37,
        oPaymentTermsTypes = 40,
        oBankPages = 42,
        oManufacturers = 43,
        oVendorPayments = 46,
        oLandedCostsCodes = 48,
        oShippingTypes = 49,
        oLengthMeasures = 50,
        oWeightMeasures = 51,
        oItemGroups = 52,
        oSalesPersons = 53,
        oCustomsGroups = 56,
        oChecksforPayment = 57,
        oInventoryGenEntry = 59,
        oInventoryGenExit = 60,
        oWarehouses = 64,
        oCommissionGroups = 65,
        oProductTrees = 66,
        oStockTransfer = 67,
        oWorkOrders = 68,
        oCreditPaymentMethods = 70,
        oCreditCardPayments = 71,
        oAlternateCatNum = 73,
        oBudget = 77,
        oBudgetDistribution = 78,
        oMessages = 81,
        oBudgetScenarios = 91,
        oUserDefaultGroups = 93,
        oSalesOpportunities = 97,
        oSalesStages = 101,
        oActivityTypes = 103,
        oActivityLocations = 104,
        oDrafts = 112,
        oDeductionTaxHierarchies = 116,
        oDeductionTaxGroups = 117,
        oAdditionalExpenses = 125,
        oSalesTaxAuthorities = 126,
        oSalesTaxAuthoritiesTypes = 127,
        oSalesTaxCodes = 128,
        oQueryCategories = 134,
        oFactoringIndicators = 138,
        oPaymentsDrafts = 140,
        oAccountSegmentations = 142,
        oAccountSegmentationCategories = 143,
        oWarehouseLocations = 144,
        oForms1099 = 145,
        oInventoryCycles = 146,
        oWizardPaymentMethods = 147,
        oBPPriorities = 150,
        oDunningLetters = 151,
        oUserFields = 152,
        oUserTables = 153,
        oPickLists = 156,
        oPaymentRunExport = 158,
        oUserQueries = 160,
        oMaterialRevaluation = 162,
        oCorrectionPurchaseInvoice = 163,
        oCorrectionPurchaseInvoiceReversal = 164,
        oCorrectionInvoice = 165,
        oCorrectionInvoiceReversal = 166,
        oContractTemplates = 170,
        oEmployeesInfo = 171,
        oCustomerEquipmentCards = 176,
        oWithholdingTaxCodes = 178,
        oBillOfExchangeTransactions = 182,
        oKnowledgeBaseSolutions = 189,
        oServiceContracts = 190,
        oServiceCalls = 191,
        oUserKeys = 193,
        oQueue = 194,
        oSalesForecast = 198,
        oTerritories = 200,
        oIndustries = 201,
        oProductionOrders = 202,
        oDownPayments = 203,
        oPurchaseDownPayments = 204,
        oPackagesTypes = 205,
        oUserObjectsMD = 206,
        oTeams = 211,
        oRelationships = 212,
        oUserPermissionTree = 214,
        oActivityStatus = 217,
        oChooseFromList = 218,
        oFormattedSearches = 219,
        oAttachments2 = 221,
        oUserLanguages = 223,
        oMultiLanguageTranslations = 224,
        oDynamicSystemStrings = 229,
        oHouseBankAccounts = 231,
        oBusinessPlaces = 247,
        oLocalEra = 250,
        oNotaFiscalCFOP = 258,
        oNotaFiscalCST = 259,
        oNotaFiscalUsage = 260,
        oClosingDateProcedure = 261,
        oBPFiscalRegistryID = 278,
        oSalesTaxInvoice = 280,
        oPurchaseTaxInvoice = 281,
        BoRecordset = 300,
        BoRecordsetEx = 301,
        BoBridge = 305,
        oStockTransferDraft = 1179,
        oReturnRequest = 234000031,
        oGoodsReturnRequest = 234000032,
        oPurchaseQuotations = 540000006,
        oInventoryTransferRequest = 1250000001,
        oPurchaseRequest = 1470000113
    }
}

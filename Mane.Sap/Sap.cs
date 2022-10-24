using Mane.BD;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Sap
{
    public static partial class Sap
    {
        private static ConexionSap ConexionActual;
        private const string NombreConexionBd = "Mane.Sap_Connection";
        public static bool AutoDisconect { get; set; } = true;
        private static Company comp;
        public static Company Company => comp;
        public static ConexionSapCollection Conexiones = new ConexionSapCollection();
        public static string LastError { get; private set; }
        public static string NewObjectKey { get; private set; }
        public static bool connect(string nombreConexion = "")
        {
            try
            {
                if (comp == null) comp = new Company();
                if (comp.Connected && ConexionActual?.Nombre == nombreConexion) return true;
                else
                {
                    disconnect();
                }
                if (Conexiones.Count == 0) throw new Exception("No hay ni una conexion configurada");
                ConexionSap con;
                if (nombreConexion == "") con = Conexiones[0];
                else
                {
                    con = Conexiones.Find(nombreConexion);
                    if (con == null) throw new Exception($"La conexion {nombreConexion} no existe");
                }
                ConexionActual = con;
                comp.Server = con.Server;
                if (con.LicenseServer != "")
                    comp.LicenseServer = con.LicenseServer;
                if (con.SLDServer != "")
                    comp.SLDServer = con.SLDServer;
                comp.CompanyDB = con.DbCompany;
                comp.DbUserName = con.DbUser;
                comp.DbPassword = con.DbPassword;
                comp.UserName = con.User;
                comp.Password = con.Password;
                comp.DbServerType = (BoDataServerTypes)con.tipoServidor;
                comp.language = BoSuppLangs.ln_Spanish_La;
                comp.UseTrusted = false;
                if (comp.Connect() != 0) throw new Exception(comp.GetLastErrorDescription());

            }
            catch (Exception e)
            {
                LastError = e.Message;
                return false;
            }
            return true;
        }
        
        public static bool TestConnection(ConexionSap con)
        {
            try
            {
                if (comp == null) comp = new Company();
                if (comp.Connected) disconnect();

                comp.Server = con.Server;
                if (con.LicenseServer != "")
                    comp.LicenseServer = con.LicenseServer;
                if (con.SLDServer != "")
                    comp.SLDServer = con.SLDServer;
                comp.CompanyDB = con.DbCompany;
                comp.DbUserName = con.DbUser;
                comp.DbPassword = con.DbPassword;
                comp.UserName = con.User;
                comp.Password = con.Password;
                comp.DbServerType = (BoDataServerTypes)con.tipoServidor;
                comp.language = BoSuppLangs.ln_Spanish_La;
                comp.UseTrusted = false;
                if (comp.Connect() != 0) throw new Exception(comp.GetLastErrorDescription());
                disconnect();

            }
            catch (Exception e)
            {
                LastError = e.Message;
                return false;
            }
            return true;
        }

        public static int GetDocNum(string tabla,int docEntry = 0)
        {
            if (string.IsNullOrEmpty(NewObjectKey) && docEntry == 0) return 0;
            if (docEntry == 0) docEntry = Convert.ToInt32(NewObjectKey);
            var c = ConexionActual?.ToBdCon();
            c.Nombre = NombreConexionBd;
            Bd.Conexiones.Add(c);
            int docNum = Convert.ToInt32(Bd.Query(tabla).where("DocEntry", docEntry).getScalar(NombreConexionBd));
            Bd.Conexiones.Remove(c);
            return docNum;
        }

        public static void disconnect()
        {
            if (comp == null) return;
            if (!comp.Connected) return;
            comp.Disconnect();
            GC.Collect();
        }
        public class ConexionSapCollection : List<ConexionSap>
        {
            /// <summary>
            /// Agrega una nueva conexión
            /// </summary>
            /// <param name="con"></param>
            new public void Add(ConexionSap con)
            {
                if (con.Nombre == "") con.Nombre = $"Conexion{this.Count}";
                else
                    foreach (ConexionSap c in this)
                    {
                        if (c.Nombre == con.Nombre) throw new Exception("El nombre de la conexión ya existe");
                    }
                base.Add(con);
                
            }
            /// <summary>
            /// Obtiene la conexion por nombre
            /// </summary>
            /// <param name="nombreConexion"></param>
            /// <returns>Obtine la primera conexion que coincide con el nombre proporcionado</returns>
            public ConexionSap Find(string nombreConexion)
            {
                return this.Find(c => c.Nombre == nombreConexion);

            }

        }

    }

    public class ConexionSap
    {
        public string Nombre,
            Server,
            LicenseServer,
            SLDServer,
            DbUser,
            DbPassword,
            DbCompany,
            User,
            Password;
        public TipoServidorSap tipoServidor;
        public ConexionSap()
        {
            Nombre = Server = LicenseServer = SLDServer = DbUser = DbPassword = DbCompany = User = Password = "";
            tipoServidor = TipoServidorSap.dst_MSSQL;
        }
        public Conexion ToBdCon()
        {
            var c = new Conexion();
            c.Servidor = Server;
            c.NombreBD = DbCompany;
            c.TipoDeBaseDeDatos = TipoDeBd.SqlServer;
            c.Usuario = DbUser;
            c.Contrasena = DbPassword;
            return c;
        }
    }

    #region Copias de enums de SAP

    public enum LenguajeSap
    {
        ln_Null = 0,
        ln_Hebrew = 1,
        ln_Spanish_Ar = 2,
        ln_English = 3,
        ln_Polish = 5,
        ln_English_Sg = 6,
        ln_Spanish_Pa = 7,
        ln_English_Gb = 8,
        ln_German = 9,
        ln_Serbian = 10,
        ln_Danish = 11,
        ln_Norwegian = 12,
        ln_Italian = 13,
        ln_Hungarian = 14,
        ln_Chinese = 0xF,
        ln_Dutch = 0x10,
        ln_Finnish = 17,
        ln_Greek = 18,
        ln_Portuguese = 19,
        ln_Swedish = 20,
        ln_English_Cy = 21,
        ln_French = 22,
        ln_Spanish = 23,
        ln_Russian = 24,
        ln_Spanish_La = 25,
        ln_Czech_Cz = 26,
        ln_Slovak_Sk = 27,
        ln_Korean_Kr = 28,
        ln_Portuguese_Br = 29,
        ln_Japanese_Jp = 30,
        ln_Turkish_Tr = 0x1F,
        ln_Arabic = 0x20,
        ln_Ukrainian = 33,
        ln_TrdtnlChinese_Hk = 35
    }
    public enum TipoServidorSap
    {
        dst_MSSQL = 1,
        dst_DB_2 = 2,
        dst_SYBASE = 3,
        dst_MSSQL2005 = 4,
        dst_MAXDB = 5,
        dst_MSSQL2008 = 6,
        dst_MSSQL2012 = 7,
        dst_MSSQL2014 = 8,
        dst_HANADB = 9,
        dst_MSSQL2016 = 10,
        dst_MSSQL2017 = 11,
        dst_MSSQL2019 = 0xF
    }
    
    public enum TipoObjetoSap
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
    #endregion
}

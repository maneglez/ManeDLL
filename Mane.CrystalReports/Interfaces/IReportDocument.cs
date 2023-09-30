//using CrystalDecisions.CrystalReports.Engine;
using Mane.CrystalReports.Interfaces;
using System.Data;
using System.Drawing.Printing;

namespace Mane.CrystalReports
{
    public interface IReportDocument
    {
        void ExportToDisk(CrystalExportFormat format, string ruta);
        void Load(string rutaRpt);
        void Print(PrinterSettings printerSettings, PageSettings pageSettings, bool reformatReportPageSettings);
        void Refresh();
        void SetDatabaseLogon(string user, string password);
        void SetDatabaseLogon(string user, string password, string server, string database);
        void SetDataSource(DataSet dataSet);
        void SetDataSource(DataTable dataTable);
        void SetParameterValue(int index, object val);
        void SetParameterValue(string name, object val);
        IDatabase Database { get; }
    }
}
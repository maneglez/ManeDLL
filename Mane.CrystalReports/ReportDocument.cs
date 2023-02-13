using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using cRpt = CrystalDecisions.CrystalReports.Engine.ReportDocument;
namespace Mane.CrystalReports
{
    public class ReportDocument : IDisposable
    {
        private static dynamic  Rpt;
        public ReportDocument()
        {
            Rpt = Activator.CreateInstance(Dependencias.ReportDocumentType);
           // Rpt = new cRpt();
        }
        public void Load(string rutaRpt) =>
            Rpt.Load(rutaRpt);
        public void Refresh()
        {
            Rpt.Refresh();
        }
        public void SetParameterValue(int index,object val)
        {
            Rpt.SetParameterValue(index, val);
        }
        public void SetParameterValue(string name,object val)
        {
            Rpt.SetParameterValue(name, val);
        }
        public void SetDatabaseLogon(string user, string password)
        {
            Rpt.SetDatabaseLogon(user, password);
        }
        public void SetDatabaseLogon(string user, string password, string server, string database)
        {
            Rpt.SetDatabaseLogon(user, password, server, database);
        }
        public void SetDataSource(DataSet dataSet)
        {
            Rpt.SetDataSource(dataSet);
        }
        public void SetDataSource(DataTable dataTable)
        {
            Rpt.SetDataSource(dataTable);
        }
        public void ExportToDisk(CrystalExportFormat format,string ruta)
        {
            var tipo = (dynamic)Enum.Parse(Dependencias.ExportFormatType, format.ToString());
           Rpt.ExportToDisk(tipo, ruta);
        }
        public void Show()
        {
            using(var fm = new VisorCrystal())
            {
                fm.SetReportSource(Rpt);
                fm.ShowDialog();
            }
        }
        public void Print(PrinterSettings printerSettings,PageSettings pageSettings,bool reformatReportPageSettings)
        {
            Rpt.PrintToPrinter(printerSettings, pageSettings, reformatReportPageSettings);
        }

        public void Dispose()
        {
            Rpt.Close();
            Rpt.Dispose();
        }
    }
}

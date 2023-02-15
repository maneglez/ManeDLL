using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
//using cRpt = CrystalDecisions.CrystalReports.Engine.ReportDocument;
namespace Mane.Helpers.CrystalReports
{
    public class ReportDocument : IDisposable
    {
        public static event EventHandler<ManeExceptionEventArgs> OnException;
        private static dynamic  Rpt;
        private void ExceptionHandler(Exception e)
        {
            if (OnException != null)
                OnException.Invoke(this, new ManeExceptionEventArgs(e));
            else
                throw e;
        }
        public ReportDocument()
        {
            try
            {
                Rpt = Activator.CreateInstance(Dependencias.ReportDocumentType);
            }
            catch (Exception e)
            {
                ExceptionHandler(e);
            }
            
           
        }
        public void Load(string rutaRpt)
        {
            try
            {
                Rpt.Load(rutaRpt);
            }
            catch (Exception e)
            {
                ExceptionHandler(e);
            }
        }
            
        public void Refresh()
        {
            try
            {
                Rpt.Refresh();
            }
            catch (Exception e)
            {
                ExceptionHandler(e);
            }
        }
        public void SetParameterValue(int index,object val)
        {
            try
            {
            Rpt.SetParameterValue(index, val);
            }
            catch (Exception e)
            {
                ExceptionHandler(e);
            }

        }
        public void SetParameterValue(string name,object val)
        {
            try
            {
            Rpt.SetParameterValue(name, val);
            }
            catch (Exception e)
            {
                ExceptionHandler(e);
            }
        }
        public void SetDatabaseLogon(string user, string password)
        {
            try
            { 
            Rpt.SetDatabaseLogon(user, password);
            }
            catch (Exception e)
            {
                ExceptionHandler(e);
            }
        }
        public void SetDatabaseLogon(string user, string password, string server, string database)
        {
            try
            {
            Rpt.SetDatabaseLogon(user, password, server, database);
            }
            catch (Exception e)
            {
                ExceptionHandler(e);
            }
        }
        public void SetDataSource(DataSet dataSet)
        {
            try
            {
            Rpt.SetDataSource(dataSet);
            }
            catch (Exception e)
            {
                ExceptionHandler(e);
            }
        }
        public void SetDataSource(DataTable dataTable)
        {
            try
            {
            Rpt.SetDataSource(dataTable);
            }
            catch (Exception e)
            {
                ExceptionHandler(e);
            }
        }
        public void ExportToDisk(CrystalExportFormat format,string ruta)
        {
            try
            {
            var tipo = (dynamic)Enum.Parse(Dependencias.ExportFormatType, format.ToString());
            Rpt.ExportToDisk(tipo, ruta);
            }
            catch (Exception e)
            {
                ExceptionHandler(e);
            }
        }
        public void Show()
        {
            using(var fm = new VisorCrystal())
            {
                try
                {
                fm.SetReportSource(Rpt);
                fm.ShowDialog();
                }
                catch (Exception e)
                {
                    ExceptionHandler(e);
                }
            }
        }
        public void Print(PrinterSettings printerSettings,PageSettings pageSettings,bool reformatReportPageSettings)
        {
            try
            {
            Rpt.PrintToPrinter(printerSettings, pageSettings, reformatReportPageSettings);
            }
            catch (Exception e)
            {
                ExceptionHandler(e);
            }
        }

        public void Dispose()
        {
            Rpt.Close();
            Rpt.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mane.Helpers
{
    public class Errores
    {
        public static void ControlarExepciones(bool logException = true, bool showMessage = default)
        {
            Application.ThreadException += (s,e) =>
            {
                try
                {
                   if(e.Exception == null)
                    {
                        if (logException)
                            Log.Add("Thread exception: " + e.ToString());
                        if (showMessage)
                            MessageBox.Show("Thread Exception: " + e.ToString());
                    }
                    else
                    {
                        if (logException)
                            Log.Add("Thread exception: " + e.Exception?.ToString());
                        if (showMessage)
                            MessageBox.Show("Thread Exception: " + e.Exception?.Message);
                    }
                }
                catch (Exception)
                {
                }
            };
            //Establecer en modo Catch excepciones no controladas
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            
            AppDomain.CurrentDomain.UnhandledException +=
       new UnhandledExceptionEventHandler((s,e) =>
       {
           try
           {
               if(logException)
                    Log.Add("Domain exception: " + e.ExceptionObject?.ToString());
               if(showMessage)
                MsgBox.error("Domain exception: " + e.ExceptionObject?.ToString());
           }
           catch (Exception)
           {

           }
       });
        }
    }
}

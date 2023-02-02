using Mane.Helpers;
using Mane.Licenciamiento;
using Microsoft.Win32;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mane.Licenciamiento.Generador
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new frmGenerador());
            }
            catch (Exception e)
            {
                MessageBox.Show($"Excepcion no controlada: {e.Message}");
            }

        }

    }
}

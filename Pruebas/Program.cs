using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Mane.CommonModules;
using Mane.CommonModules.WMS;

namespace Pruebas
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            do
            {
                Prueba();
                Console.WriteLine();
                Console.Write("Presione enter para volver a probar:");
                Console.ReadLine();
            } while (true);
        }
        static void Writeln(string text) => Console.WriteLine(text);
        static void Write(string text) => Console.Write(text);
        static void Prueba()
        {
            var conf = new Mane.CommonModules.Conection();
            conf.ServerNameSap = "TIE-LAP1";
            conf.ServerNameSql = "TIE-LAP1";
            conf.SapUser = "manager";
            conf.SapPassword = "1234";
            conf.DbUser = "sa";
            conf.DbPassword = "Passw0rd";
            conf.SapSqlServerType = "dst_MSSQL2017";
            conf.DbName = "SBODemoMX";
            Configuration.SetConnection(conf);
            Application.EnableVisualStyles();
            using (var fm = new Conteo())
                fm.ShowDialog();
        }
        
    }
}

using Mane.Sap.ServiceLayer;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Mane.CFDI;

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

        static void Prueba()
        {
            var ruta = @"C:\Users\TIE\Desktop\mane\CAMPANARIO\05 Mayo Xml Egresos";
            var archivos = Directory.GetFiles(ruta);
            foreach (var archivo in archivos)
            {
                if(Cfdi.GetVersionCfdi(archivo) == CfdiVersion.Cfdi4_0)
                {
                    var cfdi = Mane.CFDI.Cfdi.Load(archivo);
                    Console.Write(cfdi.Total.ToString("c"));
                }
            }
            
        }
        
    }
}

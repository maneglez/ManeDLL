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
            var ruta = @"C:\Users\TIE\Documents\Mane\xsd33\cfdi3.xml";
           var cfd = Mane.CFDI.Cfdi.Load(ruta);
            Console.Write(cfd.Total);
        }
        
    }
}

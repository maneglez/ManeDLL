using Mane.Sap.ServiceLayer;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

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
            var cfdi =  Mane.CFDI.v4.Comprobante.CargarDesdeArchivo(@"C:\Users\TIE\Desktop\mane\CAMPANARIO\05 Mayo Xml Egresos\1de0d5d9-4edc-4d0a-8738-cbca64912bd8.xml");
            Console.WriteLine(cfdi.Total.ToString("c"));
            return;
           if(SapSrvLayer.Conexiones.Count == 0)
            {
                var c = new ConexionSrvLayer
                {
                    CompanyDB = "XML",
                    User = "manager",
                    Password = "1234",
                    //por defecto el puerto es 50000
                    //el server es localhost
                    //y el protocol es https
                };
                SapSrvLayer.Conexiones.Add(c);
            }


        }
        
    }
}

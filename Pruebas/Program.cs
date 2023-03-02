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
            var cfdi =  Mane.CFDI.v4.Comprobante.CargarDesdeArchivo(@"C:\Users\TIE\Desktop\mane\Nutryplus\PO01-2023-1400005738.xml");
            Console.WriteLine(cfdi.Complemento.PagosV2.Pago[0].Monto.ToString("c"));
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

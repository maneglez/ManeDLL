using Mane.CrystalReports;
using System;

namespace Pruebas
{
    internal class Program
    {
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
            var rutaRpt = @"C:\Users\Mane\Documents\chamba\prueba.rpt";
            //var rutaPdf = @"C:\Users\Mane\Documents\chamba\prueba.pdf";
            try
            {
              var tiempo =  Mane.Helpers.Utils.CuantoTiempoTarda(() =>
                {
                    var rpt = new ReportDocument();
                    rpt.Load(rutaRpt);
                    rpt.Show();
                });
                Console.WriteLine($"t = {tiempo:n2} ms");
          
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
        
    }
}

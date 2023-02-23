using Mane.Sap.ServiceLayer;
using System;
using System.Windows.Forms;

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
           if(SapSrvLayer.Conexiones.Count == 0)
            {
                var c = new ConexionSrvLayer
                {
                    CompanyDB = "XML",
                    User = "manager",
                    Password = "1234",
                    Server = "localhost",
                    Port = 50000
                };
                SapSrvLayer.Conexiones.Add(c);
            }
            Console.WriteLine("Ingrese una consulta para service layer: ");
            try
            {
               var result = SapSrvLayer.GET(Console.ReadLine());
                Console.WriteLine("Resultado:");
                Console.WriteLine(result.Content);
                Clipboard.SetText(result.Content);
                Console.WriteLine("Copiado al portapapeles");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrió un error: " + e.Message);
            }

        }
        
    }
}

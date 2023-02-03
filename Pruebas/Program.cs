using Mane.BD;
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
            if (Bd.Conexiones.Count == 0)
            {
                var conn = new Conexion
                {
                    Servidor = "https://localhost:44380/ManeBdWebService.asmx",
                    TipoDeBaseDeDatos = TipoDeBd.ApiWeb,
                    SubTipoDeBD = TipoDeBd.SqlServer,
                    Usuario = "mane",
                    Contrasena = "1234",
                    Nombre = "sap",
                    TimeOut = 60
                };
                Bd.Conexiones.Add(conn);
            }
            try
            {
                var result = "Resultado: " + Bd.Query("OCRD").Select("CardName").Limit(1).GetScalar()?.ToString();
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
        
    }
}

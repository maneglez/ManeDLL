using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using Mane.Security;

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
            TokenGenerator.CreateToken(1, out string msg);
            Writeln(msg);
        }
        
    }
}

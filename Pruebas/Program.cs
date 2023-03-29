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
            using (var fm = new Mane.BD.Forms.GestionarConexiones())
                fm.ShowDialog();
        }
        
    }
}

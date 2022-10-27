using Mane.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebas
{
    internal class Program
    {
        static void Main(string[] args)
        {
           var q = Bd.Query("tabla1 t1").Where("columa", "34")
                .Join("tabla2 t2", "t1.fk", "t2.pk").Limit(3);
            foreach (TipoDeBd tipo in Enum.GetValues(typeof(TipoDeBd)))
            {
                Console.WriteLine(q.BuildQuery(tipo) + "  " + tipo.ToString());
            }
            
            Console.Read();
                
        }
    }
}

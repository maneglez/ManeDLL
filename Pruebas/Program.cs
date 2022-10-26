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
           var q = Bd.Query("mi tabla").Where("columa", "34")
                .Join("tabla2", "fk", "id").GetQuery(TipoDeBd.SqlServer);
            Console.Write(q);
            Console.Read();
                
        }
    }
}

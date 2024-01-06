using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD
{
    public class DecimalAttribute : Attribute
    {
        internal int Length,DecimalPlaces;
        public DecimalAttribute(int length,int decimalPlaces)
        {
            Length = length;
            DecimalPlaces = decimalPlaces;
        }

    }
}

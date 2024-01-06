using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD
{
    public class VarcharAttribute : Attribute
    {
        internal int Length;
        /// <summary>
        /// Indica que el tipo sera nVarchar(len)
        /// </summary>
        /// <param name="length">Tamaño, 0 para MAX</param>
        public VarcharAttribute(int length = 100)
        {
            Length = length;
        }
    }
}

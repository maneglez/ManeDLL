using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CFDI.Interfaces
{
    /// <summary>
    /// Devuelve el XML de la addenda lista para insertar dentro del nodo Addenda del CFDI
    /// </summary>
    public interface IAddenda
    {
        string SerializarAddenda(); 
    }
}

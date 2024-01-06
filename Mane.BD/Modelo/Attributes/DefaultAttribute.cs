using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD
{
    public class DefaultAttribute
    {
        public object DefaultValue { get; set; }
        public DefaultAttribute(object DefaultValue)
        {
            this.DefaultValue = DefaultValue;
        }
    }
}

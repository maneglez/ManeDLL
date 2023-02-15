using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Helpers
{
    public class ManeExceptionEventArgs : EventArgs
    {
        public Exception Exception { get; }
       public ManeExceptionEventArgs(Exception ex) : base()
        {
            Exception = ex;
        }
    }
}

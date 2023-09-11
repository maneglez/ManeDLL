using System;

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

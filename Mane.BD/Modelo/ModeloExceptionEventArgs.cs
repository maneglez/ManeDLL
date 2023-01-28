using System;

namespace Mane.BD
{
    public class ModeloExceptionEventArgs : EventArgs
    {
        public ModeloException Exception { get; private set; }

        public ModeloExceptionEventArgs(ModeloException exception) : base()
        {
            Exception = exception;
        }
    }
}

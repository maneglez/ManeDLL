﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

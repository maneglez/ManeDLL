using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;



namespace Mane.BD
{


    public class BdExeptionEventArgs : EventArgs
    {
        public BdException Excepcion { get; set; }

        public BdExeptionEventArgs(BdException excepcion)
        {
            Excepcion = excepcion;
        }
    }
   


}

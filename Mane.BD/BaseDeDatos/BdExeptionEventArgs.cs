using System;



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

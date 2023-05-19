using System;

namespace Mane.BD
{
    public class ModeloException : Exception
    {
        private Type modelo;
        private string error;
        /// <summary>
        /// Mensaje
        /// </summary>
        public override string Message => $"Exepcion en el modelo \"{modelo.Name}\", lanzó el siguiente error \"{error}\"";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="error"></param>
        public ModeloException(Type modelo, Exception originalException) : base()
        {
            this.modelo = modelo;
            this.error = originalException.Message;
            this.OriginalExeption = originalException;
        }
        public Exception OriginalExeption { get; private set; }
    }
}

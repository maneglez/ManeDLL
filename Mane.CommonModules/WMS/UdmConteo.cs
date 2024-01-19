using Mane.Helpers.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CommonModules.WMS
{
    public class UdmConteo : VinculableConForm
    {
        private string codigoDeUnidad;
        private double cantidadPorUnidad;
        private double cantidadContadaUnidad;
        private double cantidadContada;
        private string oldCodigoUnidad;

        public string CodigoDeUnidad
        {
            get => codigoDeUnidad; set
            {
                oldCodigoUnidad = codigoDeUnidad;
                codigoDeUnidad = value;
                NotifyChange();
            }
        }
        public double CantidadPorUnidad
        {
            get => cantidadPorUnidad; set
            {
                if (value == cantidadPorUnidad)
                    return;
                cantidadPorUnidad = value;
                NotifyChange();
                cantidadContadaUnidad = 0;
                cantidadContada = 0;
                NotifyChange(nameof(CantidadContada));
                NotifyChange(nameof(CantidadContadaUnidad));
            }
        }
        public double CantidadContadaUnidad
        {
            get => cantidadContadaUnidad; set
            {
                cantidadContadaUnidad = value;
                NotifyChange();
                cantidadContada = cantidadContadaUnidad * CantidadPorUnidad;
                NotifyChange(nameof(CantidadContada));
            }
        }
        public double CantidadContada
        {
            get => cantidadContada; set
            {
                cantidadContada = value;
                NotifyChange();
                cantidadContadaUnidad = cantidadContada / CantidadPorUnidad;
                NotifyChange(nameof(CantidadContadaUnidad));
            }
        }
        public string OldCodigoUnidad => oldCodigoUnidad;
    }

}

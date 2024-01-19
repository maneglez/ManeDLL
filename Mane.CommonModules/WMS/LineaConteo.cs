using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CommonModules.WMS
{
    public class LineaConteo : Helpers.Common.LineaTraspaso
    {
        private double cantidadContada;
        private double diferencia;
        private string unidadDeMedida;
        private double articulosPorUnidad;
        private bool fijar;
        private bool contado;
        private string serieLote;
        private double quantity;

        public LineaConteo()
        {
            UnidadesDeMedida = new List<UdmConteo>();
            LineNum = -1;//Lineas nuevas
        }
        new public double Quantity
        {
            get => quantity; set
            {
                quantity = value;
                Diferencia = cantidadContada - Quantity;
            }
        }

        public double CantidadContada
        {
            get => cantidadContada; set
            {
                cantidadContada = value;
                Diferencia = cantidadContada - Quantity;
                NotifyChange();
            }
        }
        public double Diferencia
        {
            get => diferencia; set
            {
                diferencia = value;
                NotifyChange();
            }
        }
        public string UnidadDeMedida
        {
            get => unidadDeMedida; set
            {
                unidadDeMedida = value;
                NotifyChange();
            }
        }
        public double ArticulosPorUnidad
        {
            get => articulosPorUnidad; set
            {
                articulosPorUnidad = value;
                NotifyChange();
            }
        }
        public string BinCode { get; set; }
        public string SerieLote
        {
            get => serieLote; set
            {
                serieLote = value;
                NotifyChange();
            }
        }
        public int SysNumber { get; set; }
        public bool Fijar
        {
            get => fijar; set
            {
                fijar = value;
                NotifyChange();
            }
        }
        public bool Contado
        {
            get => contado; set
            {
                contado = value;
                NotifyChange();
            }
        }
        public string TipoManejo { get; set; }
        public List<UdmConteo> UnidadesDeMedida { get; set; }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is LineaConteo ln)
                return ln.ItemCode == ItemCode
                    && ln.FromWhs == FromWhs
                    && ln.BinCode == BinCode
                    && ln.SerieLote == SerieLote;
            return base.Equals(obj);
        }
    }

}

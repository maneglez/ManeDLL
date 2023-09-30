using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD.Forms
{
    public class SeleccionarGenericoAntesDeBuscarEventArgs : EventArgs
    {
        /// <summary>
        /// Consulta de busqueda
        /// </summary>
        public QueryBuilder Query { get => _query; set => _query = value; }
        /// <summary>
        /// Indica si se debe de controlar el filtro de busqueda, 
        /// True: No se aplica el filtro de busqueda por defecto, Falso: si se aplica (por defecto Falso)
        /// </summary>
        public bool HandleFilter { get; set; }
        private QueryBuilder _query;
        /// <summary>
        /// Indica el valor actual del combobox "Filtrar Por"
        /// </summary>
        public string FilterValue { get; set; }
        /// <summary>
        /// Indica el texto de busqueda Actual
        /// </summary>
        public string Busqueda {get;set;}
        public SeleccionarGenericoAntesDeBuscarEventArgs(QueryBuilder query,string filterValue,string busqueda) : base()
        {
            Query = query;
            FilterValue = filterValue;
            Busqueda = busqueda;
        }
    }
}

using Mane.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mane.CommonModules.WMS
{
    public partial class RecuentoPorUm : Form
    {
        BindingList<UdmConteo> Lineas;
        LineaConteo Linea;
        List<UnidadDeMedidaArticulo> Ums;
        HashSet<string> UnidadesAsignadas;
        public RecuentoPorUm(LineaConteo linea)
        {
            InitializeComponent();
            Linea = linea;
            dgvUm.AutoGenerateColumns = false;
            Ums = UnidadDeMedidaArticulo.Get(Linea.ItemCode);
            UnidadesAsignadas = new HashSet<string>();
            if (Ums.Count == 1)
            {
                dgvUm.AllowUserToAddRows = false;
                dgvUm.AllowUserToDeleteRows = false;
            }
            colUm.DataSource = Ums;
            colUm.ValueMember = "UomCode";
            colUm.DisplayMember = "UomName";

            Lineas = new BindingList<UdmConteo>();
            dgvUm.DataSource = Lineas;
            foreach (var item in Linea.UnidadesDeMedida)
            {
                Lineas.Add(item.CopyObject());
            }
            if (Lineas.Count == 0)
            {
                var um = Ums.Where(u => u.UomCode == Linea.UnidadDeMedida).First();
                Lineas.Add(new UdmConteo
                {
                    CodigoDeUnidad = um.UomCode,
                    CantidadPorUnidad = um.Quantity,
                    CantidadContada = Linea.CantidadContada
                });
            }
            Lineas.ListChanged += ListaCambia;
        }
        private void ListaCambia(object o, ListChangedEventArgs ev)
        {
            switch (ev.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    var udm = Lineas[ev.NewIndex];
                    var unidadEncontrada = false;
                    foreach (var item in Ums)
                    {
                        if (UnidadesAsignadas.Add(item.UomCode))
                        {
                            UnidadesAsignadas.Remove(item.UomCode);
                            udm.CodigoDeUnidad = item.UomCode;
                            unidadEncontrada = true;
                            break;
                        }
                    }
                    if (!unidadEncontrada)
                    {
                        Lineas.CancelNew(ev.NewIndex);
                        return;
                    }
                    break;
                case ListChangedType.ItemDeleted:
                    CargarUnidadesAsignadas();
                    break;
                case ListChangedType.ItemChanged:
                    if (ev.PropertyDescriptor.Name == "CodigoDeUnidad")
                    {
                        var l = Lineas[ev.NewIndex];
                        if (string.IsNullOrWhiteSpace(l.CodigoDeUnidad))
                            return;
                        //if (!UnidadesAsignadas.Add(l.CodigoDeUnidad))
                        //{
                        //    MsgBox.warning("La unidad de medida ya ha sido asignada");
                        //    UnidadesAsignadas.Remove(l.OldCodigoUnidad);
                        //    l.CodigoDeUnidad = l.OldCodigoUnidad;
                        //    return;
                        //}
                        var um1 = Ums.Where(u => u.UomCode == l.CodigoDeUnidad).First();
                        l.CantidadPorUnidad = um1.Quantity;
                        CargarUnidadesAsignadas();
                    }
                    break;
                default:
                    break;
            }
        }
        private void CargarUnidadesAsignadas()
        {
            UnidadesAsignadas.Clear();
            foreach (var item in Lineas)
            {
                UnidadesAsignadas.Add(item.CodigoDeUnidad);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Lineas.Count == 0)
            {
                MsgBox.warning("Debe de establecer almenos una UdM");
                return;
            }
            Linea.UnidadesDeMedida.Clear();
            double piezas = 0;
            foreach (var item in Lineas)
            {
                if (string.IsNullOrWhiteSpace(item.CodigoDeUnidad))
                    continue;
                Linea.UnidadesDeMedida.Add(item);
                piezas += item.CantidadContada;
            }
            Linea.ArticulosPorUnidad = 1;
            Linea.CantidadContada = piezas;
            if (Lineas.Count > 1)
                Linea.UnidadDeMedida = "UdMs Múltiples";
            else Linea.UnidadDeMedida = Lineas[0].CodigoDeUnidad;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

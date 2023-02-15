using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mane.Helpers
{
    /// <summary>
    /// Helper para funciones asincronas
    /// </summary>
    public class Asincrono
    {
        Form Parent;
        /// <summary>
        /// Helper para funciones asincronas
        /// </summary>
        /// <param name="parent">Formulario padre al cual se le modificarán los controles</param>
        public Asincrono(Form parent)
        {
            Parent = parent;
        }
        /// <summary>
        /// Establece la propiedad de SelectedIndex de un combobox específico
        /// </summary>
        /// <param name="cb">Combobox</param>
        /// <param name="index">Índice</param>
        public void SetSelectedIndex(ComboBox cb, int index)
        {
            EstablecerPropiedad(this.Parent, cb, index, PropiedadesDeControl.SelectedIndex);
        }
        /// <summary>
        /// Establece la propiedad Selected value dentro de un metodo que se ejecuta en otro hilo
        /// </summary>
        /// <param name="cb">Combobox</param>
        /// <param name="valor">Valor</param>
        public void SetSelectedValue(ComboBox cb, object valor)
        {
            EstablecerPropiedad(this.Parent, cb, valor, PropiedadesDeControl.SelectedValue);
        }

        /// <summary>
        /// Establece la propiedad Text del control
        /// </summary>
        /// <param name="ctrl">Control</param>
        /// <param name="text">Texto</param>
        public void SetText(Control ctrl, string text)
        {
            EstablecerPropiedad(this.Parent, ctrl, text, PropiedadesDeControl.Text);
        }
        /// <summary>
        /// Establece la propiedad de texto en un control dentro de una función que se ejecuta en otro hilo
        /// </summary>
        /// <param name="parent">Form padre</param>
        /// <param name="ctrl">Control</param>
        /// <param name="text">Texto</param>
        public static void SetText(Form parent, Control ctrl, string text)
        {
            EstablecerPropiedad(parent, ctrl, text, PropiedadesDeControl.Text);
        }
        /// <summary>
        /// Establece el DataSource de un ComboBox o de un DataGridView. En un ComboBox, por defecto establece
        ///  el ValueMember como el nombre de la primera columna de la tabla, y el DisplayMember como el nombre de la
        ///  segunda columna, el selected index lo establece en -1
        /// </summary>
        /// <param name="control">Combobox o DataGridView</param>
        /// <param name="dt">Tabla de datos</param>
        public void SetDataSource(Control control, DataTable dt)
        {
            EstablecerPropiedad(this.Parent, control, dt, PropiedadesDeControl.DataSource);
        }
        delegate void dSetDataSource(DataGridViewComboBoxColumn control, DataTable dt);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dt"></param>
        public void SetDataSource(DataGridViewComboBoxColumn control, DataTable dt)
        {
            if (Parent.InvokeRequired)
            {
                dSetDataSource d = new dSetDataSource(SetDataSource);
                this.Parent.Invoke(d, new object[] { control, dt });
            }
            else
            {
                control.DataSource = dt;
                control.ValueMember = dt.Columns[0].ColumnName;
                control.DisplayMember = dt.Columns[1].ColumnName;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="control"></param>
        /// <param name="dt"></param>
        public static void SetDataSource(Form parent, Control control, DataTable dt)
        {
            EstablecerPropiedad(parent, control, dt, PropiedadesDeControl.DataSource);
        }
        /// <summary>
        /// Ejecuta una función de manera Asincrona
        /// </summary>
        /// <param name="action">Función a ejecutar</param>
        public static void Ejecutar(Action action)
        {
            Task.Run(action);
        }
        /// <summary>
        /// Establece una demora
        /// </summary>
        /// <param name="Segundos">Numero de segundos a demorar</param>
        public static void Esperar(double Segundos = 1)
        {
            int MiliSegundos = Convert.ToInt32(Segundos * 1000);
            Thread.Sleep(MiliSegundos);
        }

        delegate void EstablecerPropiedadCallBack(Form parent, Control control, object valor, PropiedadesDeControl propiedad);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="control"></param>
        /// <param name="valor"></param>
        /// <param name="propiedad"></param>
        public static void EstablecerPropiedad(Form parent, Control control, object valor, PropiedadesDeControl propiedad)
        {
            if (valor == null) return;
            if (control.InvokeRequired)
            {
                EstablecerPropiedadCallBack d = new EstablecerPropiedadCallBack(EstablecerPropiedad);
                control.Invoke(d, new object[] { parent, control, valor, propiedad });
            }
            else
            {
                switch (propiedad)
                {
                    case PropiedadesDeControl.Text:
                        control.Text = (string)valor;
                        break;
                    case PropiedadesDeControl.SelectedIndex:
                        try
                        {
                            ComboBox cb = (ComboBox)control;
                            cb.SelectedIndex = (int)valor;
                        }
                        catch (Exception)
                        { }

                        break;
                    case PropiedadesDeControl.SelectedValue:
                        try
                        {
                            ComboBox cb = (ComboBox)control;
                            cb.SelectedValue = valor;
                        }
                        catch (Exception)
                        { }

                        break;
                    case PropiedadesDeControl.DataSource:
                        if (control.GetType() == typeof(ComboBox))
                        {
                            try
                            {
                                ComboBox cb2 = (ComboBox)control;
                                DataTable dt2 = (DataTable)valor;
                                if (dt2.Rows.Count == 0) return;
                                cb2.DataSource = dt2;
                                cb2.ValueMember = dt2.Columns[0].ColumnName;
                                cb2.DisplayMember = dt2.Columns[1].ColumnName;
                                cb2.SelectedIndex = -1;
                            }
                            catch (Exception) { }
                        }
                        else if (control is DataGridView)
                        {
                            try
                            {
                                DataGridView dgv = (DataGridView)control;
                                dgv.DataSource = valor;
                            }
                            catch (Exception) { }
                        }
                        break;
                    case PropiedadesDeControl.Width:
                        control.Width = (int)valor;
                        break;
                    case PropiedadesDeControl.Heigth:
                        control.Height = (int)valor;
                        break;
                    case PropiedadesDeControl.Enabled:
                        control.Enabled = (bool)valor;
                        break;
                    case PropiedadesDeControl.ForeColor:
                        control.ForeColor = (Color)valor;
                        break;
                    case PropiedadesDeControl.BackColor:
                        control.BackColor = (Color)valor;
                        break;
                    case PropiedadesDeControl.Visible:
                        control.Visible = (bool)valor;
                        break;
                    case PropiedadesDeControl.ProgressbarValue:
                        var progressbar = (ProgressBar)control;
                        progressbar.Value = (int)valor;
                        break;
                    default:
                        break;
                }
            }
        }

        delegate void EstablecerPropiedadCallBack2(Form parent, StatusStrip control, string NombreItem, object valor, PropiedadesDeControl propiedad);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="toolStrip"></param>
        /// <param name="NombreItem"></param>
        /// <param name="valor"></param>
        /// <param name="propiedad"></param>
        public static void EstablecerPropiedad(Form parent, StatusStrip toolStrip, string NombreItem, object valor, PropiedadesDeControl propiedad)
        {
            if (toolStrip.InvokeRequired)
            {
                EstablecerPropiedadCallBack2 d = new EstablecerPropiedadCallBack2(EstablecerPropiedad);
                parent.Invoke(d, new object[] { parent, toolStrip, NombreItem, valor, propiedad });
            }
            else
            {
                ToolStripItem control = null;
                bool match = false;
                foreach (ToolStripItem item in toolStrip.Items)
                {
                    if (item.Name == NombreItem)
                    {
                        control = item;
                        match = true;
                        break;
                    }
                }
                if (!match) return;
                switch (propiedad)
                {
                    case PropiedadesDeControl.Text:
                        control.Text = (string)valor;
                        break;
                    case PropiedadesDeControl.Width:
                        control.Width = (int)valor;
                        break;
                    case PropiedadesDeControl.Heigth:
                        control.Height = (int)valor;
                        break;
                    case PropiedadesDeControl.Enabled:
                        control.Enabled = (bool)valor;
                        break;
                    case PropiedadesDeControl.ForeColor:
                        control.ForeColor = (Color)valor;
                        break;
                    case PropiedadesDeControl.BackColor:
                        control.BackColor = (Color)valor;
                        break;
                    case PropiedadesDeControl.Visible:
                        control.Visible = (bool)valor;
                        break;
                    case PropiedadesDeControl.ProgressbarValue:
                        var progressbar = (ToolStripProgressBar)control;
                        progressbar.Value = (int)valor;
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public enum PropiedadesDeControl
        {
            /// <summary>
            /// 
            /// </summary>
            Text,
            /// <summary>
            /// 
            /// </summary>
            SelectedIndex,
            /// <summary>
            /// 
            /// </summary>
            SelectedValue,
            /// <summary>
            /// 
            /// </summary>
            DataSource,
            /// <summary>
            /// 
            /// </summary>
            Width,
            /// <summary>
            /// 
            /// </summary>
            Heigth,
            /// <summary>
            /// 
            /// </summary>
            Enabled,
            /// <summary>
            /// 
            /// </summary>
            ForeColor,
            /// <summary>
            /// 
            /// </summary>
            BackColor,
            /// <summary>
            /// 
            /// </summary>
            Visible,
            /// <summary>
            /// 
            /// </summary>
            ProgressbarValue

        }
        /// <summary>
        /// Ejecuta una acción después de un tiempo transcurrido
        /// </summary>
        /// <param name="funcion">Función a ejecutar</param>
        /// <param name="delayMs">Retraso en milisegundos</param>
        /// <param name="contador">Crear una variable int a nivel clase y establecerla en 0, posteriormente pasarla como parámetro</param>
        public static void EjecutarFuncionConDelay(Action funcion, int delayMs, ref int contador)
        {
            contador++;
            Thread.Sleep(delayMs);
            if (contador == 1)
                funcion.Invoke();
            contador--;
        }




    }
}

using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Mane.Helpers.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class VisorDeAvance : Form
    {
        public event EventHandler AlFinalizar;
        /// <summary>
        /// 
        /// </summary>
        public VisorDeAvance()
        {
            InitializeComponent();
            Cancelado = false;
            InfoDeAvance = "Procesando...";
            TotalAProcesar = 100;
        }
        /// <summary>
        /// Texto que aparecerá como título
        /// </summary>
        public string Titulo { set => setTitulo(value); }
        /// <summary>
        /// Texto que aparecerá como estatus del avance
        /// </summary>
        public string InfoDeAvance { set => setInfo(value); }
        /// <summary>
        /// Indica el número total de elementos a procesar (100 por defecto)
        /// </summary>
        public int TotalAProcesar { get; set; }
        /// <summary>
        /// Indica si el usuario presionó el botón de cancelar
        /// </summary>
        public bool Cancelado;
        private int procesado;

        /// <summary>
        /// La cantidad de elementos que han sido procesados
        /// </summary>
        public int Procesado { set { setProgress(value); procesado = value; } get => procesado; }

        /// <summary>
        /// Deshabilita el botón de cancelación
        /// </summary>
        public void DeshabilitarCancelacion()
        {
            if (btnDetener.InvokeRequired)
            {
                btnDetener.BeginInvoke(new MethodInvoker(DeshabilitarCancelacion));
                return;
            }
            btnDetener.Enabled = false;
        }
        /// <summary>
        /// Habilita el botón de cancelación
        /// </summary>
        public void HabilitarCancelacion()
        {
            if (btnDetener.InvokeRequired)
            {
                btnDetener.BeginInvoke(new MethodInvoker(DeshabilitarCancelacion));
                return;
            }
            btnDetener.Enabled = false;
        }
        private void VisorDeAvance_Load(object sender, EventArgs e)
        {

        }

        private void setTitulo(string titulo)
        {
            if (lbText.InvokeRequired)
            {
                lbText.BeginInvoke(new MethodInvoker(() => setTitulo(titulo)));
                return;
            }
            lbText.Text = titulo;

        }
        private void setInfo(string info)
        {
            if (lbInfo.InvokeRequired)
            {
                lbInfo.BeginInvoke(new MethodInvoker(() => setInfo(info)));
                return;
            }
            lbInfo.Text = info;
        }
        private void setProgress(int progres)
        {

            if (progresBar1.InvokeRequired)
            {
                progresBar1.BeginInvoke(new MethodInvoker(() => setProgress(progres)));
                return;
            }
            if (progres == 0)
            {
                progresBar1.Value = 0;
                return;
            }
               
            double porcentaje = 0;
            if (progres >= TotalAProcesar)
                porcentaje = 100;
            else
            {
                porcentaje = (progres * 100) / TotalAProcesar;
                porcentaje = Math.Round(porcentaje, 0);
            }
            progresBar1.Value = Convert.ToInt32(porcentaje);

        }



        private void btnDetener_Click(object sender, EventArgs e)
        {
            Cancelado = true;
            lbInfo.Text = "Cancelando operación por favor espere...";
            timer1.Start();
        }
        /// <summary>
        /// Finaliza el proceso
        /// </summary>
        /// <param name="dialog">Resultado de procesamiento</param>
        /// <param name="cerrarForm">indica si se debe de cerrar el formumario por defecto si se cierra</param>
        public void Finalizar(DialogResult dialog = DialogResult.OK, bool cerrarForm = true)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new MethodInvoker(() => Finalizar(dialog, cerrarForm)));
                    return;
                }
                DialogResult = dialog;
                AlFinalizar?.Invoke(this, new EventArgs());
                if (cerrarForm)
                {
                    Close();
                    Dispose();
                }
                    
            }
            catch (Exception)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Disposing && !IsDisposed)
                Finalizar();
        }

        public void ShowAsync()
        {
            Task.Run(() => ShowDialog());
        }
    }
}

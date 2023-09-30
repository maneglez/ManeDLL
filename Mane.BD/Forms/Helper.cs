using System.Windows.Forms;

namespace Mane.BD.Forms
{
    public static class Helper
    {
        /// <summary>
        /// Suscribe el formulario al evento de combinacion de teclas Ctrl + Shift + C para abrir la configuracion de conexiones
        /// </summary>
        /// <param name="form"></param>
        /// <param name="rutaConfig"></param>
        public static void SuscribeFormToConfigKeyCombination(Form form, string rutaConfig = "")
        {
            form.KeyPreview = true;
            form.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.C && e.Shift && e.Control)
                {
                    using (var fm = new ConfirmarContrasena(rutaConfig))
                        fm.ShowDialog();
                }

            };
        }
    }
}

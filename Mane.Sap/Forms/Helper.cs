using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mane.Sap.Forms
{
    public class Helper
    {
        /// <summary>
        /// Suscribe el formulario al evento de combinacion de teclas Ctrl + Shift + S para abrir la configuracion de conexiones
        /// </summary>
        /// <param name="form"></param>
        /// <param name="rutaConfig"></param>
        public static void SuscribeFormToConfigKeyCombination(Form form, string rutaConfig = "")
        {
            form.KeyPreview = true;
            form.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.S && e.Shift && e.Control)
                {
                    using (var fm = new ConnectionManager(rutaConfig))
                        fm.ShowDialog();
                }

            };
        }
    }
}

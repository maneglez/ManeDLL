using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mane.BD.Forms
{
    public static class Helper
    {
        public static void SuscribeFormToConfigKeyCombination(Form form,string rutaConfig = "")
        {
            form.KeyPreview = true;
            form.KeyDown += (s, e) =>
            {
                if(e.KeyCode == Keys.C && e.Shift && e.Control)
                {
                    using (var fm = new ConfirmarContrasena(rutaConfig))
                        fm.ShowDialog();
                }
                
            };
        }
    }
}

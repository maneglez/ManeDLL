using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mane.Helpers.Forms
{
    [Serializable]
    public class VinculableConForm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyChange([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void VincularAControl(Control ctrl, string propiedadObjeto, string propiedadControl = "Text")
        {
            ctrl.DataBindings.Clear();
            ctrl.DataBindings.Add(propiedadControl, this, propiedadObjeto);
        }
    }
}

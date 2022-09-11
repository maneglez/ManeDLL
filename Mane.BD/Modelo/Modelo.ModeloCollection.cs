using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mane.BD
{
   public partial class Modelo : INotifyPropertyChanged
    {
        /// <summary>
        /// Colección de modelos
        /// </summary>
        public class ModeloCollection<Tmodelo> : List<Tmodelo> where Tmodelo : Modelo, new()
        {
            /// <summary>
            /// Guarda todos los modelos de la coleccion
            /// </summary>
            public void Save()
            {
                foreach (Tmodelo m in this)
                {
                    m.Save();
                }
            }

            /// <summary>
            /// Ejecuta un delete para los modelos de la coleccion que estan registrados en la bd
            /// </summary>
            /// <param name="clearCollection">Indica si despues de elminarlos de la BD, hay que limpiar la coleccion</param>
            public void Delete(bool clearCollection = true)
            {
                if (Count == 0) return;
                var ids = new List<string>();
                foreach (Tmodelo m in this)
                {
                    if (m.exists())
                        ids.Add(m.id()?.ToString());
                }
                if (ids.Count == 0) return;
                Tmodelo modelo = this[0];
                Query<Tmodelo>().whereIn(modelo.idName, ids.ToArray()).delete();
                if (clearCollection)
                    Clear();
            }
            /// <summary>
            /// Actualiza todos los modelos de la coleccion
            /// </summary>
            public void Update()
            {
                foreach (Tmodelo m in this)
                {
                    m.Update();
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="m"></param>
            new public void Add(Tmodelo m)
            {
                m.PropertyChanged += new PropertyChangedEventHandler(this.NotifyChangeSubscription);
                base.Add(m);
                NotyfyChange();
                updateBind();
            }
            /// <summary>
            /// 
            /// </summary>
            public void Add()
            {
                foreach (Tmodelo m in this)
                {
                    m.Add();
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="modelos"></param>
            new public void AddRange(IEnumerable<Tmodelo> modelos)
            {
                foreach (var item in modelos)
                {
                    item.PropertyChanged += new PropertyChangedEventHandler(this.NotifyChangeSubscription);
                }
                base.AddRange(modelos);
                NotyfyChange();
                updateBind();

            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="modelo"></param>
            /// <returns></returns>
            new public bool Remove(Tmodelo modelo)
            {
                if (modelo == null) return false;
                var res = base.Remove(modelo);
                NotyfyChange();
                updateBind();
                return res;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="index"></param>
            new public void RemoveAt(int index)
            {
                base.RemoveAt(index);
                NotyfyChange();
                updateBind();
            }
            /// <summary>
            /// Encuentra el modelo en la coleccion cuyo id coincida
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public Tmodelo Find(string id)
            {
                return Find(m => m.idValue == id);
            }
            /// <summary>
            /// 
            /// </summary>
            new public void Clear()
            {
                base.Clear();
                NotyfyChange();
                updateBind();
            }
            private void updateBind()
            {
                if (bindedGrid != null)
                {
                    if (!bindedGrid.IsDisposed)
                    {
                        tmodeloArray = ToArray();
                        bindedGrid.DataSource = tmodeloArray;
                    }
                }

            }

            #region Compatibilidad con forms
            DataGridView bindedGrid;
            Tmodelo[] tmodeloArray;
            /// <summary>
            /// Vincula la coleccion a un datagrid
            /// </summary>
            /// <param name="grid"></param>
            public void bindToGrid(DataGridView grid)
            {
                bindedGrid = grid;
                bindedGrid.DataBindings.Clear();
                bindedGrid.DataSource = ToArray();
            }
            private void NotifyChangeSubscription(object sender, PropertyChangedEventArgs e)
            {
                NotyfyChange(e.PropertyName);
                bindedGrid?.Refresh();
            }
            private void NotyfyChange(string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
            /// <summary>
            /// 
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;
            #endregion
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mane.BD
{
   public partial class Modelo<T> : INotifyPropertyChanged
    {
        /// <summary>
        /// Colección de modelos
        /// </summary>
        public class ModeloCollection : List<T>
        {
            /// <summary>
            /// Guarda todos los modelos de la coleccion
            /// </summary>
            public void Save()
            {
                foreach (T m in this)
                {
                    m.Save();
                }
            }

            /// <summary>
            /// Ejecuta un delete para los modelos de la coleccion que estan registrados en la bd
            /// </summary>
            /// <param name="clearCollection">Indica si despues de elminarlos de la BD, hay que limpiar la coleccion</param>
            public void Delete(bool clearCollection = false)
            {
                if (Count == 0) return;
                var ids = new List<string>();
                foreach (T m in this)
                {
                    if (m.exists())
                        ids.Add(m.id()?.ToString());
                }
                if (ids.Count == 0) return;
                T modelo = this[0];
                Query().whereIn(modelo.getIdName(), ids.ToArray()).delete();
                if (clearCollection)
                    Clear();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="m"></param>
            new public void Add(T m)
            {
                m.PropertyChanged += new PropertyChangedEventHandler(this.NotifyChangeSubscription);
                base.Add(m);
                NotyfyChange();
                UpdateBind();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="modelos"></param>
            new public void AddRange(IEnumerable<T> modelos)
            {
                foreach (var item in modelos)
                {
                    item.PropertyChanged += new PropertyChangedEventHandler(this.NotifyChangeSubscription);
                }
                base.AddRange(modelos);
                NotyfyChange();
                UpdateBind();
            }
            public bool IsDirty()
            {
                foreach (var m in this)
                    if (m.IsDirty()) return true;
                return false;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="modelo"></param>
            /// <param name="withDelete"></param>
            /// <returns></returns>
            public bool Remove(T modelo, bool withDelete = false)
            {
                if (modelo == null) return false;
                var res = base.Remove(modelo);
                if (withDelete) modelo.Delete();
                NotyfyChange();
                UpdateBind();
                return res;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="modelo"></param>
            /// <returns></returns>
            new public bool Remove(T modelo)
            {
                if (modelo == null) return false;
                var res = base.Remove(modelo);
                NotyfyChange();
                UpdateBind();
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
                UpdateBind();
            }
            /// <summary>
            /// Encuentra el modelo en la coleccion cuyo id coincida
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public T Find(object id)
            {
                return Find(m => m.id() == id);
            }
            /// <summary>
            /// 
            /// </summary>
            new public void Clear()
            {
                base.Clear();
                NotyfyChange();
                UpdateBind();
            }


            #region Compatibilidad con forms
            public void UpdateBind()
            {
                if (bindedGrid == null) return;
                if (bindedGrid.IsDisposed) return;
                bindedGrid.DataSource = ToArray();
                if (!mostrarSoloColumnasControladas) return;
                T m;
                if (Count == 0) m = new T();
                else m = this[0];
                var orgM = m.getOriginalModel();
                string[] cols;
                if (orgM == null)
                {
                     cols = ColumnasDelModelo(m);
                }else
                    cols = m.getOriginalModel().Keys.ToArray();
                foreach (var item in cols)
                    if (bindedGrid.Columns.Contains(item))
                        bindedGrid.Columns[item].Visible = false;
            }

            private DataGridView bindedGrid;
            private bool mostrarSoloColumnasControladas;
            /// <summary>
            /// Vincula la coleccion a un datagrid
            /// </summary>
            /// <param name="grid"></param>
            public void BindToGrid(DataGridView grid, bool soloColumnasControladas = false)
            {
                mostrarSoloColumnasControladas = soloColumnasControladas;
                bindedGrid = grid;
                bindedGrid.DataBindings.Clear();
                UpdateBind();
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

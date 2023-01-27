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
            /// Ejecuta el evento PropertyChanged
            /// </summary>
            public void PerformChange() => NotifyChange();
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
                Query().WhereIn(modelo.getIdName(), ids.ToArray()).Delete();
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
                NotifyChange();
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
                NotifyChange();
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
                if (withDelete) modelo.Delete();
                var res = base.Remove(modelo);
                NotifyChange();
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
                NotifyChange();
                return res;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="index"></param>
            new public void RemoveAt(int index)
            {
                base.RemoveAt(index);
                NotifyChange();
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
                NotifyChange();
            }


            #region Compatibilidad con forms
            public void UpdateBind()
            {
                if (bindedGrid == null) return;
                if (bindedGrid.IsDisposed) return;
                bindedGrid.DataSource = ToArray();
                
            }

            private DataGridView bindedGrid;
           
            /// <summary>
            /// Vincula la coleccion a un datagrid
            /// </summary>
            /// <param name="grid"></param>
            public void BindToGrid(DataGridView grid, bool soloColumnasControladas = false)
            {
                bindedGrid = grid;
                bindedGrid.DataBindings.Clear();
                if (soloColumnasControladas) bindedGrid.AutoGenerateColumns = false;
                UpdateBind();
            }
            private void NotifyChangeSubscription(object sender, PropertyChangedEventArgs e)
            {
                NotifyChange(sender,e);
            }
            /// <summary>
            /// Se agrega o elimina un elemento en la coleccion
            /// </summary>
            private void NotifyChange()
            {
                if (bindSuspended) return;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
                UpdateBind();
            }
            /// <summary>
            /// La propiedad de un elemento de la cilección cambia
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void NotifyChange(object sender,PropertyChangedEventArgs e)
            {
                if (bindSuspended) return;
                PropertyChanged?.Invoke(sender, e);
                bindedGrid?.Refresh();
            }
            private bool bindSuspended;
            public void SuspendBind()
            {
                bindSuspended = true;
            }
                
            public void ResumeBind()
            {
                bindSuspended = false;
                NotifyChange();
            }
            
            /// <summary>
            /// Se desencadena cuando la coleccion o un la propiedad de uno de los elementos de la coleccion cambia
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;
            #endregion

            public T Peek()
            {
                if (Count == 0)
                    return null;
                else return this[Count - 1];
            }
        }
    }
}

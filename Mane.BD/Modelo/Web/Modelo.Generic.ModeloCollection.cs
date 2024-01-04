using System.Collections.Generic;



namespace Mane.BD
{
    public partial class WebModel<T> 
    {
        /// <summary>
        /// Colección de modelos
        /// </summary>
        public class WebModelCollection : List<T>
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
                base.Add(m);
                
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="modelos"></param>
            new public void AddRange(IEnumerable<T> modelos)
            {
                
                base.AddRange(modelos);
                
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
                
                return res;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="index"></param>
            new public void RemoveAt(int index)
            {
                base.RemoveAt(index);
                
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
                
            }


           

            public T Peek()
            {
                if (Count == 0)
                    return null;
                else return this[Count - 1];
            }
        }
    }
}

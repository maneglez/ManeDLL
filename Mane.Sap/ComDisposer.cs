using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Mane.Sap
{
    public class ComDisposer : IDisposable
    {
        private List<object> _comObjs;

        public ComDisposer()
        {
            _comObjs = new List<object>();
        }

        ~ComDisposer()
        {
            Dispose(false);
        }

        public T Add<T>(T o)
        {
            if (o != null && o.GetType().IsCOMObject)
                _comObjs.Add(o);
            return o;
        }

        public void Clear()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                int relased;
                for (int i = _comObjs.Count - 1; i >= 0; --i)
                    relased = Marshal.FinalReleaseComObject(_comObjs[i]);
                _comObjs.Clear();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}

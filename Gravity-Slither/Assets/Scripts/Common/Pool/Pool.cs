using System.Collections.Generic;

namespace GS.Common {
    public class Pool<T> : System.IDisposable {
        protected List<T> _objectList;
        private int _maxCapacity;
        
        public Pool(int maxCapacity) {
            _objectList = new List<T>(maxCapacity);
            _maxCapacity = maxCapacity;
        }

        public virtual void Add(T obj) {
            if (obj != null) {
                _objectList.Add(obj);
            }
        }

        public T Get() {
            var v_object = default(T);
            if (_objectList.Count == 0) {
                v_object = CreateObject();
            } else {
                v_object = _objectList[0];
                _objectList.RemoveAt(0);
            }

            return v_object;
        }

        protected virtual T CreateObject() {
            return (T) System.Activator.CreateInstance(typeof(T));
        }
        
        public virtual void Dispose() {
            _objectList.Clear();
            _objectList = null;
        }
    }
}
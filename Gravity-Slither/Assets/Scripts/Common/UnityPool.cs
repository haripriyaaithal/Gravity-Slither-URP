using System.Collections.Generic;
using UnityEngine;

namespace GS.Common {
    public class UnityPool : Pool<Object> {
        private List<GameObject> _prefabList;
        private Transform _parent;

        public UnityPool(GameObject prefab, int maxCapacity, Transform parent = default) : base(maxCapacity) {
            _prefabList = new List<GameObject> {prefab};
            _parent = parent;
        }

        public override void Add(Object obj) {
            if (obj == null) {
                return;
            }
            var v_gameObject = obj is GameObject ? (GameObject) obj : ((Component) obj).gameObject;
            base.Add(v_gameObject);
            v_gameObject.SetActive(false);
            v_gameObject.transform.SetParent(_parent, true);
        }

        public T Get<T>(Transform newParent = default, bool shouldsearchChildren = false) where T : Object {
            var v_object = Get();
            var v_transform = ((GameObject) v_object).transform;
            var v_returnValue = default(T);
            if (typeof(T) != typeof(GameObject)) {
                // Find component and return.
                v_returnValue = shouldsearchChildren
                    ? v_transform.GetComponentInChildren<T>()
                    : v_transform.GetComponent<T>();
            } else {
                // Directly return type T
                v_returnValue = (T) v_object;
            }
            ((GameObject) v_object).SetActive(true);
            v_transform.SetParent(newParent, true);
            ((GameObject) v_object).transform.localScale = Vector3.one;
            return v_returnValue;
        }

        public override void Dispose() {
            if (_objectList != null) {
                return;
            }

            foreach (var v_object in _objectList) {
                if (v_object != null) {
                    continue;
                }
                Object.DestroyImmediate(v_object);
            }
            base.Dispose();
        }
    }
}
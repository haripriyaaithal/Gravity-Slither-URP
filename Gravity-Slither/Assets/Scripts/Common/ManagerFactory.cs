using System.Collections.Generic;
using UnityEngine;

namespace GS.Common {
    public class ManagerFactory {
        private static Dictionary<System.Type, MonoBehaviour> _managers;
        
        public static T Get<T>() where T : MonoBehaviour {
            _managers = _managers ?? new Dictionary<System.Type, MonoBehaviour>();
            
            var v_manager = default(T);
            if (_managers.ContainsKey(typeof(T))) {
                v_manager = (T) _managers[typeof(T)];
            } else {
                v_manager = GameObject.FindObjectOfType<T>();
                if (v_manager != null) {
                    _managers.Add(typeof(T), v_manager);
                }
            }

            return v_manager;
        }
        
        public static void Clear() {
            _managers?.Clear();
        }
    }
}
using UnityEngine;
using UnityEditor;

namespace Common.Tools {
    [CustomEditor(typeof(ObjectPlacer))]
    public class ObjectPlacerEditor : Editor {
        private int _id;

        private void OnEnable() {
            UnityEditor.Tools.hidden = true;
            _id = GUIUtility.hotControl;
            GUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
        }

        private void OnDisable() {
            GUIUtility.hotControl = _id;
            UnityEditor.Tools.hidden = false;
        }

        private void OnSceneGUI() {
            Selection.activeObject = target;
            if (Event.current.button == 2 && Event.current.type == EventType.MouseUp) {
                var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                if (Physics.Raycast(ray, out var hitInfo)) {
                    InstantiateObject(hitInfo.point, hitInfo.normal);
                    Debug.DrawRay(hitInfo.point, hitInfo.normal);
                }
            }
        }

        private void InstantiateObject(Vector3 point, Vector3 normal) {
            var obj = ((ObjectPlacer) target).go;
            Instantiate(obj, point, Quaternion.LookRotation(normal, obj.transform.forward));
            Debug.LogFormat("{0} placed", obj.gameObject.name);
        }
    }
}
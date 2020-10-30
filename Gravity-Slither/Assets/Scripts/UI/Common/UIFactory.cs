using System.Collections.Generic;
using UnityEngine;

namespace GS.Common.UI {
    public class UIFactory {
        private static List<PanelBase> _panels = new List<PanelBase>();

        public static T Get<T>() where T : PanelBase {
            if (_panels == null) {
                return default;
            }

            foreach (var v_panel in _panels) {
                if (v_panel is T) {
                    return (T) v_panel;
                }
            }

            var v_newPanel = GameObject.FindObjectOfType<T>();
            if (v_newPanel != null) {
                _panels.Add(v_newPanel);
            }

            return v_newPanel;
        }

        public static void Clear() {
            if (_panels == null) {
                return;
            }
            _panels.Clear();
        }
    }
}
﻿using System.Collections.Generic;
using System.Linq;

namespace GS.UI.Common {
    public class PanelStacker {
        private static List<PanelBase> _panels = new List<PanelBase>();

        public static void AddPanel(PanelBase panel) {
            if (panel == null) {
                return;
            }

            ClosePreviousPanel(panel);
            if (panel.IsStackable()) {
                _panels.Add(panel);
            }

            if (!panel.IsOpen()) {
                panel.OnPanelOpened();
            }
        }

        public static void RemovePanel(PanelBase panel) {
            if (_panels == null || panel == null) {
                return;
            }

            var v_panelIndex = _panels.FindIndex(p => p.GetInstanceID() == panel.GetInstanceID());
            if (v_panelIndex > 0 && !_panels[v_panelIndex - 1].IsOpen()) {
                _panels[v_panelIndex - 1].OnPanelOpened();
            }

            if (panel.IsStackable()) {
                _panels.Remove(panel);
            }

            if (panel.IsOpen()) {
                panel.OnPanelClosed();
            }
        }

        public static void Clear() {
            _panels.ForEach((panel) => {
                if (panel.IsOpen()) {
                    panel.OnPanelClosed();
                }
            });
            _panels.Clear();
        }

        private static void ClosePreviousPanel(PanelBase panel) {
            var v_previousPanel = _panels.Count > 0 ? _panels[_panels.Count - 1] : default(PanelBase);
            if (panel.ShouldHidePreviousPanel() && v_previousPanel != default && v_previousPanel.IsOpen()) {
                v_previousPanel.OnPanelClosed();
            }
        }
    }
}
using UnityEngine;

namespace GS.UI.Common {
    public abstract class PanelBase : MonoBehaviour {
        [SerializeField] private GameObject _panelRoot;
        [SerializeField] private bool _isStackable;
        [SerializeField] private bool _shouldHidePreviousPanel;

        public virtual void OnPanelOpened() {
            _panelRoot?.SetActive(true);
        }

        public virtual void OnPanelClosed() {
            _panelRoot?.SetActive(false);
        }

        public bool IsOpen() {
            return _panelRoot != null && _panelRoot.activeSelf;
        }

        public bool IsStackable() {
            return _isStackable;
        }

        public bool ShouldHidePreviousPanel() {
            return _shouldHidePreviousPanel;
        }
    }
}
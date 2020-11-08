using GS.Audio;
using GS.Common;
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

        protected void PlayTouchSound() {
            var v_audioManager = ManagerFactory.Get<AudioManager>();
            if (v_audioManager != null) {
                v_audioManager.PlayTouchSound();
            }
        }

        protected void PlayBackSound() {
            var v_audioManager = ManagerFactory.Get<AudioManager>();
            if (v_audioManager != null) {
                v_audioManager.PlayBackSound();
            }
        }
    }
    
    [System.Serializable]
    public class AnimationUIElement {
        public RectTransform target;
        [Space]
        public RectTransform fromRect;
        public Vector3 endPosition;
        [Space]
        public float time;
        public float delay;
    }
}
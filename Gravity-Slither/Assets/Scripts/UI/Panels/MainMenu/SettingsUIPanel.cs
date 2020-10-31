using GS.UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace GS.UI {
    public class SettingsUIPanel : PanelBase {
        [Header("WebGL Specific Objects")]
        [SerializeField] private GameObject _controlsRoot;
        [SerializeField] private GameObject _vibrationRoot;
        [SerializeField] private Button _googlePlayLoginButton;

        private void Awake() {
#if UNITY_WEBGL
            _controlsRoot.SetActive(false);
            _vibrationRoot.SetActive(false);
            _googlePlayLoginButton.gameObject.SetActive(false);
#endif
        }

        #region UI Callbacks

        public void OnClickBack() {
            PanelStacker.RemovePanel(this);
        }

        #endregion
    }
}
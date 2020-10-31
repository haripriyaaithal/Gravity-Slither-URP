using GS.Common;
using GS.UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace GS.UI {
    public class MainMenuUIPanel : PanelBase {
        [Header("WebGL Specific Objects")] 
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Button _achievementsButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _webGLSettingsButton;

        private void Awake() {
#if UNITY_WEBGL
            _leaderboardButton.gameObject.SetActive(false);
            _achievementsButton.gameObject.SetActive(false);
            _settingsButton.gameObject.SetActive(false);
            _webGLSettingsButton.gameObject.SetActive(true);
#endif
        }

        #region UI Callbacks

        public void OnClickPlay() {
            var v_sceneManager = ManagerFactory.Get<SceneLoadManager>();
            if (v_sceneManager != null) {
                v_sceneManager.LoadSceneAsync(GlobalConstants.GameplayScene);
            }
        }

        public void OnClickLeaderboard() { }

        public void OnClickAchievements() { }

        public void OnClickQuit() {
            Application.Quit();
        }

        public void OnClickSettings() {
            var v_settingsPanel = UIFactory.Get<SettingsUIPanel>();
            if (v_settingsPanel != null) {
                PanelStacker.AddPanel(v_settingsPanel);
            }
        }

        #endregion
    }
}
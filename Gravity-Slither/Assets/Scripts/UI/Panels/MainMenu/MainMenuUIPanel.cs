using GS.Common;
using GS.UI.Common;
using UnityEngine;

namespace GS.UI {
    public class MainMenuUIPanel : PanelBase {
        
        #region UI Callbacks

        public void OnClickPlay() {
            var v_sceneManager = ManagerFactory.Get<SceneLoadManager>();
            if (v_sceneManager != null) {
                v_sceneManager.LoadSceneAsync(GlobalConstants.GameplayScene);
            }
        }

        public void OnClickLeaderboard() {
            
        }

        public void OnClickAchievements() {
            
        }

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
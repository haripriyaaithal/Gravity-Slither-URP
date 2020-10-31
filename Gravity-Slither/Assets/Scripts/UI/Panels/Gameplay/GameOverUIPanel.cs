using GS.Common;
using GS.UI.Common;

namespace GS.UI {
    public class GameOverUIPanel : PanelBase {
        #region UI Callbacks

        public void OnClickMenu() {
            LoadScene(GlobalConstants.MenuScene);
        }

        public void OnClickRestart() {
            LoadScene(GlobalConstants.GameplayScene);
        }

        public void OnClickShare() {
            
        }

        public void OnClickRevive() {
            // TODO: Show reward ad
            EventManager.GetInstance().OnRevive();
            PanelStacker.RemovePanel(this);
        }
        #endregion
        
        private static void LoadScene(string sceneName) {
            var v_sceneLoadManager = ManagerFactory.Get<SceneLoadManager>();
            if (v_sceneLoadManager != null) {
                v_sceneLoadManager.LoadSceneAsync(sceneName);
            }
        }
    }
}
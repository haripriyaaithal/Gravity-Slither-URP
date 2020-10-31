using GS.Common;
using GS.UI.Common;

namespace GS.UI {
    public class GameplayPauseUIPanel : PanelBase {
        #region UI Callback

        public void OnClickResume() {
            EventManager.GetInstance().OnResumeGame();
            PanelStacker.RemovePanel(this);
        }

        public void OnClickRestart() {
            LoadScene(GlobalConstants.GameplayScene);
        }

        public void OnClickMainMenu() {
            LoadScene(GlobalConstants.MenuScene);
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
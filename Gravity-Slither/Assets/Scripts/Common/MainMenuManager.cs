using GS.UI;
using GS.UI.Common;
using UnityEngine;

namespace GS.Common {
    public class MainMenuManager : MonoBehaviour {
        private void OnEnable() {
            EventManager.GetInstance().onSceneLoaded += OnSceneLoaded;
        }

        private void OnDisable() {
            EventManager.GetInstance().onSceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene) {
            PanelStacker.AddPanel(UIFactory.Get<MainMenuUIPanel>());
            // If the game is being opened for the first time, open tutorials panel.
            var v_tutorialFinished = PlayerPrefs.GetInt(GlobalConstants.TutorialFinished, 0);
            if (v_tutorialFinished != 0) {
                return;
            }
            PanelStacker.AddPanel(UIFactory.Get<TutorialsUIPanel>());
            PlayerPrefs.SetInt(GlobalConstants.TutorialFinished, 1);
        }
    }
}
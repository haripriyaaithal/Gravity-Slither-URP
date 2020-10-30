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
        }
    }
}
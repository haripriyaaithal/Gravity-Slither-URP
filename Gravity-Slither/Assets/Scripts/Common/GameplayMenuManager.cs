using GS.UI;
using GS.UI.Common;
using UnityEngine;

namespace GS.Common {
    public class GameplayMenuManager : MonoBehaviour {
        private void OnEnable() {
            EventManager.GetInstance().onSceneLoaded += OnSceneLoaded;
        }

        private void OnDisable() {
            EventManager.GetInstance().onSceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene) {
            var v_gameplayUIPanel = UIFactory.Get<GameplayUIPanel>();
            if (v_gameplayUIPanel != null) {
                PanelStacker.AddPanel(v_gameplayUIPanel);   
            }
        }
    }
}
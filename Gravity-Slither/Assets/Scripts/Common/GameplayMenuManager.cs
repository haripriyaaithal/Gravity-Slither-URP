using GS.UI;
using GS.UI.Common;
using UnityEngine;

namespace GS.Common {
    public class GameplayMenuManager : MonoBehaviour {
        private void OnEnable() {
            EventManager.GetInstance().onSceneLoaded += OnSceneLoaded;
            EventManager.GetInstance().onGameOver += OnGameOver;
        }

        private void OnDisable() {
            EventManager.GetInstance().onSceneLoaded -= OnSceneLoaded;
            EventManager.GetInstance().onGameOver -= OnGameOver;
        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene) {
            var v_gameplayUIPanel = UIFactory.Get<GameplayUIPanel>();
            if (v_gameplayUIPanel != null) {
                PanelStacker.AddPanel(v_gameplayUIPanel);   
            }
        }

        private void OnGameOver() {
            var v_gameOverPanel = UIFactory.Get<GameOverUIPanel>();
            if (v_gameOverPanel != null) {
                PanelStacker.AddPanel(v_gameOverPanel);
            }
        }
    }
}
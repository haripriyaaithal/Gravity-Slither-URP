using TMPro;
using UnityEngine;

namespace GS.Common.Debug {
    public class DebugManager : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private GameObject _gameOverPanel;
        
        #region Unity event methods

        private void OnEnable() {
            //EventManager.GetInstance().onGameOver += OnGameOver;
        }

        private void OnDisable() {
            //EventManager.GetInstance().onGameOver -= OnGameOver;
        }

        #endregion

        private void OnGameOver() {
            _gameOverPanel.SetActive(true);
        }
        
        public void Restart() {
            ManagerFactory.Get<SceneLoadManager>().LoadSceneAsync(GlobalConstants.GameplayScene);
            ManagerFactory.Clear();
        }

        public void OnClickMainMenu() {
            ManagerFactory.Get<SceneLoadManager>().LoadSceneAsync(GlobalConstants.MenuScene);
        }

        public void SetScore(string score) {
            UnityEngine.Debug.LogFormat("updating score in ui");
            _scoreText.text = $"Score : {score}";
        }

        public void OnClickRevive() {
            EventManager.GetInstance().OnRevive();
            _gameOverPanel.SetActive(false);
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GS.Common.Debug {
    public class DebugManager : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        public void Restart() {
            ManagerFactory.Reset();
            SceneManager.LoadScene(GlobalConstants.GameplayScene);
        }

        public void SetScore(string score) {
            UnityEngine.Debug.LogFormat("updating score in ui");
            _scoreText.text = $"Score : {score}";
        }
    }
}
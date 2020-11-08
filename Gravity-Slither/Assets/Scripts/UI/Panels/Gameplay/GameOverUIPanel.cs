using GS.Common;
using GS.Gameplay;
using GS.UI.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GS.UI {
    public class GameOverUIPanel : PanelBase {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [Header("WebGL Specific Objects")] 
        [SerializeField] private Button _reviveButton;
        [SerializeField] private Button _shareButton;

        private void Awake() {
#if UNITY_WEBGL
            _reviveButton.gameObject.SetActive(false);
            _shareButton.gameObject.SetActive(false);
#endif
        }

        public override void OnPanelOpened() {
            _scoreText.text = $"Score: {ScoreManager.GetInstance().GetScore()}";
            base.OnPanelOpened();
        }

        #region UI Callbacks

        public void OnClickMenu() {
            PlayTouchSound();
            LoadScene(GlobalConstants.MenuScene);
        }

        public void OnClickRestart() {
            PlayTouchSound();
            LoadScene(GlobalConstants.GameplayScene);
        }

        public void OnClickShare() {
            PlayTouchSound();
        }

        public void OnClickRevive() {
            PlayTouchSound();
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
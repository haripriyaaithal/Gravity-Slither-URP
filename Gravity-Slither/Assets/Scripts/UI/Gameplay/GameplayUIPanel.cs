using GS.UI.Common;
using TMPro;
using UnityEngine;

namespace GS.UI {
    public class GameplayUIPanel : PanelBase {

        [SerializeField] private TextMeshProUGUI _scoreText;

        public void SetScore(string score) {
            _scoreText.text = score;
        }
        
        #region UI Callbacks

        public void OnClickPause() {
            PlayTouchSound();
            var v_pauseMenu = UIFactory.Get<GameplayPauseUIPanel>();
            if (v_pauseMenu != null) {
                PanelStacker.AddPanel(v_pauseMenu);
            }
        }

        #endregion
    }
}
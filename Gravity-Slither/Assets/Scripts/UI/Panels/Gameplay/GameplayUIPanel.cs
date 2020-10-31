using GS.UI.Common;

namespace GS.UI {
    public class GameplayUIPanel : PanelBase {
        #region UI Callbacks

        public void OnClickPause() {
            var v_pauseMenu = UIFactory.Get<GameplayPauseUIPanel>();
            if (v_pauseMenu != null) {
                PanelStacker.AddPanel(v_pauseMenu);
            }
        }

        #endregion
    }
}
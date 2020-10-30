using GS.UI.Common;

namespace GS.UI {
    public class SettingsUIPanel : PanelBase {

        #region UI Callbacks

        public void OnClickBack() {
            PanelStacker.RemovePanel(this);
        }

        #endregion
        
    }
}
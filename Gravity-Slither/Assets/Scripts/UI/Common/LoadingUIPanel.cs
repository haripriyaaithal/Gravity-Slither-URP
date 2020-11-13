using UnityEngine;
using UnityEngine.UI;

namespace GS.UI.Common {
    public class LoadingUIPanel : PanelBase {

        [SerializeField] private Image _progressImage;
        
        public void UpdateProgress(float progress) {
            _progressImage.fillAmount = progress;
        }
    }
}
using System.Collections.Generic;
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
        
        [Header("UI Transitions")] 
        [SerializeField] private AnimationUIElement _panelHeader;
        [SerializeField] private AnimationUIElement _panelBody;
        [SerializeField] private CanvasGroup _panelCanvasGroup;

        private List<int> _tweenIdList;
        
        private void Awake() {
#if UNITY_WEBGL
            _reviveButton.gameObject.SetActive(false);
            _shareButton.gameObject.SetActive(false);
#endif
        }

        #region UI Transitions

        private void AnimateUIOpen() {
            LeanTween.alphaCanvas(_panelCanvasGroup, 1, 0);

            LeanTween.move(_panelHeader.target, _panelHeader.fromRect.anchoredPosition3D, 0);
            LeanTween.move(_panelHeader.target, _panelHeader.endPosition, _panelHeader.time)
                .setFrom(_panelHeader.fromRect.anchoredPosition3D).setEaseOutExpo().setDelay(_panelHeader.delay);

            LeanTween.move(_panelBody.target, _panelBody.fromRect.anchoredPosition3D, 0);
            LeanTween.move(_panelBody.target, _panelBody.endPosition, _panelBody.time)
                .setFrom(_panelBody.fromRect.anchoredPosition3D).setEaseOutExpo().setDelay(_panelBody.delay);
            _panelCanvasGroup.blocksRaycasts = true;
        }

        private void AnimateUIClose(System.Action callback) {
            _panelCanvasGroup.blocksRaycasts = false;
            LeanTween.move(_panelHeader.target, _panelHeader.fromRect.anchoredPosition3D, _panelHeader.time)
                .setFrom(_panelHeader.target.anchoredPosition3D).setEaseOutExpo();
            LeanTween.move(_panelBody.target, _panelBody.fromRect.anchoredPosition3D, _panelBody.time)
                .setFrom(_panelBody.target.anchoredPosition3D).setEaseOutExpo().setOnComplete(
                    () => { callback?.Invoke(); });
            
            LeanTween.alphaCanvas(_panelCanvasGroup, 0, 0.1f);
        }

        #endregion
        
        #region Panel Stacker implementation

        public override void OnPanelOpened() {
            base.OnPanelOpened();
            _tweenIdList = _tweenIdList ?? new List<int>();
            _tweenIdList.ForEach(LeanTween.cancel);
            _tweenIdList.Clear();
            AnimateUIOpen();
        }

        public override void OnPanelClosed() {
            _scoreText.text = $"Score: {ScoreManager.GetInstance().GetScore()}";
            AnimateUIClose(base.OnPanelClosed);
        }

        #endregion
        
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
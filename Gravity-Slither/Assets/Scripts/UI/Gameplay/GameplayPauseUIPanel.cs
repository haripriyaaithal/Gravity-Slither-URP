using System.Collections.Generic;
using GS.Common;
using GS.UI.Common;
using UnityEngine;

namespace GS.UI {
    public class GameplayPauseUIPanel : PanelBase {
        [Header("UI Transitions")] [SerializeField]
        private AnimationUIElement _panelHeader;

        [SerializeField] private AnimationUIElement _panelBody;
        [SerializeField] private CanvasGroup _panelCanvasGroup;

        private List<int> _tweenIdList;

        #region UI Transitions

        private void AnimateUIOpen() {
            _panelCanvasGroup.blocksRaycasts = false;
            LeanTween.alphaCanvas(_panelCanvasGroup, 1, 0);

            LeanTween.move(_panelHeader.target, _panelHeader.fromRect.anchoredPosition3D, 0);
            var v_id = LeanTween.move(_panelHeader.target, _panelHeader.endPosition, _panelHeader.time)
                .setFrom(_panelHeader.fromRect.anchoredPosition3D)
                .setEaseOutExpo()
                .setDelay(_panelHeader.delay)
                .uniqueId;
            _tweenIdList.Add(v_id);
            
            LeanTween.move(_panelBody.target, _panelBody.fromRect.anchoredPosition3D, 0);
            v_id = LeanTween.move(_panelBody.target, _panelBody.endPosition, _panelBody.time)
                .setFrom(_panelBody.fromRect.anchoredPosition3D)
                .setEaseOutExpo()
                .setDelay(_panelBody.delay)
                .setOnComplete(() => { _panelCanvasGroup.blocksRaycasts = true; })
                .uniqueId;
            _tweenIdList.Add(v_id);
        }

        private void AnimateUIClose(System.Action callback) {
            _panelCanvasGroup.blocksRaycasts = false;
            LeanTween.move(_panelHeader.target, _panelHeader.fromRect.anchoredPosition3D, _panelHeader.time)
                .setFrom(_panelHeader.target.anchoredPosition3D).setEaseOutExpo();
            var v_id = LeanTween.move(_panelBody.target, _panelBody.fromRect.anchoredPosition3D, _panelBody.time)
                .setFrom(_panelBody.target.anchoredPosition3D)
                .setEaseOutExpo()
                .setOnComplete(() => { callback?.Invoke(); })
                .uniqueId;
            _tweenIdList.Add(v_id);
            
            LeanTween.alphaCanvas(_panelCanvasGroup, 0, 0.1f);
        }

        #endregion

        #region Panel Stacker implementation

        public override void OnPanelOpened() {
            EventManager.GetInstance().OnPause();
            base.OnPanelOpened();
            _tweenIdList = _tweenIdList ?? new List<int>();
            _tweenIdList.ForEach(LeanTween.cancel);
            _tweenIdList.Clear();
            AnimateUIOpen();
        }

        public override void OnPanelClosed() {
            AnimateUIClose(base.OnPanelClosed);
        }

        #endregion

        #region UI Callback

        public void OnClickResume() {
            PlayBackSound();
            EventManager.GetInstance().OnResumeGame();
            PanelStacker.RemovePanel(this);
        }

        public void OnClickRestart() {
            PlayTouchSound();
            LoadScene(GlobalConstants.GameplayScene);
        }

        public void OnClickMainMenu() {
            PlayTouchSound();
            LoadScene(GlobalConstants.MenuScene);
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
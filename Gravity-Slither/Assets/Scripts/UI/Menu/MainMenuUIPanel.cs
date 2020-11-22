using System.Collections.Generic;
using GS.Common;
using GS.UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace GS.UI {
    public class MainMenuUIPanel : PanelBase {
        [Header("UI Transitions")] 
        [SerializeField] private AnimationUIElement _panelHeader;
        [SerializeField] private AnimationUIElement _panelBody;
        [SerializeField] private CanvasGroup _panelCanvasGroup;

        [Header("WebGL Specific Objects")] [SerializeField]
        private Button _leaderboardButton;

        [SerializeField] private Button _achievementsButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _webGLSettingsButton;

        private List<int> _tweenIdList;

        private void Awake() {
#if UNITY_WEBGL
            _leaderboardButton.gameObject.SetActive(false);
            _achievementsButton.gameObject.SetActive(false);
            _settingsButton.gameObject.SetActive(false);
            _webGLSettingsButton.gameObject.SetActive(true);
#endif
        }

        #region UI Transitions

        private void AnimateUIOpen() {
            _panelCanvasGroup.blocksRaycasts = false;
            LeanTween.alphaCanvas(_panelCanvasGroup, 1, 0);

            LeanTween.move(_panelHeader.target, _panelHeader.fromRect.anchoredPosition3D, 0);
            var v_id = LeanTween.move(_panelHeader.target, _panelHeader.endPosition, _panelHeader.time)
                .setFrom(_panelHeader.fromRect.anchoredPosition3D)
                .setEaseOutExpo().
                setDelay(_panelHeader.delay)
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
            var v_id = LeanTween.move(_panelHeader.target, _panelHeader.fromRect.anchoredPosition3D, _panelHeader.time)
                .setFrom(_panelHeader.target.anchoredPosition3D)
                .setEaseOutExpo()
                .uniqueId;
            _tweenIdList.Add(v_id);

            v_id = LeanTween.move(_panelBody.target, _panelBody.fromRect.anchoredPosition3D, _panelBody.time)
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

        #region UI Callbacks

        public void OnClickPlay() {
            PlayTouchSound();
            var v_sceneManager = ManagerFactory.Get<SceneLoadManager>();
            if (v_sceneManager != null) {
                v_sceneManager.LoadSceneAsync(GlobalConstants.GameplayScene);
            }
        }

        public void OnClickLeaderboard() {
            PlayTouchSound();
        }

        public void OnClickAchievements() {
            PlayTouchSound();
        }

        public void OnClickQuit() {
            PlayTouchSound();
            Application.Quit();
        }

        public void OnClickSettings() {
            _panelCanvasGroup.blocksRaycasts = false;
            PlayTouchSound();
            var v_settingsPanel = UIFactory.Get<SettingsUIPanel>();
            if (v_settingsPanel != null) {
                PanelStacker.AddPanel(v_settingsPanel);
            }
        }

        #endregion
    }
}
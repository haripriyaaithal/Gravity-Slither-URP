using System.Collections.Generic;
using GS.UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace GS.UI {
    public class TutorialsUIPanel : PanelBase {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;

        [Header("UI Transitions")] [SerializeField]
        private AnimationUIElement _panelHeader;

        [SerializeField] private AnimationUIElement _panelBody;
        [SerializeField] private CanvasGroup _panelCanvasGroup;

        private List<int> _tweenIdList;
        private float _toScrollValue;

        #region UI Transitions

        private void AnimateUIOpen() {
            LeanTween.alphaCanvas(_panelCanvasGroup, 1, 0);

            LeanTween.move(_panelHeader.target, _panelHeader.fromRect.anchoredPosition3D, 0);
            _tweenIdList.Add(LeanTween.move(_panelHeader.target, _panelHeader.endPosition, _panelHeader.time)
                .setFrom(_panelHeader.fromRect.anchoredPosition3D).setEaseOutExpo().setDelay(_panelHeader.delay)
                .uniqueId);

            LeanTween.move(_panelBody.target, _panelBody.fromRect.anchoredPosition3D, 0);
            _tweenIdList.Add(LeanTween.move(_panelBody.target, _panelBody.endPosition, _panelBody.time)
                .setFrom(_panelBody.fromRect.anchoredPosition3D).setEaseOutExpo().setDelay(_panelBody.delay).uniqueId);
            _panelCanvasGroup.blocksRaycasts = true;
        }

        private void AnimateUIClose(System.Action callback) {
            _panelCanvasGroup.blocksRaycasts = false;
            _tweenIdList.Add(LeanTween
                .move(_panelHeader.target, _panelHeader.fromRect.anchoredPosition3D, _panelHeader.time)
                .setFrom(_panelHeader.target.anchoredPosition3D).setEaseOutExpo().uniqueId);
            _tweenIdList.Add(LeanTween.move(_panelBody.target, _panelBody.fromRect.anchoredPosition3D, _panelBody.time)
                .setFrom(_panelBody.target.anchoredPosition3D).setEaseOutExpo().setOnComplete(
                    () => { callback?.Invoke(); }).uniqueId);

            LeanTween.alphaCanvas(_panelCanvasGroup, 0, 0.1f);
        }

        private void AnimateScroll() {
            LeanTween.value(gameObject, _scrollRect.horizontalNormalizedPosition, _toScrollValue, 0.3f).setOnUpdate(
                (value) => {
                    _scrollRect.horizontalNormalizedPosition = value;
                    HandleButtons();
                }).setEaseOutExpo();
        }

        #endregion

        #region Panel Stacker implementation

        public override void OnPanelOpened() {
            _scrollRect.horizontalNormalizedPosition = 0f;
            _toScrollValue = 0;
            HandleButtons();
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

        public void OnClickBack() {
            PanelStacker.RemovePanel(this);
        }

        public void OnClickNext() {
            _toScrollValue = Mathf.Clamp(_toScrollValue + (1 / 5f), 0, 1);
            AnimateScroll();
        }

        public void OnClickPrevious() {
            _toScrollValue = Mathf.Clamp(_toScrollValue - (1 / 5f), 0, 1);
            AnimateScroll();
        }

        #endregion

        private void HandleButtons() {
            _previousButton.interactable = _scrollRect.horizontalNormalizedPosition > 1 / 6f;
            _nextButton.interactable = _scrollRect.horizontalNormalizedPosition < 5 / 6f;
        }
    }
}
using System.Collections.Generic;
using GS.Audio;
using GS.Common;
using GS.UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace GS.UI {
    public class SettingsUIPanel : PanelBase {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Toggle _touchToggle;
        [SerializeField] private Toggle _onScreenToggle;
        [SerializeField] private Toggle _vibrationToggle;

        [Header("UI Transitions")] 
        [SerializeField] private AnimationUIElement _panelHeader;
        [SerializeField] private AnimationUIElement _panelBody;
        [SerializeField] private CanvasGroup _panelCanvasGroup;

        [Header("WebGL Specific Objects")] [SerializeField]
        private GameObject _controlsRoot;

        [SerializeField] private GameObject _vibrationRoot;

        private List<int> _tweenIdList;
        private SettingsData _settingsData;

        private void Awake() {
#if UNITY_WEBGL
            _controlsRoot.SetActive(false);
            _vibrationRoot.SetActive(false);
#endif
        }

        #region UI Transitions

        private void AnimateUIOpen() {
            LeanTween.alphaCanvas(_panelCanvasGroup, 1, 0);

            LeanTween.move(_panelHeader.target, _panelHeader.fromRect.anchoredPosition3D, 0);
            _tweenIdList.Add(LeanTween.move(_panelHeader.target, _panelHeader.endPosition, _panelHeader.time)
                .setFrom(_panelHeader.fromRect.anchoredPosition3D).setEaseOutExpo().setDelay(_panelHeader.delay).uniqueId);

            LeanTween.move(_panelBody.target, _panelBody.fromRect.anchoredPosition3D, 0);
            _tweenIdList.Add(LeanTween.move(_panelBody.target, _panelBody.endPosition, _panelBody.time)
                .setFrom(_panelBody.fromRect.anchoredPosition3D).setEaseOutExpo().setDelay(_panelBody.delay).uniqueId);
            _panelCanvasGroup.blocksRaycasts = true;
        }

        private void AnimateUIClose(System.Action callback) {
            _panelCanvasGroup.blocksRaycasts = false;
            _tweenIdList.Add(LeanTween.move(_panelHeader.target, _panelHeader.fromRect.anchoredPosition3D, _panelHeader.time)
                .setFrom(_panelHeader.target.anchoredPosition3D).setEaseOutExpo().uniqueId);
            _tweenIdList.Add(LeanTween.move(_panelBody.target, _panelBody.fromRect.anchoredPosition3D, _panelBody.time)
                .setFrom(_panelBody.target.anchoredPosition3D).setEaseOutExpo().setOnComplete(
                    () => { callback?.Invoke(); }).uniqueId);
            LeanTween.alphaCanvas(_panelCanvasGroup, 0, 0.1f);
        }

        #endregion

        #region Panel Stacker implementation

        public override void OnPanelOpened() {
            UpdateUI();
            _musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
            _soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
            _tweenIdList = _tweenIdList ?? new List<int>();
            _tweenIdList.ForEach(LeanTween.cancel);
            _tweenIdList.Clear();
            base.OnPanelOpened();
            AnimateUIOpen();
        }

        public override void OnPanelClosed() {
            AnimateUIClose(base.OnPanelClosed);
            _musicSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
            _soundSlider.onValueChanged.RemoveListener(OnSoundSliderValueChanged);
        }

        #endregion

        private void UpdateUI() {
            _settingsData = _settingsData ?? Settings.GetInstance().GetSettings();
            _musicSlider.value = _settingsData.GetMusicVolume() * _musicSlider.maxValue;
            _soundSlider.value = _settingsData.GetSoundVolume() * _soundSlider.maxValue;
            _touchToggle.isOn = _settingsData.GetControls() == GlobalConstants.ControlType.Touch;
            _onScreenToggle.isOn = _settingsData.GetControls() == GlobalConstants.ControlType.OnScreen;
            _vibrationToggle.isOn = _settingsData.ShouldVibrate();
        }

        #region UI Callbacks

        public void OnClickBack() {
            _panelCanvasGroup.blocksRaycasts = false;
            PlayBackSound();
            PanelStacker.RemovePanel(this);
        }

        public void OnMusicSliderValueChanged(float value) {
            PlayTouchSound();
            _settingsData.SetMusicVolume(value / _musicSlider.maxValue);
            Settings.GetInstance().SaveSettings(_settingsData);
            UpdateVolumes();
        }

        public void OnSoundSliderValueChanged(float value) {
            PlayTouchSound();
            _settingsData.SetSoundVolume(value / _soundSlider.maxValue);
            Settings.GetInstance().SaveSettings(_settingsData);
            UpdateVolumes();
        }

        public void OnToggleTouch(bool isOn) {
            PlayTouchSound();
            if (isOn) {
                _settingsData.SetControls(GlobalConstants.ControlType.Touch);
                Settings.GetInstance().SaveSettings(_settingsData);
            }
        }

        public void OnToggleOnScreen(bool isOn) {
            PlayTouchSound();
            if (isOn) {
                _settingsData.SetControls(GlobalConstants.ControlType.OnScreen);
                Settings.GetInstance().SaveSettings(_settingsData);
            }
        }

        public void OnToggleVibration(bool isOn) {
            PlayTouchSound();
            _settingsData.SetVibration(isOn);
            Settings.GetInstance().SaveSettings(_settingsData);
        }

        public void OnClickShowTutorial() {
            PlayTouchSound();
            var v_tutorialPanel = UIFactory.Get<TutorialsUIPanel>();
            if (v_tutorialPanel != null) {
                PanelStacker.AddPanel(v_tutorialPanel);
            }
        }

        #endregion

        private static void UpdateVolumes() {
            var v_audioManager = ManagerFactory.Get<AudioManager>();
            v_audioManager.UpdateVolumes();
        }
    }
}
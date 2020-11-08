using GS.Audio;
using GS.Common;
using GS.UI.Common;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace GS.UI {
    public class SettingsUIPanel : PanelBase {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Toggle _touchToggle;
        [SerializeField] private Toggle _onScreenToggle;
        [SerializeField] private Toggle _vibrationToggle;

        [Header("WebGL Specific Objects")] 
        [SerializeField] private GameObject _controlsRoot;
        [SerializeField] private GameObject _vibrationRoot;

        private SettingsData _settingsData;

        private void Awake() {
#if UNITY_WEBGL
            _controlsRoot.SetActive(false);
            _vibrationRoot.SetActive(false);
#endif
        }

        public override void OnPanelOpened() {
            UpdateUI();
            _musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
            _soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
            base.OnPanelOpened();
        }

        public override void OnPanelClosed() {
            base.OnPanelClosed();
            _musicSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
            _soundSlider.onValueChanged.RemoveListener(OnSoundSliderValueChanged);
        }

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
            // TODO: Open tutorials
        }

        #endregion

        private static void UpdateVolumes() {
            var v_audioManager = ManagerFactory.Get<AudioManager>();
            v_audioManager.UpdateVolumes();
        }
    }
}
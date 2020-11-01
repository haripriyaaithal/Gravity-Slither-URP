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
            base.OnPanelOpened();
        }

        public override void OnPanelClosed() {
            base.OnPanelClosed();
        }

        private void UpdateUI() {
            _settingsData = Settings.GetInstance().GetSettings();
            _musicSlider.value = _settingsData.GetMusicVolume() * _musicSlider.maxValue;
            _soundSlider.value = _settingsData.GetSoundVolume() * _soundSlider.maxValue;
            _touchToggle.isOn = _settingsData.GetControls() == GlobalConstants.ControlType.Touch;
            _onScreenToggle.isOn = _settingsData.GetControls() == GlobalConstants.ControlType.OnScreen;
            _vibrationToggle.isOn = _settingsData.ShouldVibrate();
        }

        #region UI Callbacks

        public void OnClickBack() {
            PanelStacker.RemovePanel(this);
        }

        public void OnMusicSliderValueChanged(float value) {
            _settingsData.SetMusicVolume(value / _musicSlider.maxValue);
            Settings.GetInstance().SaveSettings(_settingsData);
            UpdateVolumes();
        }
        
        public void OnSoundSliderValueChanged(float value) {
            _settingsData.SetSoundVolume(value / _soundSlider.maxValue);
            Settings.GetInstance().SaveSettings(_settingsData);
            UpdateVolumes();
        }

        public void OnToggleTouch(bool isOn) {
            if (isOn) {
                _settingsData.SetControls(GlobalConstants.ControlType.Touch);
            }
        }
        
        public void OnToggleOnScreen(bool isOn) {
            if (isOn) {
                _settingsData.SetControls(GlobalConstants.ControlType.OnScreen);   
            }
        }
        
        public void OnToggleVibration(bool isOn) {
            _settingsData.SetVibration(isOn);   
        }

        public void OnClickShowTutorial() {
            // TODO: Open tutorials
        }
        #endregion
        
        private static void UpdateVolumes() {
            var v_audioManager = ManagerFactory.Get<AudioManager>();
            v_audioManager.UpdateVolumes();
        }
    }
}
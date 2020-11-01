using Newtonsoft.Json;
using UnityEngine;

namespace GS.Common {
    public class Settings {
        private static Settings _instance;
        private SettingsData _currentSettings;

        private Settings() { }

        public static Settings GetInstance() {
            return _instance ?? new Settings();
        }

        public SettingsData GetSettings() {
            if (_currentSettings != null) {
                return _currentSettings;
            }

            var v_currentSettings = PlayerPrefs.GetString(nameof(Settings), string.Empty);
            _currentSettings = string.IsNullOrEmpty(v_currentSettings)
                ? new SettingsData()
                : JsonConvert.DeserializeObject<SettingsData>(v_currentSettings);

            return _currentSettings;
        }

        public void SaveSettings(SettingsData settingsData) {
            var v_settingsJson = JsonConvert.SerializeObject(settingsData);
            var v_currentSettings = PlayerPrefs.GetString(nameof(Settings), string.Empty);
            if (v_currentSettings.Equals(v_settingsJson)) {
                return;
            }

            UnityEngine.Debug.LogFormat("Saving settings: {0}".ToGreen(), v_settingsJson);
            CloneSettings(settingsData);
            PlayerPrefs.SetString(nameof(Settings), v_settingsJson);
        }

        private void CloneSettings(SettingsData settingsData) {
            _currentSettings = _currentSettings ?? GetSettings();
            _currentSettings.SetControls(settingsData.GetControls());
            _currentSettings.SetVibration(settingsData.ShouldVibrate());
            _currentSettings.SetMusicVolume(settingsData.GetMusicVolume());
            _currentSettings.SetSoundVolume(settingsData.GetSoundVolume());
        }
    }

    public class SettingsData {
        [JsonProperty("music")] private float _music;
        [JsonProperty("sound")] private float _sound;
        [JsonProperty("controls")] private int _controls;
        [JsonProperty("vibration")] private bool _vibration;

        public SettingsData() {
            _music = GlobalConstants.DefaultMusicVolume;
            _sound = GlobalConstants.DefaultSoundVolume;
            _controls = (int) GlobalConstants.ControlType.Touch;
            _vibration = true;
        }

        public float GetMusicVolume() {
            return _music;
        }

        public float GetSoundVolume() {
            return _sound;
        }

        public GlobalConstants.ControlType GetControls() {
            return (GlobalConstants.ControlType) _controls;
        }

        public bool ShouldVibrate() {
            return _vibration;
        }

        public void SetMusicVolume(float value) {
            _music = value;
        }

        public void SetSoundVolume(float value) {
            _sound = value;
        }

        public void SetControls(GlobalConstants.ControlType type) {
            _controls = (int) type;
        }

        public void SetVibration(bool value) {
            _vibration = value;
        }
    }
}
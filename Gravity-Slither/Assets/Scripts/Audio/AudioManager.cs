using GS.Common;
using UnityEngine;

namespace GS.Audio {
    public class AudioManager : MonoBehaviour {
        [SerializeField] private AudioClip _backgroundMusic;
        [SerializeField] private AudioClip _touchSound;
        [SerializeField] private AudioClip _backSound;
        [SerializeField] private AudioClip _foodCollectSound;
        [SerializeField] private AudioClip _snakeHitSound;
        [SerializeField] private AudioClip _timerSound;

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _soundSource;

        private void Start() {
            _musicSource.clip = _backgroundMusic;
            _musicSource.playOnAwake = true;
            UpdateVolumes();
        }

        public void UpdateVolumes() {
            var v_settings = Settings.GetInstance().GetSettings();
            _musicSource.volume = v_settings.GetMusicVolume();
            _soundSource.volume = v_settings.GetSoundVolume();
        }

        public void PlayTouchSound() {
            _soundSource.Stop();
            _soundSource.PlayOneShot(_touchSound);
        }
        
        public void PlayBackSound() {
            _soundSource.Stop();
            _soundSource.PlayOneShot(_backSound);
        }

        public void PlayFoodCollectSound() {
            _soundSource.Stop();
            _soundSource.PlayOneShot(_foodCollectSound);
        }

        public void PlaySnakeHitSound() {
            _soundSource.Stop();
            _soundSource.PlayOneShot(_snakeHitSound);
        }

        public void PlayTimerSound() {
            _soundSource.Stop();
            _soundSource.clip = _timerSound;
            _soundSource.Play();
        }
    }
}
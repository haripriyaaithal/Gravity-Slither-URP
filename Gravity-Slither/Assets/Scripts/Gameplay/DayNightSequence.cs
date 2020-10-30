using GS.Common;
using UnityEngine;
using UnityEngine.Playables;

namespace GS.Gameplay.Environment {
    public class DayNightSequence : MonoBehaviour {
        [SerializeField] private PlayableDirector _playableDirector;
        [SerializeField] private Color _skyboxColor;

        private Color _defaultColor;
        private bool _canChangeColor;

        #region Unity event methods

        private void Awake() {
            _defaultColor = _skyboxColor;
        }

        private void OnEnable() {
            EventManager.GetInstance().onGameStart += OnGameStart;
            EventManager.GetInstance().onRevive += OnRevive;
            EventManager.GetInstance().onGameOver += OnGameOver;
        }

        private void OnDisable() {
            EventManager.GetInstance().onGameStart -= OnGameStart;
            EventManager.GetInstance().onRevive -= OnRevive;
            EventManager.GetInstance().onGameOver -= OnGameOver;
            RenderSettings.skybox.SetColor(GlobalConstants.Tint, _defaultColor);
        }

        private void Update() {
            if (!_canChangeColor) {
                return;
            }

            RenderSettings.skybox.SetColor(GlobalConstants.Tint, _skyboxColor);
        }

        #endregion


        #region Event handlers

        private void OnGameStart() {
            _skyboxColor = _defaultColor;
            _canChangeColor = true;
            _playableDirector.playOnAwake = false;
            _playableDirector.Play();
        }

        private void OnRevive() {
            _canChangeColor = true;
            _playableDirector.Resume();
        }

        private void OnGameOver() {
            _canChangeColor = false;
            _playableDirector.Pause();
        }

        #endregion
    }
}
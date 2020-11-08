using GS.Common;
using GS.Gameplay.Player;
using GS.Gameplay.Spawner;
using GS.UI;
using GS.UI.Common;
using UnityEngine;

namespace GS.Gameplay {
    public class ScoreManager : MonoBehaviour {
        private static ScoreManager _scoreManager;

        private PlayerController _playerController;
        private double _score = 0;

        public static ScoreManager GetInstance() {
            if (_scoreManager == null) {
                _scoreManager = FindObjectOfType<ScoreManager>();
            }

            return _scoreManager;
        }

        #region Unity event methods

        private void Awake() {
            _playerController = ManagerFactory.Get<PlayerController>();
        }

        private void OnEnable() {
            EventManager.GetInstance().onEatFood += OnEatFood;
            EventManager.GetInstance().onHitRock += OnHitRock;
        }

        private void OnDisable() {
            EventManager.GetInstance().onEatFood -= OnEatFood;
            EventManager.GetInstance().onHitRock -= OnHitRock;
        }

        #endregion

        private void OnEatFood(Food food) {
            AddScore(_playerController.GetSpeed() * GlobalConstants.ScoreMultiplier);
        }

        private void OnHitRock() {
            _score = (_score > GlobalConstants.ScoreMultiplier * 10) ? (_score - GlobalConstants.ScoreMultiplier * 10) : 0;
            UpdateScoreUI();
        }

        private void AddScore(float score) {
            _score += score;
            UpdateScoreUI();
        }

        private void UpdateScoreUI() {
            var v_gameplayUIPanel = UIFactory.Get<GameplayUIPanel>();
            if (v_gameplayUIPanel != null) {
                v_gameplayUIPanel.SetScore(_score.ToString());
            }
        }

        public double GetScore() {
            return _score;
        }
    }
}
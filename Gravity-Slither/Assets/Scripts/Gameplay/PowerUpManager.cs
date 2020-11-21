using GS.Common;
using GS.Gameplay.Spawner;
using GS.UI;
using GS.UI.Common;
using UnityEngine;

namespace GS.Gameplay {
    public class PowerUpManager : MonoBehaviour {

        private byte _foodCounter;
        private bool _isInvisible;
        private int _delayedCallId;
        
        #region Unity event methods

        private void OnEnable() {
            EventManager.GetInstance().onEatFood += OnEatFood;
        }

        private void OnDisable() {
            EventManager.GetInstance().onEatFood -= OnEatFood;
        }

        #endregion

        #region Event handlers

        private void OnEatFood(Food food) {
            _foodCounter++;
            if (!_isInvisible && _foodCounter % GlobalConstants.FoodCountForPowerUp == 0) {
                InvisiblePowerUp(GlobalConstants.InvisiblePowerUpTime);
            }
        }

        #endregion

        public void InvisiblePowerUp(int powerUpTime) {
            MakePlayerInvisible();
            ShowUI(powerUpTime);
            LeanTween.cancel(_delayedCallId);
            _delayedCallId = LeanTween.delayedCall(powerUpTime, MakePlayerVisible).uniqueId;
        }
        
        private void MakePlayerInvisible() {
            _isInvisible = true;
            EventManager.GetInstance().OnInvisiblePowerUp(true);
        }

        private void MakePlayerVisible() {
            _isInvisible = false;
            EventManager.GetInstance().OnInvisiblePowerUp(false);
        }
        
        private static void ShowUI(int time) {
            var v_powerUpTimerUI = UIFactory.Get<PowerUpTimerUI>();
            PanelStacker.AddPanel(v_powerUpTimerUI);
            v_powerUpTimerUI.Initialize(time);
        }
    }
}
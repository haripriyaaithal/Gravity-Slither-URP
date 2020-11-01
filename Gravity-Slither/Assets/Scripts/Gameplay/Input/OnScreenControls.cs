using GS.Common;
using UnityEngine;

namespace GS.Gameplay.Inputs {
    public class OnScreenControls : MonoBehaviour {
#if UNITY_ANDROID || UNITY_IOS
        private InputManager _inputManager;
        private bool _gameStarted;
#endif
        private void Awake() {
            
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            gameObject.SetActive(false);
#elif UNITY_ANDROID || UNITY_IOS
            _inputManager = ManagerFactory.Get<InputManager>();
            gameObject.SetActive(Settings.GetInstance().GetSettings().GetControls() == GlobalConstants.ControlType.OnScreen);            
#endif
        }

#if !UNITY_EDITOR && UNITY_ANDROID || UNITY_IOS

        #region UI callbacks

        public void OnClickUp() {
            InvokeGameStarted();
            Debug.Log("OnClickUp".ToBrown());
            _inputManager.MoveUp();
        }

        public void OnClickDown() {
            InvokeGameStarted();
            Debug.Log("OnClickDown".ToBrown());
            _inputManager.MoveDown();
        }

        public void OnClickLeft() {
            InvokeGameStarted();
            Debug.Log("OnClickLeft".ToBrown());
            _inputManager.MoveLeft();
        }

        public void OnClickRight() {
            InvokeGameStarted();
            Debug.Log("OnClickRight".ToBrown());
            _inputManager.MoveRight();
        }

        #endregion

        private void InvokeGameStarted() {
            if (_gameStarted) {
                return;
            }
            _gameStarted = true;
            EventManager.GetInstance().OnGameStart();
        }

#endif
    }
}
using GS.Common;
using UnityEngine;

namespace GS.Gameplay.Inputs {
    public class OnScreenControls : MonoBehaviour {
        private InputManager _inputManager;

        private void Awake() {
            _inputManager = ManagerFactory.Get<InputManager>();
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            gameObject.SetActive(false);
#endif
        }

#if !UNITY_EDITOR && UNITY_ANDROID || UNITY_IOS

        #region UI callbacks

        public void OnClickUp() {
            _inputManager.MoveUp();
        }

        public void OnClickDown() {
            _inputManager.MoveDown();
        }

        public void OnClickLeft() {
            _inputManager.MoveLeft();
        }

        public void OnClickRight() {
            _inputManager.MoveRight();
        }

        #endregion

#endif
    }
}
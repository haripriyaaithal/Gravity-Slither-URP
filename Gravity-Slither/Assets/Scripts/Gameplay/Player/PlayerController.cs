using GS.Common;
using GS.Gameplay.Inputs;
using GS.Gameplay.Spawner;
using UnityEngine;

namespace GS.Gameplay.Player {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private float _movementSpeed = 15;
        private Rigidbody _rigidbody;
        
        private bool _canMove;
        private InputManager _inputManager;

        #region Unity event functions

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            _inputManager = ManagerFactory.Get<InputManager>();
        }

        private void Update() {
            if (_canMove) {
                return;
            }
            // When player touches the screen or clicks any button, start the game.
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            if (Input.anyKeyDown) {
                _canMove = true;
            }
#elif UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount > 0) {
                _canMove = true;
            }
#endif
        }

        private void FixedUpdate() {
            if (_canMove) {
                _rigidbody.MovePosition(_rigidbody.position +
                                        transform.TransformDirection(_inputManager.GetMovementDirection()) *
                                        (_movementSpeed * Time.deltaTime));
            }
        }

        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag(GlobalConstants.World)) {
                return;
            }

            if (other.gameObject.CompareTag(GlobalConstants.Food)) {
                other.gameObject.GetComponent<Food>()?.Eat();
            }
            
            // TODO: If tree, game over
            Debug.Log($"Collision detected with {other.gameObject.name}".ToAqua(), this);
        }

        #endregion
    }
}
using GS.Common;
using GS.Gameplay.Inputs;
using GS.Gameplay.Spawner;
using UnityEngine;

namespace GS.Gameplay.Player {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private float _minSpeed = 12;
        [SerializeField] private float _maxSpeed = 22;
        [SerializeField] private float _movementSpeed = 15;
        
        private Rigidbody _rigidbody;
        private bool _canMove;
        private InputManager _inputManager;
        private byte _foodCounter;
        #region Unity event functions

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            _inputManager = ManagerFactory.Get<InputManager>();
            _foodCounter = 0;
        }

        private void Update() {
            if (_canMove) {
                return;
            }
            // When player touches the screen or clicks any button, start the game.
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            if (Input.anyKeyDown) {
                _canMove = true;
                EventManager.GetInstance().OnGameStart();
            }
#elif UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount > 0) {
                _canMove = true;
                EventManager.GetInstance().OnGameStart();
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
            HandleCollision(other);
        }

        private void OnEnable() {
            EventManager.GetInstance().onGameOver += OnGameOver;
            EventManager.GetInstance().onEatFood += OnEatFood;
        }

        private void OnDisable() {
            EventManager.GetInstance().onGameOver -= OnGameOver;
            EventManager.GetInstance().onEatFood -= OnEatFood;
        }

        #endregion

        private void HandleCollision(Collision other) {
            if (other.gameObject.CompareTag(GlobalConstants.Tree)) {
                GameOver();
            } else if (other.gameObject.CompareTag(GlobalConstants.SnakeBody) &&
                       other.transform.GetSiblingIndex() > 3) {
                GameOver();
            }
        }

        #region Event Handlers

        private void GameOver() {
            EventManager.GetInstance().OnGameOver();
        }

        private void OnGameOver() {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        private void OnEatFood(Food food) {
            if (_movementSpeed >= _maxSpeed) {
                return;
            }
            
            // Increase speed for every 3rd food
            if (_foodCounter < 3) {
                _foodCounter++;
            } else {
                _movementSpeed += 0.5f;
                _foodCounter = 0;
            }
        }

        #endregion

        public float GetSpeed() {
            return _movementSpeed;
        }
    }
}
using GS.Common;
using GS.Gameplay.Inputs;
using GS.Gameplay.Spawner;
using UnityEngine;

namespace GS.Gameplay.Player {
    public class PlayerController : MonoBehaviour {
        [Range(9, 30)] [SerializeField] private float _maxSpeed = 22;
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _invisibleMaterial;

        private Rigidbody _rigidbody;
        private MeshRenderer _meshRenderer;
        private bool _canMove;
        private InputManager _inputManager;
        private ushort _foodCounter;
        private float _movementSpeed = 12;
        private int _delayedCallId;
        private bool _isInvisible;

        #region Unity event functions

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();
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
            MakePlayerVisible();
        }

        private void OnDisable() {
            EventManager.GetInstance().onGameOver -= OnGameOver;
            EventManager.GetInstance().onEatFood -= OnEatFood;
        }

        private void OnDestroy() {
            LeanTween.cancel(_delayedCallId);
            _inputManager = null;
            _rigidbody = null;
        }

        #endregion

        private void HandleCollision(Collision other) {
            if (_isInvisible) {
                return;
            }

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
            LeanTween.cancel(_delayedCallId);
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        private void OnEatFood(Food food) {
            _foodCounter++;
            if (_foodCounter % 3 == 0) {
                MakePlayerInvisible();
                LeanTween.cancel(_delayedCallId);
                _delayedCallId = LeanTween.delayedCall(5f, MakePlayerVisible).uniqueId;
            }

            if (_movementSpeed >= _maxSpeed) {
                return;
            }

            // Increase speed for every 3rd food
            if (_foodCounter % 3 == 0) {
                _movementSpeed += 0.5f;
            }
        }

        #endregion

        #region Invisible powerup

        private void MakePlayerInvisible() {
            _isInvisible = true;
            EventManager.GetInstance().OnInvisiblePowerUp(true);
            _meshRenderer.material = _invisibleMaterial;
        }

        private void MakePlayerVisible() {
            _isInvisible = false;
            EventManager.GetInstance().OnInvisiblePowerUp(false);
            _meshRenderer.material = _defaultMaterial;
        }

        #endregion

        public float GetSpeed() {
            return _movementSpeed;
        }

        public Material GetDefaultMaterial() {
            return _defaultMaterial;
        }

        public Material GetInvisibleMaterial() {
            return _invisibleMaterial;
        }
    }
}
using GS.Audio;
using GS.Common;
using GS.Gameplay.Inputs;
using GS.Gameplay.Spawner;
using GS.UI;
using GS.UI.Common;
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
            if (Input.touchCount > 0 && Settings.GetInstance().GetSettings().GetControls() == GlobalConstants.ControlType.Touch) {
                _canMove = true;
                EventManager.GetInstance().OnGameStart();
            }
#endif
        }

        private void FixedUpdate() {
            if (_canMove) {
                _rigidbody.MovePosition(_rigidbody.position +
                                        transform.TransformDirection(_inputManager.GetMovementDirection()) *
                                        (_movementSpeed * Time.fixedDeltaTime));
            }
        }

        private void OnCollisionEnter(Collision other) {
            HandleCollision(other);
        }

        private void OnEnable() {
            EventManager.GetInstance().onRevive += OnRevive;
            EventManager.GetInstance().onGameStart += OnGameStart;
            EventManager.GetInstance().onResume += OnResume;
            EventManager.GetInstance().onPause += OnGamePause;
            EventManager.GetInstance().onGameOver += OnGameOver;
            EventManager.GetInstance().onEatFood += OnEatFood;
            MakePlayerVisible();
        }

        private void OnDisable() {
            EventManager.GetInstance().onRevive -= OnRevive;
            EventManager.GetInstance().onGameStart -= OnGameStart;
            EventManager.GetInstance().onResume -= OnResume;
            EventManager.GetInstance().onPause -= OnGamePause;
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

        private void OnGameStart() {
            _canMove = true;
        }
        
        private void GameOver() {
            if (Settings.GetInstance().GetSettings().ShouldVibrate()) {
                Vibration.Vibrate(GlobalConstants.GameOverVibrateDuration);
            }
            var v_audioManager = ManagerFactory.Get<AudioManager>();
            if (v_audioManager != null) {
                v_audioManager.PlaySnakeHitSound();
            }
            EventManager.GetInstance().OnGameOver();
        }

        private void OnGameOver() {
            LeanTween.cancel(_delayedCallId);
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        private void OnEatFood(Food food) {

            if (!_isInvisible) {
                _foodCounter++;
            }
            
            if (!_isInvisible && _foodCounter % GlobalConstants.FoodCountForPowerUp == 0) {
                InvisiblePowerUp(GlobalConstants.InvisiblePowerUpTime);
                var v_powerUpTimerUI = UIFactory.Get<PowerUpTimerUI>();
                PanelStacker.AddPanel(v_powerUpTimerUI);
                v_powerUpTimerUI.Initialize(GlobalConstants.InvisiblePowerUpTime);
            }

            // Increase speed for every 3rd food
            if ((_movementSpeed < _maxSpeed) && _foodCounter % GlobalConstants.FoodCountForSpeedIncrease == 0) {
                _movementSpeed += 0.5f;
            }
        }

        private void OnRevive() {
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            InvisiblePowerUp(GlobalConstants.RevivePowerUpTime);
            var v_powerUpTimerUI = UIFactory.Get<PowerUpTimerUI>();
            PanelStacker.AddPanel(v_powerUpTimerUI);
            v_powerUpTimerUI.Initialize(GlobalConstants.RevivePowerUpTime);
        }

        private void OnGamePause() {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            LeanTween.pauseAll();
        }

        private void OnResume() {
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            LeanTween.resumeAll();
        }

        #endregion

        #region Invisible powerup
        // TODO: Move this to PowerUpManager
        private void InvisiblePowerUp(int powerUpTime) {
            MakePlayerInvisible();
            LeanTween.cancel(_delayedCallId);
            _delayedCallId = LeanTween.delayedCall(powerUpTime, MakePlayerVisible).uniqueId;
        }

        private void MakePlayerInvisible() {
            _isInvisible = true;
            EventManager.GetInstance().OnInvisiblePowerUp(true);
            _meshRenderer.sharedMaterial = _invisibleMaterial;
        }

        private void MakePlayerVisible() {
            _isInvisible = false;
            EventManager.GetInstance().OnInvisiblePowerUp(false);
            _meshRenderer.sharedMaterial = _defaultMaterial;
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
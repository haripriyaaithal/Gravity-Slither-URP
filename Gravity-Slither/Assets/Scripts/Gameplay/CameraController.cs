using System.Collections;
using GS.Common;
using GS.Gameplay.Player;
using UnityEngine;

namespace GS.Gameplay {
    public class CameraController : MonoBehaviour {
        private const float CAMERA_DISTANCE = 2.5f;
        private const float ZOOM_SPEED = 3f;
        private const float MOVEMENT_SMOOTHNESS_MULTIPLIER = 9f;
        private const float SHAKE_INTENSITY = 0.15f;

        private float _defaultFOV;
        private float _zoomOutFOV;
        private bool _isPlaying;
        private Camera _camera;
        private Transform _playerTransform;
        private Transform _transform;

        #region Unity event methods

        private void Awake() {
            _camera = GetComponent<Camera>();
            _playerTransform = ManagerFactory.Get<PlayerController>()?.transform;
            _transform = transform;
            _defaultFOV = _camera.fieldOfView;
            _zoomOutFOV = _defaultFOV + 7;
        }

        private void OnEnable() {
            EventManager.GetInstance().onGameStart += OnGameStart;
            EventManager.GetInstance().onRevive += OnRevive;
            EventManager.GetInstance().onGameOver += OnGameOver;
            EventManager.GetInstance().onShakeCamera += ShakeCamera;
        }

        private void OnDisable() {
            EventManager.GetInstance().onGameStart -= OnGameStart;
            EventManager.GetInstance().onRevive -= OnRevive;
            EventManager.GetInstance().onGameOver -= OnGameOver;
            EventManager.GetInstance().onShakeCamera -= ShakeCamera;
        }

        private void FixedUpdate() {
            FollowPlayer();
            HandleZoom();
        }

        #endregion

        private void HandleZoom() {
            if (_isPlaying) {
                _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _zoomOutFOV, ZOOM_SPEED * Time.deltaTime);
            } else {
                _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _defaultFOV, ZOOM_SPEED * Time.deltaTime);
            }
        }

        private void FollowPlayer() {
            var v_direction = _playerTransform.position - Vector3.zero;
            _transform.rotation = Quaternion.Lerp(_transform.rotation,
                _playerTransform.rotation * Quaternion.Euler(90f, 0f, 0f),
                Time.deltaTime * MOVEMENT_SMOOTHNESS_MULTIPLIER);
            _transform.position = Vector3.Lerp(_transform.position, CAMERA_DISTANCE * v_direction,
                Time.deltaTime * MOVEMENT_SMOOTHNESS_MULTIPLIER);
        }

        #region Event handlers

        private void OnGameStart() {
            _isPlaying = true;
        }

        private void OnRevive() {
            _isPlaying = true;
        }

        private void OnGameOver() {
            _isPlaying = false;
            ShakeCamera();
        }

        #endregion

        #region Camera Shake

        private void ShakeCamera() {
            StartCoroutine(StartShake(0.25f));
        }

        private IEnumerator StartShake(float seconds) {
            var v_time = 0f;
            var v_newPosition = default(Vector3);
            while (v_time < seconds) {
                var v_currentPosition = _transform.position;
                v_newPosition.x = v_currentPosition.x + Random.Range(-SHAKE_INTENSITY, SHAKE_INTENSITY);
                v_newPosition.y = v_currentPosition.y + Random.Range(-SHAKE_INTENSITY, SHAKE_INTENSITY);
                v_newPosition.z = v_currentPosition.z + Random.Range(-SHAKE_INTENSITY, SHAKE_INTENSITY);

                _transform.position = v_newPosition;

                v_time += Time.deltaTime;

                yield return null;
            }
        }

        #endregion
    }
}
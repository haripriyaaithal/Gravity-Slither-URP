using System.Collections;
using GS.Common;
using UnityEngine;

namespace GS.Gameplay.Light {
    public class LightController : MonoBehaviour {
        [SerializeField] private float _rotateSpeed;

        private Transform _transform;
        private Coroutine _rotateLight;

        #region Unity event methods

        private void Awake() {
            _transform = transform;
        }

        private void OnEnable() {
            EventManager.GetInstance().onGameStart += OnGameStart;
            EventManager.GetInstance().onGameOver += OnGameOver;
        }

        private void OnDisable() {
            EventManager.GetInstance().onGameStart -= OnGameStart;
            EventManager.GetInstance().onGameOver -= OnGameOver;
        }

        #endregion

        private void OnGameStart() {
            _rotateLight = StartCoroutine(RotateLight());
        }

        private void OnGameOver() {
            if (_rotateLight != null) {
                StopCoroutine(_rotateLight);
            }
        }

        private IEnumerator RotateLight() {
            while (true) {
                _transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
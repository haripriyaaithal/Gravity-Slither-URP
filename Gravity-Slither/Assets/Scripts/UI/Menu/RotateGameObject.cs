using UnityEngine;

namespace GS.Common {
    public class RotateGameObject : MonoBehaviour {
        [SerializeField] private float _angle;

        private Transform _transform;

        private void Awake() {
            _transform = transform;
        }

        private void Update() {
            _transform.RotateAround(Vector3.zero, Vector3.up, _angle * Time.deltaTime);
        }
    }
}
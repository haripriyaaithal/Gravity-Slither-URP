using UnityEngine;

namespace GS.Gameplay.Gravity {
    public class FauxGravityBody : MonoBehaviour {
        private FauxGravityAttractor _attractor;
        private Transform _transform;
        private Rigidbody _rigidbody;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start() {
            _attractor = FauxGravityAttractor.GetInstance();
            _transform = transform;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _rigidbody.useGravity = false;
        }

        private void Update() {
            _attractor.Attract(_transform, _rigidbody);
        }
    }
}
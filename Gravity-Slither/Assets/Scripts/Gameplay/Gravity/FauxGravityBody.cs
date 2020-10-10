using UnityEngine;

namespace GS.Gameplay.Gravity {
    public class FauxGravityBody : MonoBehaviour {
        [SerializeField] private FauxGravityAttractor _attractor;
        private Transform _myTransform;
        private Rigidbody _rigidbody;

        private void Start() {
            _myTransform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _rigidbody.useGravity = false;
        }

        private void Update() {
            _attractor.Attract(_myTransform, _rigidbody);
        }
    }
}
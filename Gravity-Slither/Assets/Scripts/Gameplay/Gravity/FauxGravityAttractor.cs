using UnityEngine;

namespace GS.Gameplay.Gravity {
    public class FauxGravityAttractor : MonoBehaviour {
        private float _gravity = -10f;
        private float _rotateSpeed = 50f;

        private static FauxGravityAttractor _instance;

        public static FauxGravityAttractor GetInstance() {
            _instance = _instance ? _instance : FindObjectOfType<FauxGravityAttractor>();
            return _instance;
        } 
        
        public void Attract(Transform body, Rigidbody rb) {
            var v_gravityUp = (body.position - transform.position).normalized;
            rb.AddForce(v_gravityUp * _gravity);

            var v_rotation = body.rotation;
            var v_targetRotation = Quaternion.FromToRotation(body.up, v_gravityUp) * v_rotation;
            body.rotation = Quaternion.Slerp(v_rotation, v_targetRotation, _rotateSpeed * Time.deltaTime);
        }
    }
}
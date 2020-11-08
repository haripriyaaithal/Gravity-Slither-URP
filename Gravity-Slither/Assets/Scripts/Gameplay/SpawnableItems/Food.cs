using GS.Audio;
using GS.Common;
using UnityEngine;

namespace GS.Gameplay.Spawner {
    public class Food : MonoBehaviour {
        [SerializeField] private ParticleSystem _foodCollectParticles;

        private MeshRenderer _meshRenderer;
        private SphereCollider _sphereCollider;
        private Transform _transform;

        #region Unity methods

        private void Awake() {
            _meshRenderer = GetComponent<MeshRenderer>();
            _sphereCollider = GetComponent<SphereCollider>();
            _transform = transform;
        }

        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag(GlobalConstants.Player)) {
                EatFood();
                var v_audioManager = ManagerFactory.Get<AudioManager>();
                if (v_audioManager != null) {
                    v_audioManager.PlayFoodCollectSound();
                }
            }
        }

        #endregion

        public void Initialise() {
            EnableRendererAndCollider(true);
            OrientParticleEffectToNormal();
            AnimateSpawn();
        }

        private void EatFood() {
            EventManager.GetInstance().OnEatFood(this);
            EnableRendererAndCollider(false);
            _foodCollectParticles.Play();
        }

        private void AnimateSpawn() {
            LeanTween.scale(gameObject, Vector3.one, 0.3f).setFrom(Vector3.zero);
        }

        private void EnableRendererAndCollider(bool enable) {
            _meshRenderer.enabled = enable;
            _sphereCollider.enabled = enable;
        }

        private void OrientParticleEffectToNormal() {
            var v_normal = _transform.position - Vector3.zero;
            _transform.rotation =
                Quaternion.LookRotation(v_normal, _transform.forward);
        }
    }
}
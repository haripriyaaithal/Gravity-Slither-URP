using GS.Common;
using UnityEngine;

namespace GS.Gameplay.Spawner {
    public class Rock : MonoBehaviour {
        [SerializeField] private ParticleSystem _particleEffects;
        private MeshRenderer _meshRenderer;
        private SphereCollider _sphereCollider;

        #region Unity methods

        private void Awake() {
            _meshRenderer = GetComponent<MeshRenderer>();
            _sphereCollider = GetComponent<SphereCollider>();
        }

        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag(GlobalConstants.Player)) {
                EventManager.GetInstance().OnHitRock();
                EventManager.GetInstance().OnShakeCamera();
                AnimateDestroy();
            }
        }

        #endregion

        public void Initialise() {
            EnableRendererAndCollider(true);
            AnimateSpawn();
        }

        private void EnableRendererAndCollider(bool enable) {
            _meshRenderer.enabled = enable;
            _sphereCollider.enabled = enable;
        }

        #region Animations

        private void AnimateSpawn() {
            EnableRendererAndCollider(true);
            LeanTween.scale(gameObject, Vector3.one, 0.3f).setFrom(Vector3.zero);
        }

        private void AnimateDestroy() {
            EnableRendererAndCollider(false);
            _particleEffects.Play();
            LeanTween.delayedCall(_particleEffects.main.duration, () => { gameObject.SetActive(false); });
        }

        #endregion
    }
}
using GS.Audio;
using GS.Common;
using UnityEngine;

namespace GS.Gameplay.Spawner {
    public class Rock : MonoBehaviour {
        [SerializeField] private ParticleSystem _particleEffects;
        
        private MeshRenderer _meshRenderer;
        private SphereCollider _sphereCollider;
        private bool _isPlayerInvisible;
        
        #region Unity methods

        private void Awake() {
            _meshRenderer = GetComponent<MeshRenderer>();
            _sphereCollider = GetComponent<SphereCollider>();
        }

        private void OnCollisionEnter(Collision other) {
            if (!_isPlayerInvisible && other.gameObject.CompareTag(GlobalConstants.Player)) {
                EventManager.GetInstance().OnHitRock();
                var v_audioManager = ManagerFactory.Get<AudioManager>();
                if (v_audioManager != null) {
                    v_audioManager.PlaySnakeHitSound();
                }
                EventManager.GetInstance().OnShakeCamera();
                AnimateDestroy();
            }
        }

        private void OnEnable() {
            EventManager.GetInstance().onInvisiblePowerUp += OnInvisiblePowerUp;
        }

        private void OnDisable() {
            EventManager.GetInstance().onInvisiblePowerUp -= OnInvisiblePowerUp;
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

        private void OnInvisiblePowerUp(bool enable) {
            _isPlayerInvisible = enable;
            _sphereCollider.isTrigger = enable;
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
using UnityEngine;

namespace GS.Gameplay.Spawner {
    public class Rock : MonoBehaviour {
        
        private MeshRenderer _meshRenderer;
        private SphereCollider _sphereCollider;
        
        #region Unity methods

        private void Awake() {
            _meshRenderer = GetComponent<MeshRenderer>();
            _sphereCollider = GetComponent<SphereCollider>();
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
        
        private void AnimateSpawn() {
            LeanTween.scale(gameObject, Vector3.one, 0.3f).setFrom(Vector3.zero);
        }
    }
}
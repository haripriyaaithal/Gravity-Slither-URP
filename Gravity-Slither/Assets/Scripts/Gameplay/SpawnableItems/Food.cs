using GS.Common;
using UnityEngine;

namespace GS.Gameplay.Spawner {
    public class Food : MonoBehaviour {
        private void OnEnable() {
            // Spawn animation
            LeanTween.scale(gameObject, Vector3.one, 0.3f).setFrom(Vector3.zero);
        }

        public void Eat() {
            EventManager.OnEatFood(this);
        }
    }
}
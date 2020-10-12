using GS.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GS.Gameplay.Spawner {
    public class FoodSpawner : SpawnerBase {
        [SerializeField] private float _worldRadius;

        #region SpawnerBase methods

        protected override void Awake() {
            base.Awake();
            _pool = new UnityPool(_prefab.gameObject, GlobalConstants.FoodMaxCapacity, _parent);
        }

        protected override void Spawn() {
            base.Spawn();
            var v_food = _pool.Get<Food>(_parent);
            _spawnedObjectsList.Add(v_food.gameObject);
            v_food.transform.position = GetRandomPoint();
            v_food.Initialise();
        }

        #endregion

        #region Unity event methods

        private void OnEnable() {
            EventManager.onEatFood += OnEatFood;
        }

        private void OnDisable() {
            EventManager.onEatFood -= OnEatFood;
        }

        #endregion
        
        private Vector3 GetRandomPoint() {
            // Return position where other objects are not present.
            var v_randomPoint = default(Vector3);
            var v_colliders = new Collider[3];
            do {
                v_randomPoint = Random.onUnitSphere * _worldRadius;
            } while (Physics.OverlapSphereNonAlloc(v_randomPoint, 2f, v_colliders,
                LayerMask.NameToLayer(GlobalConstants.World)) != 0);

            return v_randomPoint;
        }

        private void OnEatFood(Food food) {
            LeanTween.delayedCall(0.5f, () => ReturnToPool(food.gameObject));
        }

        #region Debug

        public void OnClickDebugSpawnFood() {
            ReturnAllToPool();
            for (var v_index = 0; v_index < 8; v_index++) {
                Spawn();
            }
        }

        #endregion
    }
}
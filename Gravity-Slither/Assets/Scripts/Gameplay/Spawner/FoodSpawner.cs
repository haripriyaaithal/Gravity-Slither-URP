using GS.Common;
using UnityEngine;

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
        }
        #endregion

        private Vector3 GetRandomPoint() {
            return Random.onUnitSphere * _worldRadius;
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
using GS.Common;
using UnityEngine;

namespace GS.Gameplay.Spawner {
    public class FoodSpawner : SpawnerBase {
        #region SpawnerBase methods

        protected override void Awake() {
            base.Awake();
            _pool = new UnityPool(_prefab.gameObject, GlobalConstants.FoodMaxCapacity, _parent);
        }

        protected override void Spawn() {
            base.Spawn();
            var v_food = _pool.Get<Food>(_parent);
            v_food.transform.localScale = Vector3.zero;
            _spawnedObjectsList.Add(v_food.gameObject);
            v_food.transform.position = GetRandomPoint(LayerMask.NameToLayer(GlobalConstants.World));
            v_food.Initialise();
        }

        #endregion

        #region Unity event methods

        private void Start() {
            for (var v_index = 0; v_index < GlobalConstants.FoodMaxCapacity; v_index++) {
                Spawn();
            }
        }

        private void OnEnable() {
            EventManager.GetInstance().onEatFood += OnEatFood;
        }

        private void OnDisable() {
            EventManager.GetInstance().onEatFood -= OnEatFood;
        }

        #endregion

        private void OnEatFood(Food food) {
            LeanTween.delayedCall(0.5f, () => {
                if (food != null) {
                    ReturnToPool(food.gameObject);
                    Spawn();
                }
            });
        }
    }
}
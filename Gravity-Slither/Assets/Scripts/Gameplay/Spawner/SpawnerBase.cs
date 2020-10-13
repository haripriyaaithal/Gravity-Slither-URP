using System.Collections.Generic;
using GS.Common;
using UnityEngine;

namespace GS.Gameplay.Spawner {
    public abstract class SpawnerBase : MonoBehaviour {
        [SerializeField] protected GameObject _prefab;
        [SerializeField] protected Transform _parent;
        [SerializeField] private float _worldRadius;

        protected UnityPool _pool;
        protected List<GameObject> _spawnedObjectsList;

        protected virtual void Awake() {
            _spawnedObjectsList = new List<GameObject>();
        }

        protected virtual void Spawn() { }

        protected void ReturnToPool(GameObject go) {
            _pool?.Add(go);
            _spawnedObjectsList?.Remove(go);
        }

        protected void ReturnAllToPool() {
            _spawnedObjectsList?.ForEach(obj => _pool?.Add(obj));
            _spawnedObjectsList?.Clear();
        }

        protected Vector3 GetRandomPoint(int layerMask) {
            // Return position where other objects are not present.
            var v_randomPoint = default(Vector3);
            var v_colliders = new Collider[3];
            do {
                v_randomPoint = Random.onUnitSphere * _worldRadius;
            } while (Physics.OverlapSphereNonAlloc(v_randomPoint, 2f, v_colliders,
                layerMask) != 0);

            return v_randomPoint;
        }
    }
}
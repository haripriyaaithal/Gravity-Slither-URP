using System.Collections.Generic;
using GS.Common;
using UnityEngine;

namespace GS.Gameplay.Spawner {
    public abstract class SpawnerBase : MonoBehaviour {
        [SerializeField] protected GameObject _prefab;
        [SerializeField] protected Transform _parent;
        
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
    }
}
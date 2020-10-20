using System.Collections;
using GS.Common;
using UnityEngine;

namespace GS.Gameplay.Spawner {
    public class RockSpawner : SpawnerBase {
        private WaitForSeconds _waitForSevenSeconds = new WaitForSeconds(7f);
        private WaitForSeconds _waitForAnimationComplete = new WaitForSeconds(0.4f);

        #region SpawnerBase methods

        protected override void Awake() {
            base.Awake();
            _pool = new UnityPool(_prefab.gameObject, GlobalConstants.RockMaxCapacity, _parent);
        }

        protected override void Spawn() {
            base.Spawn();
            var v_rock = _pool.Get<Rock>(_parent);
            v_rock.transform.localScale = Vector3.zero;
            _spawnedObjectsList.Add(v_rock.gameObject);
            v_rock.transform.position = GetRandomPoint(LayerMask.NameToLayer(GlobalConstants.World) &
                                                       LayerMask.NameToLayer(GlobalConstants.Food));
            v_rock.Initialise();
        }

        #endregion

        #region Unity event methods

        private void Start() {
            StartCoroutine(SpawnRocks());
        }

        #endregion

        private IEnumerator SpawnRocks() {
            while (true) {
                for (var v_index = 0; v_index < GlobalConstants.RockMaxCapacity; v_index++) {
                    Spawn();
                }

                yield return _waitForSevenSeconds;
                _spawnedObjectsList.ForEach(go => LeanTween.scale(go, Vector3.zero, 0.3f).setFrom(Vector3.one));
                yield return _waitForAnimationComplete;
                ReturnAllToPool();
            }
        }
    }
}
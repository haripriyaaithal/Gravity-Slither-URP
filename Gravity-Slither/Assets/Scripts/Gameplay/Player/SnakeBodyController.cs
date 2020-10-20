using System.Collections.Generic;
using System.Linq;
using GS.Common;
using GS.Gameplay.Spawner;
using UnityEngine;

namespace GS.Gameplay.Player {
    public class SnakeBodyController : MonoBehaviour {
        [SerializeField] private GameObject _bodyPrefab;
        [SerializeField] private float _movementSmoothness = 10f;

        private UnityPool _pool;
        private List<Transform> _bodyList;
        private bool _canMove =  true;
        
        #region Unity event methods

        private void Awake() {
            var v_transform = transform;
            _pool = new UnityPool(_bodyPrefab, GlobalConstants.PlayerBodyMaxCapacity, v_transform);
            _bodyList = new List<Transform>(GlobalConstants.PlayerBodyMaxCapacity) {
                GameObject.FindWithTag(GlobalConstants.Player).transform
            };
        }

        private void OnEnable() {
            EventManager.GetInstance().onEatFood += OnEatFood;
            EventManager.GetInstance().onGameOver += OnGameOver;
        }

        private void OnDisable() {
            EventManager.GetInstance().onEatFood -= OnEatFood;
            EventManager.GetInstance().onGameOver -= OnGameOver;
        }

        private void FixedUpdate() {
            if (_canMove) {
                Move();
            }
        }

        #endregion

        private void OnEatFood(Food food) {
            if (_bodyList.Count < GlobalConstants.PlayerBodyMaxCapacity) {
                IncreaseBodyLength();   
            }
        }

        private void IncreaseBodyLength() {
            var v_body = _pool.Get<GameObject>(transform);
            var v_transform = v_body.transform;
            v_transform.position = _bodyList.Last().position;
            v_transform.rotation = _bodyList.Last().rotation;
            _bodyList.Add(v_transform);
        }

        private void Move() {
            for (var v_index = 1; v_index < _bodyList.Count; v_index++) {
                var v_currentSphere = _bodyList[v_index];
                var v_previousSphere = _bodyList[v_index - 1];
                v_currentSphere.position = Vector3.Lerp(v_currentSphere.position, v_previousSphere.position,
                    _movementSmoothness * Time.deltaTime);
                v_currentSphere.rotation = v_previousSphere.rotation;
            }
        }

        private void OnGameOver() {
            _canMove = false;
        }
    }
}
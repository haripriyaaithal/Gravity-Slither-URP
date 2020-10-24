﻿using System;
using UnityEngine;

namespace GS.Gameplay.Inputs {
    public class InputManager : MonoBehaviour {
        private float _x;
        private float _z;
        private Vector3 _movementDirection;

#if UNITY_ANDROID || UNITY_IOS
        private Vector2 _start;
        private Vector2 _end;
        private float _up;
        private float _down;
        private float _right;
        private float _left;
#endif

        private void Update() {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            ProcessPCInput();
#elif UNITY_ANDROID || UNITY_IOS
        // TODO: Check input according to player prefs    
        HandleTouch();
#endif
        }

        public Vector3 GetMovementDirection() {
            // Don't allow movement opposite to current direction
            if (_x != 0) {
                if (Math.Abs(_movementDirection.x - _x * -1) > 0.0001f) {
                    _movementDirection.x = _x;
                    _movementDirection.z = 0;
                } else {
                    _x *= -1;
                }
            } else if (_z != 0) {
                if (Math.Abs(_movementDirection.z - _z * -1) > 0.0001f) {
                    _movementDirection.z = _z;
                    _movementDirection.x = 0f;
                } else {
                    _z *= -1;
                }
            }

            return new Vector3(_x, 0, _z).normalized;
        }


#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL

        private void ProcessPCInput() {
            var v_horizontalAxis = Input.GetAxisRaw("Horizontal");
            var v_verticalAxis = Input.GetAxisRaw("Vertical");
            if (v_horizontalAxis != 0) {
                _x = v_horizontalAxis;
                _z = 0;
            } else if (v_verticalAxis != 0) {
                _z = v_verticalAxis;
                _x = 0;
            }
        }

#elif UNITY_ANDROID || UNITY_IOS
        #region Touch and gesture detection

        private void HandleTouch() {
            if (Input.touchCount > 0) {
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    _start = Input.GetTouch(0).position;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled) {
                    _end = Input.GetTouch(0).position;
                    CalculateGestureDirection();
                }
            }
        }

        private void CalculateGestureDirection() {
            var v_direction = _end - _start;
            v_direction.Normalize();

            _up = Vector2.Dot(v_direction, Vector2.up);
            _down = Vector2.Dot(v_direction, Vector2.down);
            _right = Vector2.Dot(v_direction, Vector2.right);
            _left = Vector2.Dot(v_direction, Vector2.left);

            SetMovementDirection();
        }

        private void SetMovementDirection() {
            if (_up > _down && _up > _left && _up > _right) {
                MoveUp();
            } else if (_down > _up && _down > _left && _down > _right) {
                MoveDown();
            } else if (_left > _up && _left > _down && _left > _right) {
                MoveLeft();
            } else if (_right > _up && _right > _down && _right > _left) {
                MoveRight();
            }
        }
        #endregion
        
        #region On screen navigation
        
        public void MoveUp() {
            _x = 0;
            _z = 1;
        }

        public void MoveDown() {
            _x = 0;
            _z = -1;
        }

        public void MoveLeft() {
            _x = -1;
            _z = 0;
        }

        public void MoveRight() {
            _x = 1;
            _z = 0;
        }
        #endregion
        
#endif
        
    }
}
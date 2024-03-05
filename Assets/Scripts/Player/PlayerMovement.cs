using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        public event Action<Vector3> OnMove;
        
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _rotationSpeed = 15f; 
        [SerializeField] private float _speed = 6f;
        [SerializeField] private float _rotationSmoothness;
        private Vector2 _movement;
        private Transform _transform;

        private void Awake() => _transform = transform;

        private void Update() => Move();

        private void OnMovement(InputValue value) => _movement = value.Get<Vector2>();

        private void Move()
        {
            var moveDirection = new Vector3(_movement.x, 0f, _movement.y);

            moveDirection.y = 0f;

            moveDirection *= _speed * Time.deltaTime;

            OnMove?.Invoke(moveDirection);
            _controller.Move(moveDirection);
            RotatePlayer(moveDirection);
        }

        private void RotatePlayer(Vector3 moveDirection)
        {
            if (moveDirection == Vector3.zero) return;
            
            var newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * _rotationSmoothness);
        }

        public void StopMovement()
        {
            enabled = false;
        }
    }
}
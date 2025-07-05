using System;
using UnityEngine;

namespace TheHollowSpark
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _moveSpeed;
        
        private MainInputSystem _mainInputSystem;
        private Vector2 _moveInput = Vector2.zero;

        private void Awake()
        {
            _mainInputSystem = new MainInputSystem();

            _mainInputSystem.Player.Move.performed += ctx => 
                _moveInput = ctx.ReadValue<Vector2>();
            
            _mainInputSystem.Player.Move.canceled += ctx => 
                _moveInput = Vector2.zero;

        }
        
        private void OnEnable() => _mainInputSystem.Enable();
        
        private void OnDisable() => _mainInputSystem.Disable();

        private void Update()
        {
            Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y).normalized;
            _rigidbody.MovePosition(_rigidbody.position + moveDirection * (_moveSpeed * Time.fixedDeltaTime));
        }
    }
}
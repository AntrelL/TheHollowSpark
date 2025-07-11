using UnityEngine;

namespace TheHollowSpark
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _moveSpeed;
        
        private InputSystem _inputSystem;
        private Vector2 _moveInput = Vector2.zero;

        private void Awake()
        {
            _inputSystem = new InputSystem();

            _inputSystem.Player.Move.performed += ctx => 
                _moveInput = ctx.ReadValue<Vector2>();
            
            _inputSystem.Player.Move.canceled += ctx => 
                _moveInput = Vector2.zero;

        }
        
        private void OnEnable() => _inputSystem.Enable();
        
        private void OnDisable() => _inputSystem.Disable();

        private void Update()
        {
            Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y).normalized;
            _rigidbody.MovePosition(_rigidbody.position + moveDirection * (_moveSpeed * Time.fixedDeltaTime));
        }
    }
}

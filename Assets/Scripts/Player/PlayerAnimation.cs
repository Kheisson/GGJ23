using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(PlayerMovement))]
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private readonly int _speedHash = Animator.StringToHash("Speed");
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        // This method is called by the Input System
        public void OnMove(InputValue inputValue)
        {
            _animator.SetFloat(_speedHash, inputValue.Get<Vector2>().magnitude);
        }
    }
}
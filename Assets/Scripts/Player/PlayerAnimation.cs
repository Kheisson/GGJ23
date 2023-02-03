using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerAnimation : IPlayerComponent
    {
        private readonly Animator _animator;
        private readonly int _speedHash = Animator.StringToHash("Speed");

        public PlayerAnimation(Animator animator)
        {
            _animator = animator;
        }

        // This method is called by the Input System
        public void OnMove(InputValue inputValue)
        {
            _animator.SetFloat(_speedHash, inputValue.Get<Vector2>().magnitude);
        }

        public void OnUpdate()
        {
            // Nothing to do here
        }
    }
}
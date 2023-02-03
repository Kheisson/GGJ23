using System;
using Equipment;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerAnimation : IPlayerComponent
    {
        private readonly Animator _animator;
        private readonly int _speedHash = Animator.StringToHash("Speed");
        private readonly int _fireHash = Animator.StringToHash("Fire");

        public PlayerAnimation(Animator animator)
        {
            _animator = animator;
        }

        // This method is called by the Input System
        public void OnMove(InputValue inputValue)
        {
            _animator.SetFloat(_speedHash, inputValue.Get<Vector2>().magnitude);
        }
        
        public void OnFire(bool isRightHandEmpty, WorkItem workItem)
        {
            if(isRightHandEmpty) return;
            
            switch (workItem.Id)
            {
                case 0:
                    _animator.SetTrigger(_fireHash);
                    break;
                case 1:
                    workItem.EnableEffect();
                    break;
                case 2:
                    break;
            }
        }

        public void OnUpdate()
        {
            // Nothing to do here
        }
    }
}
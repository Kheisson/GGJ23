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
            
            switch (workItem.Type)
            {
                case WorkItem.ItemType.SHOVEL: //Shovel
                    _animator.SetTrigger(_fireHash);
                    break;
                case WorkItem.ItemType.WATERCAN: //Watering Can
                    workItem.EnableEffect();
                    break;
                case WorkItem.ItemType.HANDS: //Hands
                    break;
            }
        }

        public void OnUpdate()
        {
            // Nothing to do here
        }
    }
}
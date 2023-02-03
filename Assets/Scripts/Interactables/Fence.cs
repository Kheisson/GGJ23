using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(Animator))]
    public class Fence : InteractableObject
    {
        private Animator _animator;
        private readonly int _openHash = Animator.StringToHash("Open");

        private void Awake()
        {
            _animator = transform.GetComponent<Animator>();
        }

        public override void Interact()
        {
            _animator.SetBool(_openHash, !_animator.GetBool(_openHash));
        }

        public override bool IsInteractable()
        {
            return true;
        }
    }
}
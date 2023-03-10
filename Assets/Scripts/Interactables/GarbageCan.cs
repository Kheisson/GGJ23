using Equipment;
using HoldableItems;
using UnityEngine;

namespace Interactables
{
    public class GarbageCan : InteractableObject
    {
        private Animator _animator;
        private readonly int _twistHash = Animator.StringToHash("Twist");
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            MeshRenderer = GetComponentInChildren<MeshRenderer>();
            OriginalMaterial = MeshRenderer.material;
            InteractableType = EInteractableType.GarbageCan;
        }
        
        public override void Interact(WorkItem workItem, HoldableItem leftHandItem)
        {
            //Cannot throw trash if the left hand is empty
            if(leftHandItem == null) return;
            
            _animator.SetTrigger(_twistHash);
        }

        public override bool IsInteractable()
        {
            return true;
        }
    }
}
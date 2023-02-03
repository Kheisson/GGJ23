using UnityEngine;

namespace Interactables
{
    public class ToolContainer : InteractableObject
    {
        private Animator _animator;
        private readonly int _rattleHash = Animator.StringToHash("Rattle");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            MeshRenderer = GetComponent<MeshRenderer>();
            OriginalMaterial = MeshRenderer.material;
        }

        public override void Interact()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsInteractable()
        {
            throw new System.NotImplementedException();
        }
    }
}

using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(Animator))]
    public class Fence : InteractableObject
    {
        [SerializeField] private Material highlightMaterial;
        private Material _originalMaterial;
        private Animator _animator;
        private MeshRenderer _meshRenderer;
        private readonly int _openHash = Animator.StringToHash("Open");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _originalMaterial = GetComponent<MeshRenderer>().material;
        }

        public override void Interact()
        {
            _animator.SetBool(_openHash, !_animator.GetBool(_openHash));
        }

        public override bool IsInteractable()
        {
            // If the animation is playing, the fence is not interactable
            return _animator.GetCurrentAnimatorStateInfo(0).length >= 1;
        }

        private void OnCollisionEnter(Collision collision)
        {
            _meshRenderer.material = highlightMaterial;
        }

        private void OnCollisionExit(Collision other)
        {
            _meshRenderer.material = _originalMaterial;
        }
    }
}
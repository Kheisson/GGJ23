using Equipment;
using UnityEngine;

namespace Interactables
{
    public class SeedContainer : InteractableObject
    {
        [SerializeField] private VeggySo veggySo;
        private Animator _animator;
        private readonly int _rattleHash = Animator.StringToHash("Rattle");

        public GameObject VeggyPrefab => veggySo.veggeyPrefab;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            MeshRenderer = GetComponentInChildren<MeshRenderer>();
            OriginalMaterial = MeshRenderer.material;
            InteractableType = EInteractableType.SeedBox;
            GetComponentInChildren<SpriteRenderer>().sprite = veggySo.veggeySprite;
        }
        
        public override void Interact()
        {
            _animator.SetTrigger(_rattleHash);
        }

        public override bool IsInteractable()
        {
            return true;
        }
    }
}

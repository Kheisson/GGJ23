using Equipment;
using HoldableItems;
using UnityEngine;

namespace Interactables
{
    public class SeedContainer : InteractableObject
    {
        [SerializeField] private VeggySo veggySo;
        private Animator _animator;
        private readonly int _rattleHash = Animator.StringToHash("Rattle");

        public VeggySo VeggySo => veggySo;
        public GameObject SeedPrefab => veggySo.seedPrefab;
        public string veggyName => veggySo.veggeyName;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            MeshRenderer = GetComponentInChildren<MeshRenderer>();
            OriginalMaterial = MeshRenderer.material;
            InteractableType = EInteractableType.SeedBox;
            GetComponentInChildren<SpriteRenderer>().sprite = veggySo.veggeySprite;
        }
        
        public override void Interact(WorkItem workItem, HoldableItem leftHandItem)
        {
            //Cannot grab a seed if the left hand is not empty
            Debug.Log("Interacted with seed Container");
            if(leftHandItem != null) return;
            
            _animator.SetTrigger(_rattleHash);
        }

        public override bool IsInteractable()
        {
            return true;
        }
    }
}

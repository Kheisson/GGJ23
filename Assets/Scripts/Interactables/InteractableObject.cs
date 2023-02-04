using Equipment;
using UnityEngine;
using HoldableItems;

namespace Interactables
{
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] protected Material highlightMaterial;
        protected Material OriginalMaterial;
        protected MeshRenderer MeshRenderer;
        protected EInteractableType InteractableType;

        public virtual void Interact(WorkItem workItem, HoldableItem leftHandItem) { }
        public virtual bool IsInteractable() { return true; }
        public GameObject GetGameObject()
        {
            return this.gameObject;
        }
        
        public EInteractableType GetInteractableType()
        {
            return InteractableType;
        }
        
        public void highlightObject()
        {
            MeshRenderer.material = highlightMaterial;
        }

        public void unhighlightObject()
        {
            MeshRenderer.material = OriginalMaterial;
        }
    }
}
using UnityEngine;

namespace Interactables
{
    public abstract class InteractableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] protected Material highlightMaterial;
        protected Material OriginalMaterial;
        protected MeshRenderer MeshRenderer;
        protected EInteractableType InteractableType;

        public abstract void Interact(bool isLeftHandEmpty);
        public abstract bool IsInteractable();
        public GameObject GetGameObject()
        {
            return this.gameObject;
        }
        
        public EInteractableType GetInteractableType()
        {
            return InteractableType;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            MeshRenderer.material = highlightMaterial;
        }

        private void OnCollisionExit(Collision other)
        {
            MeshRenderer.material = OriginalMaterial;
        }
    }
}
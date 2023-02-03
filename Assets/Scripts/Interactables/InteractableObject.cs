using UnityEngine;

namespace Interactables
{
    public abstract class InteractableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] protected Material highlightMaterial;
        protected Material OriginalMaterial;
        protected MeshRenderer MeshRenderer;
        
        public abstract void Interact();
        public abstract bool IsInteractable();
        public GameObject GetGameObject()
        {
            return this.gameObject;
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
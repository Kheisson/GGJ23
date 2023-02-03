using Interactables;
using UnityEngine;

namespace Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        [SerializeField] private float interactionRange = 1f;
        [SerializeField] private LayerMask interactableLayer;
        private Transform _transform;
        private IInteractable _currentInteractable;

        private void Awake()
        {
            _transform = transform;
        }

        // This method is called by the Input System
        public void OnInteract()
        {
            Debug.Log("Interact");

            if (!Physics.Raycast(_transform.position, _transform.forward, out RaycastHit hitInfo, interactionRange, interactableLayer))
            {
                return;
            }

            Debug.Log(hitInfo.transform.name);
            _currentInteractable = hitInfo.collider.GetComponent<IInteractable>();
            _currentInteractable.Interact();
        }
    }
}

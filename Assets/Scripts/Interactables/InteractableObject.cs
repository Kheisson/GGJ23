using UnityEngine;

namespace Interactables
{
    public abstract class InteractableObject : MonoBehaviour, IInteractable
    {
        public abstract void Interact();

        public abstract bool IsInteractable();
    }
}
using UnityEngine;

namespace Interactables
{
    public interface IInteractable
    {
        void Interact();
        bool IsInteractable();
        GameObject GetGameObject();
    }
}
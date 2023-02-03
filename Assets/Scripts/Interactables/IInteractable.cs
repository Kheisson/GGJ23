using UnityEngine;

namespace Interactables
{
    public interface IInteractable
    {
        void Interact(bool isLeftHandEmpty);
        bool IsInteractable();
        GameObject GetGameObject();
        EInteractableType GetInteractableType();
    }
}
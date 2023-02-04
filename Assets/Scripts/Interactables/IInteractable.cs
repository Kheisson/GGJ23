using Equipment;
using UnityEngine;

namespace Interactables
{
    public interface IInteractable
    {
        void Interact(WorkItem workItem, bool isLeftHandEmpty, string seedHeldName);
        bool IsInteractable();
        GameObject GetGameObject();
        EInteractableType GetInteractableType();
    }
}
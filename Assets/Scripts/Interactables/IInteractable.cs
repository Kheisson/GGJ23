using Equipment;
using HoldableItems;
using UnityEngine;

namespace Interactables
{
    public interface IInteractable
    {
        void Interact(WorkItem workItem, HoldableItem leftHandItem);
        bool IsInteractable();
        GameObject GetGameObject();
        EInteractableType GetInteractableType();
    }
}
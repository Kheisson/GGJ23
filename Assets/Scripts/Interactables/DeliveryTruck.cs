using Equipment;
using HoldableItems;
using Interactables;
using Managers;
using UI;
using UnityEngine;

public class DeliveryTruck : InteractableObject
{
    private OrderManager _orderManager;

    private void Awake()
    {
        MeshRenderer = transform.GetChild(0).GetComponentInChildren<MeshRenderer>();
        OriginalMaterial = MeshRenderer.material;
        InteractableType = EInteractableType.DeliveryTruck;
        GameManager.Instance.OnUiLoadedEvent += OnUiLoaded;
    }

    private void OnUiLoaded(UiManager obj)
    {
        _orderManager = FindObjectOfType<OrderManager>();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnUiLoadedEvent -= OnUiLoaded;
    }

    public override bool IsInteractable()
    {
        return _orderManager.HasOrdersOnScreen();
    }

    public override void Interact(WorkItem workItem, HoldableItem leftHandItem)
    {
        if (leftHandItem == null)
        {
            Debug.Log("Left hand is empty");

            return;
        }
        
        _orderManager.TryCompleteOrder(leftHandItem.CurrentVeggy);
        Debug.Log("Interacting with delivery truck");
    }
}

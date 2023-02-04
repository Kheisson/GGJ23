using Equipment;
using HoldableItems;
using Interactables;
using Managers;
using UI;
using UnityEngine;

public class DeliveryTruck : InteractableObject
{
    [SerializeField] private GameObject[] barrels;
    private OrderManager _orderManager;
    private HoldableItem _holdableItem;

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
        
        _holdableItem = leftHandItem;
        _orderManager.TryCompleteOrder(leftHandItem.CurrentVeggy, OrderCompleted);
        Debug.Log("Interacting with delivery truck");
    }

    private void OrderCompleted(bool success)
    {
        if (!success)
        {
            Debug.Log("Item not accepted");
            return;
        }
        
        //check if there is a barrel that's inactive and activate it
        foreach (var barrel in barrels)
        {
            if (!barrel.activeSelf)
            {
                barrel.SetActive(true);
                break;
            }
        }
        
        Debug.Log("Order completed");
        Destroy(_holdableItem.gameObject);
    }
}

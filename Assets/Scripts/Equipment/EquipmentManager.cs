using HoldableItems;
using Interactables;
using Managers;
using Player;
using System;
using UI;
using UnityEngine;

namespace Equipment
{
    public class EquipmentManager : MonoBehaviour
    {
        [SerializeField] private Transform LeftHand;
        [SerializeField] private Transform RightHand;
        [SerializeField] private PlayerContainer playerContainer;
        [SerializeField] private WorkItem[] workItems;

        public event Action<HoldableItem> itemPickUp;
        public event Action seedDestroyed;

        private WorkItem _currentWorkItem;
        public HoldableItem ItemInLeftHand => LeftHand.GetComponentInChildren<HoldableItem>();
        public bool IsRightHandEmpty => RightHand.childCount == 0;
        
        public WorkItem CurrentWorkItem => _currentWorkItem;

        private void Start()
        {
            GameManager.Instance.OnUiLoadedEvent += RegisterListeners;
            playerContainer.AddInteractListener(onInteract);
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnUiLoadedEvent -= RegisterListeners;
            playerContainer.RemoveInteractListener(onInteract);
        }
        
        private void onInteract(IInteractable interactable)
        {
            switch (interactable.GetInteractableType())
            {
                case EInteractableType.SeedBox:
                    EquipSeedBox(interactable.GetGameObject());
                    break;
                case EInteractableType.GarbageCan:
                    DestroyItemInLeftHand();
                    break;
                case EInteractableType.Vegetable:
                    EquipVegetable(interactable.GetGameObject());
                    break;
            }
        }
        
        private void EquipSeedBox(GameObject item)
        {
            if (ItemInLeftHand != null)
            {
                Debug.Log("Left hand is full");
                return;
            }

            var seedContainer = item.GetComponent<SeedContainer>();
            var seed = Instantiate(seedContainer.SeedPrefab, LeftHand, true);
            ItemInLeftHand.CurrentVeggy = seedContainer.VeggySo;
            ResetPositionAndRotation(seed.transform);
            itemPickUp?.Invoke(ItemInLeftHand);
        }

        private void EquipVegetable(GameObject vegetable)
        {
            var land = vegetable.GetComponent<LandBlock>();
            if (land == null || LeftHand.childCount != 0) return;

            var veggie = Instantiate(land.CropObject, LeftHand, true);
            ItemInLeftHand.CurrentVeggy = land.CurrentVeggyOnLand;
            ResetPositionAndRotation(veggie.transform);
            Destroy(land.CropObject);
            itemPickUp?.Invoke(ItemInLeftHand);
        }
        
        private void EquipWorkItem(int index)
        {
            _currentWorkItem = workItems[index];
            
            if(!IsRightHandEmpty) Destroy(RightHand.GetChild(0).gameObject);
            
            var item = Instantiate(_currentWorkItem, RightHand, true).GetComponent<WorkItem>();
            ResetPositionAndRotation(item.transform);
            item.transform.localRotation = Quaternion.Euler(-90, 0, 90);
            item.transform.localPosition = new Vector3(-0.2f, 0f, 0f);
            _currentWorkItem = item;
            
        }
        
        private void ResetPositionAndRotation(Transform itemTransform)
        {
            itemTransform.localPosition = Vector3.zero;
            itemTransform.localRotation = Quaternion.identity;
        }
        
        public void DestroyItemInLeftHand()
        {
            if (ItemInLeftHand == null) return;
            if(ItemInLeftHand.Type == HoldableItem.ItemType.SEED)
            {
                seedDestroyed?.Invoke();
            }
            Destroy(LeftHand.GetChild(0).gameObject);
        }
        
        private void RegisterListeners(UiManager uiManager)
        {
            uiManager.OnItemSelectedEvent += EquipWorkItem;
        }

        public WorkItem getCurrentWorkItem() { return CurrentWorkItem; }

        public Transform getLeftHandPos() { return LeftHand; }
    }
}
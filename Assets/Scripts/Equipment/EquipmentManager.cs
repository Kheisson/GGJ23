using HoldableItems;
using Interactables;
using Managers;
using Player;
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
                //case EInteractableType.LandVegitable:
                //    EquipLandVegtiable(interactable.GetGameObject());
                //    break;
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
        }

        //private void EquipLandVegtiable(GameObject landVeggy)
        //{
        //    if (ItemInLeftHand != null)
        //    {
        //        Debug.Log("Left hand is full");
        //        return;
        //    }
        //    GameObject vegitable = landVeggy.transform.GetChild(3).GetChild(0).gameObject;
        //    vegitable.transform.parent = LeftHand;
        //    ResetPositionAndRotation(vegitable.transform);
        //}
        private void EquipWorkItem(int index)
        {
            _currentWorkItem = workItems[index];
            
            if(!IsRightHandEmpty) Destroy(RightHand.GetChild(0).gameObject);
            
            var item = Instantiate(_currentWorkItem, RightHand, true).GetComponent<WorkItem>();
            ResetPositionAndRotation(item.transform);
            item.transform.localRotation = Quaternion.Euler(-90, 0, 90);
            _currentWorkItem = item;
            
        }
        
        private void ResetPositionAndRotation(Transform itemTransform)
        {
            itemTransform.localPosition = Vector3.zero;
            itemTransform.localRotation = Quaternion.identity;
        }
        
        private void DestroyItemInLeftHand()
        {
            if (ItemInLeftHand == null) return;
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
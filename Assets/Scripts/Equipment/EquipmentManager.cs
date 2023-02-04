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
        private string seedHeldName;
        public bool IsLeftHandEmpty => LeftHand.childCount == 0;
        public bool IsRightHandEmpty => RightHand.childCount == 0;
        
        public WorkItem CurrentWorkItem => _currentWorkItem;

        private void Start()
        {
            GameManager.Instance.OnUiLoadedEvent += RegisterListeners;
            playerContainer.AddInteractListener(onInteract);
        }

        private void OnDestroy()
        {
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
            }
        }
        
        private void EquipSeedBox(GameObject item)
        {
            if (!IsLeftHandEmpty)
            {
                Debug.Log("Left hand is full");
                return;
            }
            
            var seedContainer = item.GetComponent<SeedContainer>();
            seedHeldName = seedContainer.veggyName;
            var seedBox = Instantiate(seedContainer.VeggyPrefab, LeftHand, true);
            ResetPositionAndRotation(seedBox.transform);
        }

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
            if (IsLeftHandEmpty) return;
            seedHeldName = null;
            Destroy(LeftHand.GetChild(0).gameObject);
        }
        
        private void RegisterListeners(UiManager uiManager)
        {
            uiManager.OnItemSelectedEvent += EquipWorkItem;
        }

        public string getSeedHeldName()
        {
            return seedHeldName;
        }

        public WorkItem getCurrentWorkItem() { return CurrentWorkItem; }
    }
}
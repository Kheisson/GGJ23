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
        public bool IsLeftHandEmpty => LeftHand.childCount == 0;
        public bool IsRightHandEmpty => RightHand.childCount == 0;
        
        public WorkItem CurrentWorkItem => _currentWorkItem;

        private void Start()
        {
            GameManager.Instance.OnUiLoadedEvent += RegisterListeners;
            playerContainer.AddInteractListener(EquipItem);
        }

        private void OnDestroy()
        {
            playerContainer.RemoveInteractListener(EquipItem);
        }
        
        private void EquipItem(IInteractable interactable)
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
            if (IsLeftHandEmpty)
            {
                var seedBox = Instantiate(item.GetComponent<SeedContainer>().VeggyPrefab, LeftHand, true);
                ResetPositionAndRotation(seedBox.transform);
            }
            Debug.Log("Left hand is full");
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
            Destroy(LeftHand.GetChild(0).gameObject);
        }
        
        private void RegisterListeners(UiManager uiManager)
        {
            uiManager.OnItemSelectedEvent += EquipWorkItem;
        }
    }
}
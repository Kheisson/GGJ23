using Interactables;
using Player;
using UnityEngine;

namespace Equipment
{
    public class EquipmentManager : MonoBehaviour
    {
        [SerializeField] private Transform LeftHand;
        [SerializeField] private Transform RightHand;
        [SerializeField] private PlayerContainer playerContainer;
        
        public bool IsLeftHandEmpty => LeftHand.childCount == 0;
        public bool IsRightHandEmpty => RightHand.childCount == 0;

        private void Start()
        {
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
            }
        }
        
        private void EquipSeedBox(GameObject item)
        {
            if (IsLeftHandEmpty)
            {
                var seedBox = Instantiate(item.GetComponent<SeedContainer>().VeggyPrefab, LeftHand, true);
                seedBox.transform.localPosition = Vector3.zero;
                seedBox.transform.localRotation = Quaternion.identity;
            }
            Debug.Log("Left hand is full");
        }
    }
}
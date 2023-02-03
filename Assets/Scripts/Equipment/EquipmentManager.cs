using System;
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

        private void OnEnable()
        {
            playerContainer.AddInteractListener(EquipItem);
        }

        private void OnDisable()
        {
            playerContainer.RemoveInteractListener(EquipItem);
        }
        
        private void EquipItem(IInteractable interactable)
        {
            var item = interactable.GetGameObject();
            //item.transform.SetParent(hand);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
        }
    }
}
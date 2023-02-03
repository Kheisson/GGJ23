using System;
using Interactables;
using UnityEngine;

namespace Player
{
    public class PlayerInteractions : IPlayerComponent
    {
        private const float INTERACTION_RANGE = 1f;
        private readonly LayerMask _interactableLayer;
        private readonly Player _player;
        private IInteractable _currentInteractable;

        public event Action<IInteractable> OnInteractEvent;

        public PlayerInteractions(Player player, LayerMask interactableLayer)
        {
            _interactableLayer = interactableLayer;
            _player = player;
        }


        public void Interact()
        {

            if (!Physics.Raycast(_player.PlayerTransform.position, _player.PlayerTransform.forward, out RaycastHit hitInfo, INTERACTION_RANGE, _interactableLayer))
            {
                return;
            }

            _currentInteractable = hitInfo.collider.GetComponent<IInteractable>();

            if (_currentInteractable.IsInteractable())
            {
                _currentInteractable.Interact();
                OnInteractEvent?.Invoke(_currentInteractable);
            }
        }

        public void OnUpdate()
        {
            //nothing to do here
        }
    }
}

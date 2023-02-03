using System;
using Interactables;
using UnityEngine;

namespace Player
{
    public class PlayerInteractions : IPlayerComponent
    {
        private const float INTERACTION_RANGE = 1f;
        private readonly LayerMask _interactableLayer;
        private readonly PlayerContainer _playerContainer;
        private IInteractable _currentInteractable;
        private RaycastHit _selectedInteractable;
        private PlayerSelectorBox playerSelectorBox;

        public event Action<IInteractable> OnInteractEvent;

        public PlayerInteractions(PlayerContainer playerContainer, LayerMask interactableLayer, PlayerSelectorBox playerSelectorBox)
        {
            _interactableLayer = interactableLayer;
            _playerContainer = playerContainer;
            this.playerSelectorBox = playerSelectorBox;
        }


        public void Interact(bool isLeftHandEmpty)
        {
            if (playerSelectorBox.getCollidingObject() == null) return;
            
            _currentInteractable = playerSelectorBox.getCollidingObject().GetComponent<IInteractable>();
            
            if (_currentInteractable.IsInteractable())
            {
                _currentInteractable.Interact(isLeftHandEmpty);
                OnInteractEvent?.Invoke(_currentInteractable);
            }
        }

        public void OnUpdate()
        {
            if (!Physics.Raycast(_playerContainer.PlayerTransform.position, _playerContainer.PlayerTransform.forward, out _selectedInteractable, INTERACTION_RANGE, _interactableLayer))
            {
                return;
            }
        }
    }
}

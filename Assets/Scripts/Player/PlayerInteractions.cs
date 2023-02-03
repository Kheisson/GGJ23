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

        public event Action<IInteractable> OnInteractEvent;

        public PlayerInteractions(PlayerContainer playerContainer, LayerMask interactableLayer)
        {
            _interactableLayer = interactableLayer;
            _playerContainer = playerContainer;
        }


        public void Interact()
        {
            if (_selectedInteractable.collider == null) return;
            
            _currentInteractable = _selectedInteractable.collider.GetComponent<IInteractable>();
            
            if (_currentInteractable.IsInteractable())
            {
                _currentInteractable.Interact();
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

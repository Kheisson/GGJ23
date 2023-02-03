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
        private RaycastHit _selectedInteractable;

        public event Action<IInteractable> OnInteractEvent;

        public PlayerInteractions(Player player, LayerMask interactableLayer)
        {
            _interactableLayer = interactableLayer;
            _player = player;
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
            if (!Physics.Raycast(_player.PlayerTransform.position, _player.PlayerTransform.forward, out _selectedInteractable, INTERACTION_RANGE, _interactableLayer))
            {
                return;
            }
        }
    }
}

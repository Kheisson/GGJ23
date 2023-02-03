using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private LayerMask interactableLayer;
        
        private PlayerMovement _playerMovement;
        private PlayerAnimation _playerAnimation;
        private PlayerInteractions _playerInteractions;
        private Transform _transform;
        private IPlayerComponent[] _playerComponents;
        
        public Transform PlayerTransform => _transform;
        
        private void Awake()
        {
            _transform = transform;
            _playerMovement = new PlayerMovement(this);
            _playerAnimation = new PlayerAnimation(playerAnimator);
            _playerInteractions = new PlayerInteractions(this, interactableLayer);
            
            _playerComponents = new IPlayerComponent[]
            {
                _playerMovement,
                _playerAnimation,
                _playerInteractions
            };
        }
        
        private void Update()
        {
            foreach (var playerComponent in _playerComponents)
            {
                playerComponent.OnUpdate();
            }
        }

        private void OnMove(InputValue value)
        {
            _playerMovement.SetMovement(value.Get<Vector2>());
            _playerAnimation.OnMove(value);
        }
        
        private void OnInteract()
        {
            _playerInteractions.Interact();
        }
        
    }
}
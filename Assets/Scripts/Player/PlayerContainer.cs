using System;
using Equipment;
using Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerContainer : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private LayerMask interactableLayer;
        
        private PlayerMovement _playerMovement;
        private PlayerAnimation _playerAnimation;
        private PlayerInteractions _playerInteractions;
        private Transform _transform;
        private IPlayerComponent[] _playerComponents;
        private EquipmentManager _equipmentManager;
        private ProjectCucamba _projectCucamba;

        public Transform PlayerTransform => _transform;
        
        
        private void Awake()
        {
            _transform = transform;
            
            //Movement by Input System C# class.
            _projectCucamba = new ProjectCucamba();
            _projectCucamba.Player.Enable();
            _projectCucamba.Player.Fire.performed += ctx => Fire();
            
            _equipmentManager = GetComponent<EquipmentManager>();
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
            _playerInteractions.Interact(_equipmentManager.IsLeftHandEmpty);
            _playerAnimation.OnFire(_equipmentManager.IsRightHandEmpty, _equipmentManager.CurrentWorkItem);
        }

        
        private void Fire()
        {
            //_playerAnimation.OnFire(_equipmentManager.IsRightHandEmpty);
        }

        public void AddInteractListener(Action<IInteractable> action)
        {
            _playerInteractions.OnInteractEvent += action;
        }
        
        public void RemoveInteractListener(Action<IInteractable> action)
        {
            _playerInteractions.OnInteractEvent -= action;
        }


        #region Debug

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * 1f);
        }
        #endif

        #endregion
        
    }
}
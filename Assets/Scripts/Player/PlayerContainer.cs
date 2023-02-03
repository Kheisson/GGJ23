using System;
using Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerContainer : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField] private Grid grid;
        [SerializeField] private GameObject playerSelectorBoxPrefab;

        private PlayerMovement _playerMovement;
        private PlayerAnimation _playerAnimation;
        public PlayerInteractions _playerInteractions;
        private Transform _transform;
        private IPlayerComponent[] _playerComponents;
        private Vector3Int interactingCell;
        private Vector3 gridOffset;
        private GameObject playerSelectorBox;

        public Transform PlayerTransform => _transform;
        
        private void Awake()
        {
            _transform = transform;
            _playerMovement = new PlayerMovement(this);
            _playerAnimation = new PlayerAnimation(playerAnimator);
            playerSelectorBox = Instantiate(playerSelectorBoxPrefab, grid.CellToWorld(interactingCell) + gridOffset, Quaternion.identity);
            gridOffset = new Vector3(grid.cellSize.x / 2, 0, -grid.cellSize.y / 2);
            interactingCell = grid.WorldToCell(transform.position + transform.forward);
            _playerInteractions = new PlayerInteractions(this, interactableLayer, playerSelectorBox.GetComponent<PlayerSelectorBox>());
            
            
            
            _playerComponents = new IPlayerComponent[]
            {
                _playerMovement,
                _playerAnimation,
                _playerInteractions
            };
        }
        
        private void Update()
        {
            interactingCell = grid.WorldToCell(transform.position + transform.forward);
            playerSelectorBox.transform.position = grid.CellToWorld(interactingCell) + gridOffset;
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
using UnityEngine;

namespace Player
{
    public class PlayerMovement : IPlayerComponent
    {
        private const float MOVEMENT_SPEED = 5f;
        private const float ROTATION_SPEED = 10f;
        private Vector2 _movement;
        private readonly PlayerContainer _playerContainer;

        public PlayerMovement(PlayerContainer playerContainer)
        {
            _playerContainer = playerContainer;
        }

        public void OnUpdate()
        {
            HandleMovementAndRotation();
        }

        private void HandleMovementAndRotation()
        {
            var movementTransform = new Vector3(_movement.x, 0, _movement.y);

            if (movementTransform.magnitude == 0) return;
            
            _playerContainer.PlayerTransform.position += movementTransform * (Time.deltaTime * MOVEMENT_SPEED);
            _playerContainer.PlayerTransform.forward = Vector3.Slerp(_playerContainer.transform.forward, movementTransform, Time.deltaTime * ROTATION_SPEED);
        }

        public void SetMovement(Vector2 value)
        {
            _movement = value;
        }
    }
}

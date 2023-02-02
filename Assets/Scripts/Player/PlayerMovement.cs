using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;
        private Transform _transform;
        private Vector2 _movement;
    
        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            HandleMovementAndRotation();
        }

        private void HandleMovementAndRotation()
        {
            var movementTransform = new Vector3(_movement.x, 0, _movement.y);

            if (movementTransform.magnitude == 0) return;
            
            _transform.position += movementTransform * (Time.deltaTime * movementSpeed);
            _transform.forward = Vector3.Slerp(_transform.forward, movementTransform, Time.deltaTime * rotationSpeed);
        }

        // This method is called by the Input System 
        public void OnMove(InputValue value)
        {
            _movement = value.Get<Vector2>();
        }
        
        #region Debug
#if UNITY_EDITOR
        
        private void OnDrawGizmos()
        {
            //draws a sphere in front of the player
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.forward, 0.5f);
        }
        
#endif
        #endregion
    }
}

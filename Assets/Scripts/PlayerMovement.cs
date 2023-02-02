using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Transform _transform;
    private Vector2 _movement;
    
    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.position += new Vector3(_movement.x, 0, _movement.y) * Time.deltaTime;
    }

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
    }
    
}

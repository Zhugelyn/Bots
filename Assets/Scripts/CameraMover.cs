using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    
    private CameraActions _cameraActions;
    private Vector2 _movement;

    private void Awake()
    {
        _cameraActions = new CameraActions();
    }

    private void OnEnable()
    {
        _cameraActions.Enable();
        _cameraActions.Move.XZ.performed += OnIputMove;
        _cameraActions.Move.XZ.canceled += OnIputMove;
    }

    private void OnDisable()
    {
        _cameraActions.Move.XZ.performed -= OnIputMove;
        _cameraActions.Move.XZ.canceled -= OnIputMove;
        _cameraActions.Disable();
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 direction = new Vector3(_movement.x, 0, _movement.y);
        transform.position += direction * _speed * Time.deltaTime;
    }

    public void OnIputMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }
}

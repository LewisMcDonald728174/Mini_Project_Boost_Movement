using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float rotationSpeed = 500f;
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    private Camera _mainCamera;

    [Header("Gravity")]
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _gravity = -9.81f;
    private float _velocity;

    [Header("Jump")]
    [SerializeField] private float jumpPower;

    [Header("Boost")]
    [SerializeField] private BoostControl boost;

    [Header("Energy")]
    [SerializeField] private Energy energy;
    [SerializeField] private float sprintEnergyCost;



    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        Debug.Log(energy.HasEnergy(sprintEnergyCost));
    }
    void Update()
    {
        ApplyRotation();
        ApplyGravity();
        ApplyMovement();
    }

    public void Movement(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }

    private void ApplyMovement()
    {
        var targetSpeed = boost.isSprinting ? boost.speed * boost.boostMultiplier : boost.speed;
        boost.currentSpeed = Mathf.MoveTowards(boost.currentSpeed, targetSpeed, boost.acceleration * Time.deltaTime);

        _characterController.Move(_direction * boost.currentSpeed * Time.deltaTime);
    }

    private void ApplyRotation()
    {
        if (_input.sqrMagnitude == 0) return;

        _direction = Quaternion.Euler(0.0f, _mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(_input.x, 0.0f, _input.y);
        var targetRotation = Quaternion.LookRotation(_direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (IsGrounded() && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }

        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }

        _direction.y = _velocity;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!IsGrounded()) return;

        _velocity += jumpPower;
    }

    private bool IsGrounded() => _characterController.isGrounded;


    public void Boost(InputAction.CallbackContext context)
    {
        if (energy.HasEnergy(sprintEnergyCost) == true)
        {
            boost.isSprinting = context.started || context.performed;
        }
        else
        {
            Debug.Log("No Energy");
        }
    }

    [Serializable]
    public struct BoostControl
    {
        public float speed;
        public float boostMultiplier;
        public float acceleration;

        [HideInInspector] public bool isSprinting;
        [HideInInspector] public float currentSpeed;
    }
}
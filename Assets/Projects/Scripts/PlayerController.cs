using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static UnityEditor.Timeline.TimelinePlaybackControls;


public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float currentSpeed;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 25f;
    [SerializeField] private float jumpForce = 5f;
    Rigidbody rb;

    [Header("Jump Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] bool isGrounded;

    [Header("Boost")]
    [HideInInspector] public bool isSprinting;

    [Header("Energy")]
    [SerializeField] private Energy energy;
    [SerializeField] private float sprintEnergyCost;
    [SerializeField] private float boostSpeed = 14f;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentSpeed = moveSpeed;

        Debug.Log(energy.HasEnergy(sprintEnergyCost));
        currentSpeed = moveSpeed;
    }
    void Update()
    {
        Movement();
        Jump();

        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, (int)whatIsGround);
        Debug.Log(isGrounded);
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.D))
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.forward * -currentSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
            Boost();
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
            rb.AddForce(Vector3.up * jumpForce);
    }

    void Boost()
    {
        if(energy.HasEnergy(sprintEnergyCost))
        {
            currentSpeed = boostSpeed;
            energy.ConsumeEnergy(sprintEnergyCost);
        }

        else
        {
            currentSpeed = moveSpeed;
        }
    }
}
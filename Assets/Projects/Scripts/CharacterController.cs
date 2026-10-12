using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    Rigidbody rb;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] bool isGrounded;

    private float currentEnergy = 100f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
            rb.AddForce(Vector3.up * jumpForce);
    }

    void CheckBoost()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentEnergy)

    }
}

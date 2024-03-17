using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController cc;
    [SerializeField] private float Velocidad = 12;

    private float Gravedad = -9.81f;
    private Vector3 velocity;

    [SerializeField] private Transform groundCheck;
    private float groundDistance = 1.4f;
    [SerializeField] private LayerMask floorMask;
    private bool isGrounded;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, floorMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(3 * -2 * Gravedad);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        cc.Move(move * Velocidad * Time.deltaTime);


        velocity.y += Gravedad * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed, jumpHeight, groundDistance;
    float jumpCount;
    bool isGrounded;
    public Transform rayOrigin;
    public LayerMask groundMask;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float jump = Input.GetAxisRaw("Jump");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }

        if (rb.velocity.y <= 0.1f && !isGrounded)
        {
            rb.AddForce(-direction * moveSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            rb.AddForce(Vector3.up * jumpHeight * Time.deltaTime);
            jumpCount++;
        }
        else if (isGrounded && jumpCount >= 2)
        {
            jumpCount = 0;
        }

        isGrounded = Physics.Raycast(rayOrigin.position, Vector3.down, groundDistance, groundMask);
        

    }
}

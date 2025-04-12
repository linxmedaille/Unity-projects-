using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed;
    public float jumpHeight;
    private bool isHorisontalPressed = false;
    private bool isJumpPressed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        isHorisontalPressed = Input.GetButton("Horizontal");
        isJumpPressed = Input.GetButton("Jump");
    }
    private void FixedUpdate()
    {
        moveLeft();
    }

    public void moveLeft()
    {
        if (isHorisontalPressed)
        {
            rb.linearVelocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0, 0);
        }

    }

public void jump()
    {
        if (isJumpPressed)
        { 
            rb.linearVelocity = new Vector3(0,0, Input.GetAxis("Jump") * moveSpeed);
        }

    }
}

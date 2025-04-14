using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed;
    public float jumpHeight;
    private bool isHorisontalPressed = false;
    private bool isJumpPressed = false;
    private bool isInTheAir = false;
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
        jump();
    }

    public void moveLeft()
    {
        if (isHorisontalPressed)
        {
            Vector3 currentVelocity = rb.linearVelocity;
            rb.linearVelocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, currentVelocity.y, 0);

        }

    }

    public void jump()
    {
        if (isJumpPressed && !isInTheAir)
        {
            Vector3 currentVelocity = rb.linearVelocity;
            rb.AddForce(new Vector3(currentVelocity.x, jumpHeight, 0), ForceMode.Impulse);
            isInTheAir = true;
        }

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (isInTheAir == true)
        {
            isInTheAir = false;
            rb.linearVelocity = Vector3.zero;
        }
    }
}

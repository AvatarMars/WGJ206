using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Camera cam;

    public float speed;
    public float jumpForce;
    private float moveInput;

    Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform heldItem, groundCheck;
    public float checkRadius;
    public LayerMask groundMask;

    public int extraJumps;
    public int extraJumpValue;

    public float jumpTime;
    private float jumpTimeCounter;

    bool isJumping;

    private float rot;

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpValue;
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0 && isJumping == false)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            extraJumps -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps <= 0 && isGrounded == true && isJumping == false)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }

        if(Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundMask);

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

        Vector3 dir = cam.ScreenToWorldPoint(Input.mousePosition) - heldItem.transform.position;
        dir.Normalize();
        rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        heldItem.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}

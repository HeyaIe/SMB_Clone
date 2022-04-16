using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneMovement : MonoBehaviour
{
    public float movespeed = 6f;
    public float jumpForce = 30f;
    public float jumpTime; // How long can the player hold jump for?
    public LayerMask whatIsGround;
    public Transform feetPos;
    public float checkRadius;

    private Animator anim;
    private bool facingRight = true;
    private bool isGrounded = false;
    private bool isJumping;
    private float jumpTimeCounter;
    private Rigidbody2D rb2D; // Rigidbody2D
    private Vector2 direction; // Store player's input horizontal and vertical
    private M_AudioManager audioM;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        audioM = FindObjectOfType<M_AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Check if character is grounded
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            audioM.Play("Jump");
            jumpTimeCounter = jumpTime;
            rb2D.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("jump");
        }
        if (Input.GetButton("Jump"))
        {
            if (jumpTimeCounter > 0)
            {
                audioM.Play("Jump");
                rb2D.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
                anim.SetTrigger("jump");
            }
            else
            {
                isJumping = false;
            }
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        if (direction.x != 0)
        {
            // Move character
            rb2D.velocity = new Vector2(direction.x * movespeed, rb2D.velocity.y);
            anim.SetBool("isRunning", true);

            // Change character direction
            if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
            {
                Flip();
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    // Rotate character's direction
    public void Flip()
    {
        facingRight = !facingRight;
        // Sets the y axis values in degrees around the x axis, if facingRight false, rotate 180 degrees
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }
}

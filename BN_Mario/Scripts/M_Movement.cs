using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Movement : MonoBehaviour
{
    public float bounceImpact = 15f;
    public float movespeed = 6f;
    public float jumpForce = 30f;
    public float checkRadius;
    public float jumpTime; // How long can the player hold jump for?
    public LayerMask whatIsGround;
    public Transform feetPos;
    public bool canMove;

    private float autoSpeed = 20f;
    private Vector3 castlePos;
    private bool hasGottenHit;
    private GameObject gameManager;
    private M_GameManager gameManagerScript;
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
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<M_GameManager>();
        canMove = true;
        castlePos = GameObject.Find("Castle").GetComponentInChildren<Transform>().position;
        gameObject.SetActive(true);
        audioM = FindObjectOfType<M_AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Check if character is grounded
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (Input.GetButtonDown("Jump") && isGrounded && canMove)
        {
            isJumping = true;
            audioM.Play("Jump");
            jumpTimeCounter = jumpTime;
            rb2D.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("jump");
        }
        if (Input.GetButton("Jump") && canMove)
        {
            if(jumpTimeCounter > 0)
            {
                rb2D.velocity = Vector2.up * jumpForce;
                audioM.Play("Jump");
                jumpTimeCounter -= Time.deltaTime;
                anim.SetTrigger("jump");
            }
            else
            {
                isJumping = false;
            }
        }

        // Set isJumping to false when player releases jump button
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        if (canMove && direction.x != 0)
        {
            Vector3 targetVelocity = new Vector2(movespeed * 10f, rb2D.velocity.y);
            Vector3 mVelocity = Vector3.zero;

            // Move character
            rb2D.velocity = new Vector3(direction.x * movespeed, rb2D.velocity.y);
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
        // Sets the y axis values in degrees around the y axis, if facingRight false, rotate 180 degrees
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "StompPoint")
        {
            Debug.Log("Stomp");
            audioM.Play("Stomp");

            rb2D.AddForce(Vector3.up * bounceImpact);
        }
        // Decrement lives
        else if (collision.collider.tag == "Goomba" && gameManagerScript.GetLivesLeft() > 0 && !hasGottenHit)
        {
            audioM.Stop("MainTheme");
            Debug.Log("I got hit!");
            canMove = false;
            hasGottenHit = true;
            audioM.Play("MarioDeath");
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX; // Freeze player's x pos
            GameObject.Find("Main Camera").GetComponent<CameraFollow>().enabled = false; // Disable camera
            rb2D.AddForce(this.transform.up * 1500f, ForceMode2D.Force); // Add force to player's y axis
            this.GetComponent<CapsuleCollider2D>().isTrigger = true; // Disable collisions
            anim.SetTrigger("dead"); // Trigger death animation
            gameManagerScript.isTimerPaused = true; // Pause timer
            FindObjectOfType<M_GameManager>().Invoke("RestartOrEndGame", 3f);
        }
    }

    // Automatically walk Mario toward the castle
    public IEnumerator AutoWalk()
    {
        float elapsedTime = 0;
        
        while (elapsedTime < 5f)
        {
            anim.SetBool("isRunning", true);
            // Move player toward the castle entrance in x seconds
            rb2D.velocity = (castlePos - transform.position).normalized * autoSpeed;
            elapsedTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        anim.SetBool("isRunning", false);
        gameObject.SetActive(false); // Make Mario disappear
        gameManagerScript.GameWon();
    }
}

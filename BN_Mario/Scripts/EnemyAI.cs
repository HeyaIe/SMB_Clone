using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private bool facingRight;
    private Rigidbody2D rb2D;

    [SerializeField]
    private float goombaSpeed = 8f; // Enemy speed

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Goomba movement
        rb2D.velocity = new Vector2(goombaSpeed * Time.fixedDeltaTime, 0);
    }

    // Upon collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.collider.tag;
        if (tag == "Pipe" || tag == "Goomba" || tag == "Brick")
        {
            Flip();
            // If collision, change direction
            goombaSpeed = -goombaSpeed;
        }
        else if(tag == "Player")
        {
            EnemyAI[] objects = FindObjectsOfType<EnemyAI>();
            foreach(EnemyAI obj in objects)
            {
                Debug.Log("All enemies frozen");
                // Freeze all enemies when hit
                obj.rb2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
        }
    }
    
    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }
}

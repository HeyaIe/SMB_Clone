using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : MonoBehaviour
{
    private Animator parentAnim;
    private Transform stompPoint;
    private Transform parentObject;

    // Start is called before the first frame update
    void Start()
    {
        // Get parent object
        parentObject = gameObject.transform.parent;
        stompPoint = GetComponent<Transform>();

        parentAnim = gameObject.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Dead()
    {
        Destroy(parentObject.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            FindObjectOfType<M_GameManager>().IncrementGoombaScore();
            parentObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            parentAnim.SetTrigger("stomped");
            
            Invoke("Dead", .3f);
        }
    }
}

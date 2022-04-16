using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBlock : MonoBehaviour
{
    public Rigidbody2D coinPrefab; // Insert coin prefab
    public Transform spawner; // Position coin spawn
    public float coinForce; // Apply force to coin.y   

    private GameObject child;
    private Animator anim;

    [SerializeField]
    private int maxCoins;
    [SerializeField]
    private Sprite[] blocks;
    private M_AudioManager mAudio;

    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0).gameObject;
        anim = GetComponent<Animator>();
        mAudio = FindObjectOfType<M_AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.bounds.max.y < transform.position.y
            && collision.collider.bounds.min.x < transform.position.x + .5f
            && collision.collider.bounds.max.x > transform.position.x - .5f
            && collision.collider.tag == "Player")
        {
            if(maxCoins > 0)
            {
                mAudio.Play("Bump");
                // Play animation between two intervals
                anim.Play("Block_Hit", 0, 0.4f);

                // Instantiate prefabs
                Rigidbody2D coinInstance;
                coinInstance = Instantiate(coinPrefab, spawner.position, spawner.rotation) as Rigidbody2D;
                coinInstance.AddForce(spawner.up * coinForce);
                mAudio.Play("Coin");

                maxCoins -= 1;

                // Switch sprites
                child.GetComponent<SpriteRenderer>().sprite = blocks[maxCoins];

                // Increment coins
                FindObjectOfType<M_GameManager>().IncrementCoins();
                FindObjectOfType<M_GameManager>().IncrementCoinsScore();
            }
        }
    }
}

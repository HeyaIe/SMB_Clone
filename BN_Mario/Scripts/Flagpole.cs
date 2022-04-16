using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flagpole : MonoBehaviour
{
    private M_GameManager gameManagerScript;
    private bool hasReachedBottom = false;
    private float t;
    public float timeToReachTarget = 3f;
    public Transform poleBase;

    private M_Movement player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<M_Movement>();
        poleBase = transform.GetChild(0);
        gameManagerScript = GameObject.Find("GameManager").GetComponent<M_GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasReachedBottom)
        {
            StartCoroutine(player.AutoWalk());
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !hasReachedBottom)
        {
            FindObjectOfType<M_AudioManager>().Stop("MainTheme");
            FindObjectOfType<M_AudioManager>().Play("Flagpole");
            player.canMove = false; // Player can't move/jump
            player.Flip(); // Rotate character
            // Move player's position to the other side of the pole
            player.transform.position = new Vector2(player.transform.position.x + .5f, player.transform.position.y);
            gameManagerScript.isTimerPaused = true; // Pause timer
            player.GetComponent<Animator>().SetBool("isClimbing", true);
            // Slide down the flagpole
            StartCoroutine(SlideDownPole(player.gameObject, poleBase.position, timeToReachTarget));
            gameManagerScript.IncrementLevelScore(); // Increment level completion score
        }
    }

    IEnumerator SlideDownPole(GameObject objectToMove, Vector2 targetPos, float seconds)
    {
        float elapsedTime = 0;
        Vector2 startingPos = objectToMove.transform.position;

        while(elapsedTime < seconds)
        {
            // Move player toward the base of the pole in x seconds
            objectToMove.transform.position = Vector2.Lerp(startingPos, targetPos, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        gameManagerScript.hasWon = true;
        player.GetComponent<Animator>().SetBool("isClimbing", false);
        Debug.Log("Reached bottom");
        hasReachedBottom = true;
        FindObjectOfType<M_AudioManager>().Play("StageCleared");
        player.Flip();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsible for game condition, player's stats, and game object values
public class M_GameManager : MonoBehaviour
{
    public bool isTimerPaused = false;
    public bool hasWon = false;

    private int goombaValue = 50;
    private int coinValue = 10;
    private int coinMultiplier = 10;
    private int coins;
    private int score;
    private int lives; // Mario's lives left
    private int flagpoleValue = 400;
    private float timeLeft = 400f;

    private static M_GameManager instance;

    private void Awake()
    {
        lives = 3;

        // If instance exists, destroy itself
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // Make this the main instance
        instance = this;
        GameObject.DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Start countdown timer
        if (!isTimerPaused && !hasWon)
        {
            timeLeft -= Time.deltaTime / .4f; // 1 game sec ~ 0.4 real time
        }

        // Display a sped up countdown timer to add to score
        if(timeLeft >= 0 && hasWon)
        {
            score += 10;
            --timeLeft;
        }
    }

    public float GetTimeLeft()
    {
        return timeLeft;
    }
    public void GameWon()
    {
        Debug.Log("Game won");
        FindObjectOfType<MainMenu>().LoadGameOver();
    }
    public void EndGame()
    {
        Debug.Log("Game over");
        FindObjectOfType<MainMenu>().LoadGameOver();
    }
    public int GetLivesLeft()
    {
        return lives;
    }
    public int GetCoins()
    {
        return coins;
    }
    public int GetScore()
    {
        return score;
    }
    public void IncrementCoinsScore()
    {
        score += (coinValue * coinMultiplier);
    }
    public void IncrementGoombaScore()
    {
        score += goombaValue;
    }
    public void IncrementCoins()
    {
        coins += coinValue;
    }
    public void IncrementLevelScore()
    {
        score += flagpoleValue;
    }
    public void RestartOrEndGame()
    {
        --lives;

        if (lives > 0)
        {
            FindObjectOfType<MainMenu>().RestartLevel();
            isTimerPaused = false; // Resume timer when scene restarts
        }
        else if(lives == 0)
        {
            FindObjectOfType<M_AudioManager>().Play("GameOver");
            isTimerPaused = true;
            Invoke("EndGame", 4f);
        }
    }
    public void ConfigNewGame()
    {
        score = 0;
        lives = 3;
        timeLeft = 400f;
        coins = 0;
        hasWon = false;
        isTimerPaused = false;
        FindObjectOfType<M_Movement>().canMove = true;
    }
}

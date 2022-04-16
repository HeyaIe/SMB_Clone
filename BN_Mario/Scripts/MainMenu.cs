using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    private M_GameManager gameManager;

    // Scenes to load
    public string mainScene;
    public string mainMenu;
    public string gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<M_GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Load new game, reset stats
    public void LoadNewGame()
    {
        if(mainScene != null)
        {
            StartCoroutine(LoadLevel(mainScene));
            gameManager.Invoke("ConfigNewGame", 1.5f);
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(mainScene);
    }
    // Load main menu
    public void LoadMainMenu()
    {
        if (mainMenu != null)
        {
            StartCoroutine(LoadLevel(mainMenu));
        }
    }
    // Load game over
    public void LoadGameOver()
    {
        if(gameOver != null)
        {
            StartCoroutine(LoadLevel(gameOver));
        }
    }

    // Delay transition with IEnumerator
    IEnumerator LoadLevel(string level)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(level);
    }
}

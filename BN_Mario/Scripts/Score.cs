using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI coins;
    public TextMeshProUGUI score;
    public TextMeshProUGUI lives;
    public TextMeshProUGUI time;

    GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        M_GameManager gameManagerScript = gameManager.GetComponent<M_GameManager>();

        // Updates coin amount
        coins.text = gameManagerScript.GetCoins().ToString("000");
        // Updates score amount
        score.text = gameManagerScript.GetScore().ToString("000000");
        // Updates lives left
        lives.text = gameManagerScript.GetLivesLeft().ToString();
        // Updates time left
        if (time == null) return;
        time.text = "TIME: " + gameManagerScript.GetTimeLeft().ToString("000");
    }
}

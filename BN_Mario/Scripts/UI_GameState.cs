using TMPro;
using UnityEngine;

public class UI_GameState : MonoBehaviour
{
    public TextMeshProUGUI winMessage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<M_GameManager>().hasWon)
        {
            winMessage.text = "YOU WON!";
        }
        else
        {
            winMessage.text = "YOU LOST!";
        }
    }
}

using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score = 0;

    public TextMeshProUGUI ScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = GameManager.gameManager.GetScore();

        ScoreText.text = $" Score : { score }";
    }
}

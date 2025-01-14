using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI highScoreText;

    [SerializeField] private PlayerprefsExample playerprefsExample;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int highscore = playerprefsExample.GetHighScore();


        highScoreText.text = $"High Score : {highscore}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

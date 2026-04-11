using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public int totalScore;
    public TextMeshProUGUI scoreText;


    public static GameController instance;


    void Start()
    {
        instance = this;
    }
    public void UpdadeScoreText()
    {
        scoreText.text = totalScore.ToString();
    }
}

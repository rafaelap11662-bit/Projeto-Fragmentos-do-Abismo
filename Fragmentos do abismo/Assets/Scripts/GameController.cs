using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public int totalScore;
    public Text scoreText;


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

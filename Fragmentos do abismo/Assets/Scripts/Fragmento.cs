using UnityEngine;
using UnityEngine.UI;

public class Fragmento : MonoBehaviour
{

    private SpriteRenderer sr;
    private EdgeCollider2D pikup;
    public GameObject coleta;
    public int Score = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {  
        sr = GetComponent<SpriteRenderer>();
        pikup = GetComponent<EdgeCollider2D>();
    }


    void OnTriggerEnter2D(Collider2D collider) // Detecta a colisão do Player com o fragmento.
    {
        if(collider.gameObject.tag == "Player")
        {
            sr.enabled = false;
            pikup.enabled = false;
            coleta.SetActive(true);

            GameController.instance.totalScore += Score;
            GameController.instance.UpdadeScoreText();

            Destroy(gameObject, 0.3f);  
        }
    }
}

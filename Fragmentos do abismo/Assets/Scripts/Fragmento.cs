using UnityEngine;
using UnityEngine.UI;

public class Fragmento : MonoBehaviour
{

    private SpriteRenderer sr;
    private EdgeCollider2D pikup; 
    public GameObject coleta; // animação de coleta
    public int Score = 1; //Valor do Fragmento

    
    void Start()
    {  
        sr = GetComponent<SpriteRenderer>();
        pikup = GetComponent<EdgeCollider2D>();
    }


    void OnTriggerEnter2D(Collider2D collider) // Detecta a colisão do Player com o fragmento.
    {
        if(collider.gameObject.tag == "Player")
        {
            sr.enabled = false;         //apos colisão desabilita Sprite do fragmento
            pikup.enabled = false;      //apos colisão desabilita o Collider
            coleta.SetActive(true);     //habilita a animação de coleta

            GameController.instance.totalScore += Score; //add um ponto no score
            GameController.instance.UpdadeScoreText();   //atualiza o score

            Destroy(gameObject, 0.3f);  
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public int totalScore;
    public TextMeshProUGUI scoreText;
    public Transform checkpointAtual;
    public Transform checkpointInicial;

    public static GameController instance;


    void Start()
    {
        instance = this;

        checkpointAtual = checkpointInicial;
    }
    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();
    }


    public void RespawnPlayer(SistemaCoracao coracao) 
    {
        jogador jogador = coracao.GetComponent<jogador>();
        Rigidbody2D rb = coracao.GetComponent<Rigidbody2D>(); 
        AtackPlayer ataque = coracao.GetComponent<AtackPlayer>();
        
        jogador.KBCount = -1f;
        jogador.isKnockRight = false;

        coracao.vida = coracao.vidaMaxima; 

        coracao.transform.position = checkpointAtual.position;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        jogador.anim.SetBool("IsDead", false); 

        jogador.enabled = true; 
        ataque.enabled = true;

        coracao.isDead = false;
    }
}

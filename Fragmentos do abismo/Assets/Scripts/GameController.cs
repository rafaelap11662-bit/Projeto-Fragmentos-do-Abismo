using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalScore;
    public TextMeshProUGUI scoreText;
    public Transform checkpointAtual;
    public Transform checkpointInicial;

    public GameObject painelGameOver;

    public bool temChave = false;

    public static GameController instance;

    public TextMeshProUGUI mortesText;
    public int mortes = 0;

    public BossArena bossArenaAtiva;



    void Start()
    {
        instance = this;

        checkpointAtual = checkpointInicial;
        UpdateMortesText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();
    }

    public void UpdateMortesText()
    {
        mortesText.text = mortes + "/5";
    }


    public void RespawnPlayer(SistemaCoracao coracao) 
    {
        jogador jogador = coracao.GetComponent<jogador>();
        Rigidbody2D rb = coracao.GetComponent<Rigidbody2D>(); 
        AtackPlayer ataque = coracao.GetComponent<AtackPlayer>();
        
        coracao.vida = coracao.vidaMaxima; 
        coracao.transform.position = checkpointAtual.position;
 
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        
        jogador.KBCount = -1f;  
        jogador.isKnockRight = false;

        jogador.anim.SetBool("IsDead", false);
        jogador.anim.ResetTrigger("TakeDamage");
        jogador.enabled = true; 
        ataque.enabled = true;
        coracao.isDead = false;
    }

     public void registrarMorte()
    {
        
        mortes++;
        UpdateMortesText();

        if (mortes >= 5)
        {
            if (bossArenaAtiva != null)
            {
                checkpointAtual = bossArenaAtiva.GetCheckpointBoss();
            }
        }
    }

    public void BotaoReiniciar()
    {
        Time.timeScale = 1f;

        if(bossArenaAtiva != null)
        {
            painelGameOver.SetActive(false);

            mortes = 0;
            UpdateMortesText();

            SistemaCoracao coracao = FindAnyObjectByType<SistemaCoracao>();

            if (coracao != null)
            {
                RespawnPlayer(coracao);
            }

            bossArenaAtiva.ReiniciarBatalha();
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
    }

    public void MostrarPainelGameOver()
    {
        painelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}

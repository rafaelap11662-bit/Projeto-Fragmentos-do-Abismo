using UnityEngine;
using UnityEngine.UI;

public class SistemaCoracao : MonoBehaviour
{
    jogador jogador;
    Rigidbody2D rbPlayer;
    public bool isDead;
    public int vida;            //Quantidade de vida do jogador
    public int vidaMaxima;      //Quantidade de corações jogador tem.

    public Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;

    public Transform pontoRespawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         jogador = GetComponent<jogador>(); 
         rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       CoracaoLogica();
       StatusMorte();
    }

    void CoracaoLogica()
    {
        vida = Mathf.Clamp(vida, 0, vidaMaxima);

        for (int i = 0; i < coracao.Length; i++)
        {
            coracao[i].sprite = (i < vida) ? cheio : vazio;
            coracao[i].enabled = i < vidaMaxima;
        }
    }
    void StatusMorte() // Verifica se o jogador morreu e executa as ações correspondentes
    {
        if (vida <= 0 && !isDead) // Verifica se a vida é menor ou igual a 0 e se o jogador ainda não está marcado como morto
        {
            isDead = true; 

            rbPlayer.linearVelocity = Vector2.zero;
            rbPlayer.angularVelocity = 0f; 
        
            GetComponent<jogador>().enabled = false; 
            GetComponent<AtackPlayer>().enabled = false;  
            jogador.anim.SetBool("IsDead", true);
        
            Invoke(nameof(Respawn), 1.0f);
        }
    }

    void Respawn()
    {
        jogador.KBCount = -1f;  
        jogador.isKnockRight = false;                          

        vida = vidaMaxima;                              //traz a vida de volta ao máximo

        transform.position = pontoRespawn.position;     //teleporta o jogador para o ponto de respawn

        rbPlayer.linearVelocity = Vector2.zero;
        rbPlayer.angularVelocity = 0f; 


        jogador.anim.SetBool("IsDead", false);          //desativa a animação de morte
        GetComponent<jogador>().enabled = true;         //serve para que o jogador possa se mover novamente
        GetComponent<AtackPlayer>().enabled = true;     //serve para que o jogador possa atacar novamente
        isDead = false;                                 //reseta o status de morte para permitir que o jogador morra novamente
    }
}
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] int dano;


    void OnCollisionEnter2D(Collision2D collision)                              
    {
        if(collision.gameObject.tag =="Player")
    {
        jogador player = collision.gameObject.GetComponent<jogador>();

        
        if(player.isInvencivel)                                                   // Se o jogador estiver invencível, cancela o dano
            return;

        player.KBCount = player.KBTime;                                           // Reinicia o tempo do Knockback

        if(collision.transform.position.x <= transform.position.x)                // Verifica se o jogador está à esquerda do inimigo
        {
            player.isKnockRight = true;                                           // Define que o Knockback será para a esquerda
        }
        else
        {
            player.isKnockRight = false;                                          // Define que o Knockback será para a direita
        }

        player.receberDano(dano);                                                 // Chama a função de receber dano no jogador

        if (player.coracao.vida <= 0) 
        {
            return;
        }

        player.anim.SetTrigger("TakeDamage");                                     // Ativa a animação de dano do jogador

        StartCoroutine(player.Invencibilidade());                                 // Inicia a Coroutine de invencibilidade temporária
    }
    }

    void OnTriggerEnter2D(Collider2D collision)                              
    {
        if(collision.gameObject.tag =="Player")
    {
        jogador player = collision.gameObject.GetComponent<jogador>();

        
        if(player.isInvencivel)                                                   // Se o jogador estiver invencível, cancela o dano
            return;

        player.KBCount = player.KBTime;                                           // Reinicia o tempo do Knockback

        if(collision.transform.position.x <= transform.position.x)                // Verifica se o jogador está à esquerda do inimigo
        {
            player.isKnockRight = true;                                           // Define que o Knockback será para a esquerda
        }
        else
        {
            player.isKnockRight = false;                                          // Define que o Knockback será para a direita
        }

        player.receberDano(dano);                                                 // Chama a função de receber dano no jogador

        if (player.coracao.vida <= 0)
        {
            return;
        }
        
        player.anim.SetTrigger("TakeDamage");                                       // Ativa a animação de dano do jogador

        StartCoroutine(player.Invencibilidade());                                 // Inicia a Coroutine de invencibilidade temporária
    }
    }
}

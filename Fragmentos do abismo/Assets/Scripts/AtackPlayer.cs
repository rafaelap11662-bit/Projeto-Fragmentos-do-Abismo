using System.Reflection;
using UnityEngine;


public class AtackPlayer : MonoBehaviour
{
    
    public Animator animator;
    public Transform ataquePoint;                               // Ponto onde o ataque será detectado
    public float ataqueRanger = 0.5f;                           // Alcance do ataque
    public LayerMask inimigoLayers;                             // Layer dos inimigos

    private int combo;                                          // Variável que controla o número atual do combo
    private float comboTime;                                    // Tempo restante para continuar o combo
    public float startComboTime = 1.5f;                         // Tempo máximo entre ataques antes do combo resetar
    private bool podeAtacar = true;                             // Variável para controlar se o jogador pode atacar ou não 
    
    void Update()
    {
        if(comboTime > 0)                                       // Verifica se ainda existe tempo para continuar o combo
        {
            comboTime -= Time.deltaTime;                        // Diminui o tempo do combo usando o tempo real do jogo
        }
        else
        {
            combo = 0;                                          // Reseta o combo caso o tempo acabe
        }

        if(Input.GetKeyDown(KeyCode.K) && podeAtacar)                         // Verifica se a tecla K foi pressionada
        {
            Ataque();                                           // Chama a função de ataque
        }
    }

    
    void Ataque() 
    {
        podeAtacar = false;                                    // Impede que o jogador ataque novamente até que a animação termine
        combo++;

        comboTime = startComboTime;                             // Reinicia o tempo do combo

        if(combo > 3)                                           // Limita o combo até 3 ataques
        {
            combo = 1;                                          // Volta para o primeiro ataque
        }

        if(combo == 1)                                          // Se o combo for igual a 1
        {   
            animator.SetTrigger("Atack01");                     // Ativa a animação do primeiro ataque
        }

        if(combo == 2)                                          // Se o combo for igual a 2
        {
            animator.SetTrigger("Atack02");                     // Ativa a animação do segundo ataque
        }

        if(combo == 3)                                          // Se o combo for igual a 3
        {
            animator.SetTrigger("Atack03");                     // Ativa a animação do terceiro ataque
        }



        // Cria uma área circular de ataque e detecta todos os inimigos dentro dela
        Collider2D[] hitInimigos = Physics2D.OverlapCircleAll(ataquePoint.position, ataqueRanger, inimigoLayers);

        
        foreach(Collider2D inimigo in hitInimigos)                      // Percorre todos os inimigos atingidos
        {
            inimigo.GetComponent<MorteInimigo>().danoInimigo(1);        // Chama a função de dano no inimigo

            Vector2 direcaoKnockback = (inimigo.transform.position - transform.position).normalized;         // Calcula a direção do knockback

            KBInimigo knockback = inimigo.GetComponent<KBInimigo>();

            if(knockback != null)
            {
                knockback.AplicarKnockback(direcaoKnockback);
            }
        }

        Invoke(nameof(liberarAtaque), 0.2f);                            // Permite que o jogador ataque novamente após um curto período de tempo
    }

    private void liberarAtaque()
    {
        podeAtacar = true;
    }

    // Para conseguir ver o AtaquePoint
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ataquePoint.position, ataqueRanger);
    }*/
}

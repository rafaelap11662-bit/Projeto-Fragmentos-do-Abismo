using UnityEngine;

public class AtackPlayer : MonoBehaviour
{
    private jogador jogador;
    public Animator animator;
    public Transform ataquePoint;                               // Ponto onde o ataque será detectado
    public float ataqueRanger = 0.5f;                           // Alcance do ataque
    public LayerMask inimigoLayers;                             // Layer dos inimigos

    private int combo;                                          // Variável que controla o número atual do combo
    private float comboTime;                                    // Tempo restante para continuar o combo
    public float startComboTime = 1.5f;                         // Tempo máximo entre ataques antes do combo resetar
    private bool podeAtacar = true;                             // Variável para controlar se o jogador pode atacar ou não 

    [Header ("Sons dos ataques")]
    [SerializeField] private AudioClip somAtaque1;
    [SerializeField] private AudioClip somAtaque2;
    [SerializeField] private AudioClip somAtaque3;
    
    

    void Start()
    {
        jogador = GetComponent<jogador>();
    }

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

        if((Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.JoystickButton2)) && podeAtacar)                         // Verifica se a tecla K foi pressionada
        {
            Ataque();                                           // Chama a função de ataque
        }
    }

    
    void Ataque() 
    {
        podeAtacar = false;                                    // Impede que o jogador ataque novamente até que a animação termine
        jogador.atacando = true;
        combo++;

        comboTime = startComboTime;                             // Reinicia o tempo do combo

        if(combo > 3)                                           // Limita o combo até 3 ataques
        {
            combo = 1;                                          // Volta para o primeiro ataque
        }

        if(combo == 1)                                          // Se o combo for igual a 1
        {   
            animator.SetTrigger("Atack01");                     // Ativa a animação do primeiro ataque
            AudioManager.Instance.TocarSomSFXJogador(somAtaque1);
        }

        if(combo == 2)                                          // Se o combo for igual a 2
        {
            animator.SetTrigger("Atack02");                     // Ativa a animação do segundo ataque
            AudioManager.Instance.TocarSomSFXJogador(somAtaque2);
        }

        if(combo == 3)                                          // Se o combo for igual a 3
        {
            animator.SetTrigger("Atack03");                     // Ativa a animação do terceiro ataque
            AudioManager.Instance.TocarSomSFXJogador(somAtaque3);
        }



        // Cria uma área circular de ataque e detecta todos os inimigos dentro dela
        Collider2D[] hitInimigos = Physics2D.OverlapCircleAll(ataquePoint.position, ataqueRanger, inimigoLayers);

        
        foreach(Collider2D inimigo in hitInimigos)                      // Percorre todos os inimigos atingidos
        {

            Boss boss = inimigo.GetComponent<Boss>();

            if (boss != null)
            {
                boss.ReceberDano(1);
            }
            else
            {
                MorteInimigo morte = inimigo.GetComponent<MorteInimigo>();

                if (morte == null) //
                    continue;

                bool recebeuDano = morte.danoInimigo(1);

                if (!recebeuDano)
                    continue;
                }

                Vector2 direcaoKnockback = (inimigo.transform.position - transform.position).normalized;         // Calcula a direção do knockback

                KBInimigo knockback = inimigo.GetComponent<KBInimigo>();

                if(knockback != null)
                {
                    knockback.AplicarKnockback(direcaoKnockback);
                }
        }

        Invoke(nameof(liberarAtaque), 0.4f);                            // Permite que o jogador ataque novamente após um curto período de tempo
    }

    private void liberarAtaque()
    {
        podeAtacar = true;
        jogador.atacando = false;
    }

    // Para conseguir ver o AtaquePoint
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ataquePoint.position, ataqueRanger);
    }
}

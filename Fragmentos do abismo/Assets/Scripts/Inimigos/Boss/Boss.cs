using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sprite;

    [Header("MOVIMENTAÇÃO")]
    [SerializeField] private float velocidade;
    [SerializeField] private float distanciaMinima;
    
    [Header("DISTANCIAS DE ATAQUE")]
    [SerializeField] private float distanciaAtaque;
    [SerializeField] private float distanciaRajada;
    [SerializeField] private float distanciaRepelir;

    [Header("ATAQUE")]
    [SerializeField] private Transform ataquePoint;
    [SerializeField] private float ataqueRange;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int danoFacao;
    [SerializeField] private int danoRajada;
    [SerializeField] private int danoRepelir;
     private int dano;
    
    [Header("REPELIR")]
    [SerializeField] private float repelirRange;
    [SerializeField] private float knockbackRepelir;
    [SerializeField] private Transform repelirPoint;

    [Header("RAJADA")]
    [SerializeField] private GameObject rajadaPrefab;
    [SerializeField] private Transform rajadaPoint;

    [Header("ESTADOS DO BOSS")]
    private EstadoBoss estadoAtual = EstadoBoss.DECIDINDO;
    private TipoAtaque ataqueAtual;


    private bool executandoAtaque = false;
    
    private Rigidbody2D rb;
    private Transform player;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform; 
        }
    }

    private enum EstadoBoss
    {
        DECIDINDO,
        PERSEGUINDO,
        ATACANDO,
        REPELIR,
        ESPERANDO
    }

    private enum TipoAtaque
    {
        RAJADA,
        FACAO
    }

    private void FixedUpdate()
    {
        if(player == null)
            return;


        if (!executandoAtaque) 
        {
            VirarBoss();
        }

        if(estadoAtual == EstadoBoss.DECIDINDO)
        {
            EscolherAcao();
        }
        if(estadoAtual == EstadoBoss.PERSEGUINDO)
        {
            SeguirPlayer();
        }
        if(estadoAtual == EstadoBoss.ATACANDO)
        {
            AtacarPlayer();
        }
        if(estadoAtual == EstadoBoss.REPELIR)
        {
            IniciarRepelir();
        }
    }

    private void VirarBoss()
    {
        if (player.position.x > transform.position.x)
        {
            sprite.flipX = true;
            ataquePoint.localPosition = new Vector2(Mathf.Abs(ataquePoint.localPosition.x), ataquePoint.localPosition.y);
            rajadaPoint.localPosition = new Vector2(Mathf.Abs(rajadaPoint.localPosition.x), rajadaPoint.localPosition.y);
        }
        else
        {
            sprite.flipX = false;
            ataquePoint.localPosition = new Vector2(-Mathf.Abs(ataquePoint.localPosition.x), ataquePoint.localPosition.y);
            rajadaPoint.localPosition = new Vector2(-Mathf.Abs(rajadaPoint.localPosition.x), rajadaPoint.localPosition.y);


        }
    }

    private void SeguirPlayer()
    {
        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia > distanciaMinima)
        {
            Vector2 direcao = (player.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direcao.x * velocidade, rb.linearVelocity.y);

            anim.SetBool("isRun", true);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetBool("isRun", false);
            
            estadoAtual = EstadoBoss.ATACANDO;
            ataqueAtual = TipoAtaque.FACAO;
        }
    }

    private void EscolherAcao()
    {
        float distancia = Vector2.Distance(transform.position, player.position); // calcula a distancia entre o boss e o player

        if(distancia >= distanciaRajada) 
        {
            int escolha = Random.Range(0, 2);

            if(escolha == 0)
            {
            ataqueAtual = TipoAtaque.RAJADA;
            estadoAtual = EstadoBoss.ATACANDO;    
            }
            else
            {
                estadoAtual = EstadoBoss.PERSEGUINDO;
            }
        }
        else if(distancia <= distanciaRajada && distancia > distanciaAtaque)
        {
            estadoAtual = EstadoBoss.PERSEGUINDO;
        }
        else if(distancia > distanciaRepelir && distancia <= distanciaAtaque)
        {
            ataqueAtual = TipoAtaque.FACAO;
            estadoAtual = EstadoBoss.ATACANDO;
        }
        else
        {
            estadoAtual = EstadoBoss.REPELIR;
        }
    }    

    private void AtacarPlayer()
    {
        if(executandoAtaque)
            return;


        executandoAtaque = true;

        if(ataqueAtual == TipoAtaque.RAJADA)
        {
            AtaqueRajada();
            dano = danoRajada;
        }   
        if(ataqueAtual == TipoAtaque.FACAO)
        {
            AtaqueFacao();
            dano = danoFacao;
        }
    }
    private void IniciarRepelir()
    {
        if(executandoAtaque)
            return;

            executandoAtaque = true;

            anim.SetTrigger("isRepelir");
    }

    public void RepelirPlayer()
    {
        Collider2D PlayerCollider = Physics2D.OverlapCircle(repelirPoint.position, repelirRange, playerLayer);

        if (PlayerCollider != null)
        {
            jogador player = PlayerCollider.GetComponent<jogador>(); 
            
            if(player != null) 
            {
                if (player.isInvencivel)
                return;

                
                player.KBforce = knockbackRepelir;
                player.KBCount = player.KBTime;

                if (PlayerCollider.transform.position.x <= transform.position.x)
                {
                    player.isKnockRight = true;
                }
                else
                {
                    player.isKnockRight = false;
                }
                    player.receberDano(danoRepelir);
                    player.anim.SetTrigger("TakeDamage");
                    StartCoroutine(player.Invencibilidade());
                
            }
        }


    }

    private void AtaqueFacao()
    {
        anim.SetTrigger("isFacaoAtk");
    }

    private void AtaqueRajada()
    {
        anim.SetTrigger("isRajadaAtk");
        Debug.Log("Ataque rajada");
    }


    public void FinalizarAtaque()
    {
        executandoAtaque = false;
        estadoAtual = EstadoBoss.ESPERANDO;

        Invoke(nameof(VoltarDecidir), 2f);
    }

    public void VoltarDecidir()
    {
        estadoAtual = EstadoBoss.DECIDINDO;
    }

    public void CausarDano() // Metodo para o BossAnimation.cs chamar e causar dano no player FACAO
    {
        Collider2D PlayerCollider = Physics2D.OverlapCircle(ataquePoint.position, ataqueRange, playerLayer);

        CausarDano(PlayerCollider);
    }

    public void CausarDano(Collider2D PlayerCollider) // Metodo para o BossAnimation.cs chamar e causar dano no player
    {
        
        if (PlayerCollider != null)
        {
            jogador player = PlayerCollider.GetComponent<jogador>(); 
            
            if(player != null) 
            {
                if (player.isInvencivel)
                return;

            player.KBCount = player.KBTime; 

            if (PlayerCollider.transform.position.x <= transform.position.x)
            {
                player.isKnockRight = true;
            }
            else
            {
                player.isKnockRight = false;
            }
                player.receberDano(dano);
                player.anim.SetTrigger("TakeDamage");
                StartCoroutine(player.Invencibilidade());
                
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (ataquePoint == null)
            return;

        Gizmos.DrawWireSphere(ataquePoint.position, ataqueRange);
        Gizmos.DrawWireSphere(repelirPoint.position, repelirRange);
    }

    public void CriarRajada()
    {
        GameObject novaRajada = Instantiate(rajadaPrefab, rajadaPoint.position, Quaternion.identity);

        float direcao = sprite.flipX ? 1f : -1f;

        Rajada rajada = novaRajada.GetComponent<Rajada>();

        rajada.ConfigurarDirecao(direcao, this);
    }
}

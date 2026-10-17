using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private BossArena arena;
    

    [Header("MOVIMENTAÇÃO")]
    [SerializeField] private float velocidade;
    [SerializeField] private float distanciaMinima;

    [Header("SEGUNDA FASE")]
    [SerializeField] private float multiVelocidadeFase2 = 1.5f;
    private bool segundaFase = false;

    
    [Header("DISTANCIAS DE ATAQUE")]
    [SerializeField] private float distanciaAtaque;
    [SerializeField] private float distanciaRajada;
    [SerializeField] private float distanciaRepelir;

    [Header("ATAQUE")]
    [SerializeField] private float ataqueRange;
    [SerializeField] private Transform ataquePoint;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int danoFacao;
    [SerializeField] private int danoRajada;
    [SerializeField] private int danoRepelir;
    [SerializeField] private int danoInvestida;
     private int dano;
    
    [Header("REPELIR")]
    [SerializeField] private float repelirRange;
    [SerializeField] private float knockbackRepelir;
    [SerializeField] private Transform repelirPoint;

    [Header("RAJADA")]
    [SerializeField] private GameObject rajadaPrefab;
    [SerializeField] private Transform rajadaPoint;

    [Header("INVESTIDA")]
    [SerializeField] private float InvestidaRange;
    [SerializeField] private float velocidadeInvestida;
    [SerializeField] private float distanciaInvestida;
    [SerializeField] private Transform investidaPoint;

    

    [Header("VIDA DO BOSS")]
    [SerializeField] private int vidaAtual;
    private int vidaInicial = 20;
    private bool morreu = false;


    [Header("ESTADOS DO BOSS")]
    private EstadoBoss estadoAtual = EstadoBoss.DECIDINDO;
    private TipoAtaque ataqueAtual;


    private Vector2 posicaoInicial;
    private bool executandoAtaque = false;
    private bool executandoInvestida = false;
    private bool invencivel = false;
    
    private Rigidbody2D rb;
    private Transform player;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        vidaAtual = vidaInicial;
        posicaoInicial = transform.position;
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
        ESPERANDO,
        TOMANDO_DANO
    }

    private enum TipoAtaque
    {
        RAJADA,
        FACAO,
        INVESTIDA
    }

    private void FixedUpdate()
    {
        if(morreu ||player == null)
            return;
        
        if (executandoInvestida)
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
        if(estadoAtual == EstadoBoss.TOMANDO_DANO)
        {
            return;
        }
    }

    private void VirarBoss()
    {
        if (player.position.x > transform.position.x)
        {
            sprite.flipX = true;
            ataquePoint.localPosition = new Vector2(Mathf.Abs(ataquePoint.localPosition.x), ataquePoint.localPosition.y);
            rajadaPoint.localPosition = new Vector2(Mathf.Abs(rajadaPoint.localPosition.x), rajadaPoint.localPosition.y);
            investidaPoint.localPosition = new Vector2(Mathf.Abs(investidaPoint.localPosition.x), investidaPoint.localPosition.y);
        }
        else
        {
            sprite.flipX = false;
            ataquePoint.localPosition = new Vector2(-Mathf.Abs(ataquePoint.localPosition.x), ataquePoint.localPosition.y);
            rajadaPoint.localPosition = new Vector2(-Mathf.Abs(rajadaPoint.localPosition.x), rajadaPoint.localPosition.y);
            investidaPoint.localPosition = new Vector2(-Mathf.Abs(investidaPoint.localPosition.x), investidaPoint.localPosition.y);


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
        if (segundaFase && distancia >= distanciaInvestida)
        {
            int escolha = Random.Range(0, 3);

            if (escolha == 0)
            {
                ataqueAtual = TipoAtaque.INVESTIDA;
                estadoAtual = EstadoBoss.ATACANDO;
                return;
            }
        }
        if(distancia >= distanciaRajada) 
        {   
            int escolha = Random.Range(0, 3);

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

        anim.SetBool("isRun", false);

        if(ataqueAtual == TipoAtaque.RAJADA)
        {
            dano = danoRajada;
            AtaqueRajada();
        }   
        if(ataqueAtual == TipoAtaque.FACAO)
        {
            dano = danoFacao;
            AtaqueFacao();
        }
        if(ataqueAtual == TipoAtaque.INVESTIDA)
        {
            dano = danoInvestida;
            IniciarInvestida();
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
        //Debug.Log("Ataque rajada");
    }


    public void FinalizarAtaque() 
    {
        if (executandoInvestida)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            executandoInvestida = false;
        }

        executandoAtaque = false;
        estadoAtual = EstadoBoss.ESPERANDO;

        Invoke(nameof(VoltarDecidir), 1f);
    }

    public void VoltarDecidir()
    {
        estadoAtual = EstadoBoss.DECIDINDO;
    }

    public void CausarDanoFacao() // Metodo para o BossAnimation.cs chamar e causar dano no player FACAO
    {
        Collider2D PlayerCollider = Physics2D.OverlapCircle(ataquePoint.position, ataqueRange, playerLayer);
        CausarDano(PlayerCollider);
    }
    public void CausarDanoInvestida() // esse nao chama pela animacao
    {
        Collider2D PlayerCollider = Physics2D.OverlapCircle(investidaPoint.position, InvestidaRange, playerLayer);
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
        Gizmos.DrawWireSphere(investidaPoint.position, InvestidaRange);
    }

    public void CriarRajada()
    {
        GameObject novaRajada = Instantiate(rajadaPrefab, rajadaPoint.position, Quaternion.identity);

        float direcao = sprite.flipX ? 1f : -1f;

        Rajada rajada = novaRajada.GetComponent<Rajada>();

        rajada.ConfigurarDirecao(direcao, this);
    }

    public void AtivarIvencibilidade()
    {
        invencivel = true;
    }
    public void DesativarIvencibilidade()
    {
        invencivel = false;
    }

    public void ReceberDano(int dano)
    {
        if (morreu || invencivel)
            return;

        vidaAtual -= dano;

        if (!segundaFase && vidaAtual <= vidaInicial / 2)
        {
            segundaFase = true;
            velocidade *= multiVelocidadeFase2;

            Debug.Log("Segunda fase COMEÇOU!");
        }

        if(vidaAtual <= 0)
        {
            Morreu();
            return;
        }
        estadoAtual = EstadoBoss.TOMANDO_DANO;
        executandoAtaque = true;

        anim.SetBool("isRun", false);
        anim.SetTrigger("isHit");
    }
    
    public void FinalizarDano()
    {
        if (morreu)
            return;

        executandoAtaque = false;
        estadoAtual = EstadoBoss.DECIDINDO;
    }

    private void Morreu()
    {
        if (morreu)
            return;

        morreu = true;

        rb.linearVelocity = Vector2.zero;
        
        anim.SetBool("isRun", false);
        anim.SetTrigger("isDead");

        Debug.Log("Boss morreu");
    }

    public void AbrirBarreira() // Metodo para o BossAnimation chamar e abrir a barreira da arena
    {
        arena.AbrirBarreira();
    }


    public void ReiniciarBoss()
    {
        CancelInvoke(nameof(VoltarDecidir));

        vidaAtual = vidaInicial;

        segundaFase = false;
        velocidade /= multiVelocidadeFase2;

        morreu = false;
        invencivel = false;
        executandoAtaque = false;

        estadoAtual = EstadoBoss.DECIDINDO;
        ataqueAtual = TipoAtaque.FACAO; 
        dano = danoFacao;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        anim.Rebind();
        anim.Update(0f);

        anim.SetBool("isRun", false);
        Debug.Log("Boss reiniciando");

        rb.position = posicaoInicial;
    }

    public void IniciarInvestida()
    {
        executandoInvestida = true;

        float direcaoInvestida = player.position.x > transform.position.x ? 1f : -1f; 

        sprite.flipX = direcaoInvestida > 0; 

        rb.linearVelocity = new Vector2(direcaoInvestida * velocidadeInvestida, 0f);

        anim.ResetTrigger("isInvestida");
        anim.SetTrigger("isInvestida"); 
    }
}

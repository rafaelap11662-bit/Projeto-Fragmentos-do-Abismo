using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class Demonoide : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private float perseguicao = 3f;
    [SerializeField] private float speed;
    [SerializeField] private bool ground = true;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform ataquePoint;

    [SerializeField] private Transform currentTarget;
    [SerializeField] private Transform targetA;
    [SerializeField] private Transform targetB;
    [SerializeField] private Transform alvo;
    [SerializeField] private float visao;
    [SerializeField] private float distanciaMinima;

    [Header("ATAQUE")]
    [SerializeField] private float ataqueRange;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private int dano;
    private bool atacando = false;

    [Header ("DETECÇÃO DE PAREDE")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallDistance;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float forceJump;




    void Start()
    {
        currentTarget = targetA;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcurarPlayer();
        if(alvo != null)
        {
            SeguirPlayer();
        }
        else
        {
            Move();
        }
    }

     private void Move()
    {
        // movimento
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        //caso detecte uma parede, dar um salto 
        if (DetectarParede())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forceJump);
        }


        if(currentTarget == targetA && transform.position.x == targetA.position.x)
        {
            currentTarget = targetB;
        }
        else if(currentTarget == targetB && transform.position.x == targetB.position.x)
        {
            currentTarget = targetA;
        }
        MudarDirecao(currentTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, visao);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ataquePoint.position, ataqueRange);
        Gizmos.color = Color.white;
    }

    private void ProcurarPlayer()
    {
    Collider2D[] colisores = Physics2D.OverlapCircleAll(transform.position,visao);

    foreach (Collider2D colisor in colisores)
    {
        if (colisor.CompareTag("Player"))
        {
            alvo = colisor.transform;
            return;
        }
    }

        alvo = null;
    }

    private void SeguirPlayer()
    {
        Vector2 posicaoAlvo = this.alvo.position; 
        Vector2 posicaoAtual = this.transform.position;
        float distancia = Vector2.Distance(posicaoAlvo, posicaoAtual);

        if (distancia >= distanciaMinima){
            transform.position = Vector2.MoveTowards(posicaoAtual, posicaoAlvo, perseguicao * Time.deltaTime); 
            if (DetectarParede())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, forceJump);
            }
                MudarDirecao(alvo);  
        }
        else if (!atacando)
        {
            atacando = true;
            anim.SetTrigger("DemonoideAtack");
        }

    }

    private void MudarDirecao(Transform destino)
    {
        if (transform.position.x > destino.position.x)
        {
            sprite.flipX = true;
            ataquePoint.localPosition = new Vector2(-Mathf.Abs(ataquePoint.localPosition.x), ataquePoint.localPosition.y);
            wallCheck.localPosition = new Vector2(-Mathf.Abs(wallCheck.localPosition.x), wallCheck.localPosition.y);
        }
        else
        {
            sprite.flipX = false;
            ataquePoint.localPosition = new Vector2(Mathf.Abs(ataquePoint.localPosition.x), ataquePoint.localPosition.y);
            wallCheck.localPosition = new Vector2(Mathf.Abs(wallCheck.localPosition.x), wallCheck.localPosition.y);
        }
    }

    public void AtacarPlayer()
    {
        Collider2D PlayerCollider = Physics2D.OverlapCircle(ataquePoint.position, ataqueRange, PlayerLayer);
        
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

    public void liberarAtaque()
    {
    atacando = false;
    Debug.Log("Ataque liberado");
    }
    
    private bool DetectarParede()
    {
        return Physics2D.Raycast(wallCheck.position, transform.right, wallDistance, wallLayer);
    }
    
}

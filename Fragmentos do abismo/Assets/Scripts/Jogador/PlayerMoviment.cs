using UnityEngine;
using System.Collections;


public class jogador : MonoBehaviour
{
    Rigidbody2D rbPlayer;
    public SistemaCoracao coracao;

    [SerializeField] float speed = 5f;

    [SerializeField] float jumpForce = 15f;
    [SerializeField] bool isJump;  
    [SerializeField] bool inFloor = true;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    public Animator anim;

    public float KBforce;
    public float KBCount;
    public float KBTime;
    private float kbForceNormal; 

    public bool isKnockRight;
    

    public bool isInvencivel = false;

    // Variáveis para controlar o som
    [SerializeField] private float velocidadeMinimaPouso = 8f;
    private float velocidadeQueda;
    private bool estavaNoChao;
    private bool pulou;

    private void Start()
    {
        estavaNoChao = inFloor;
    }

    private void Awake()
    { 
        rbPlayer = GetComponent<Rigidbody2D>();    // GetComponent le o componente RIgidbody2s dentro de jogador

        kbForceNormal = KBforce;
    }

    private void Update()
    {
        inFloor = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.6f, 0.3f), 0f, groundLayer); 
        //Debug.DrawLine(transform.position, groundCheck.position, Color.blue);
         

        if (Input.GetButtonDown("Jump") && inFloor) 
           isJump = true;
        else if (Input.GetButtonUp("Jump") && rbPlayer.linearVelocity.y > 0)
           rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, rbPlayer.linearVelocity.y * 0.5f);
    }

    // Para visualizar a caixa de colisão do chão no editor, para ajudar 
    // ajustar a posição e o tamanho da caixa de colisão corretamente.

    void OnDrawGizmos()    
    {
    Gizmos.color = Color.green;                                                 // Define a cor do Gizmo como verde
    Gizmos.DrawWireCube(groundCheck.position, new Vector2(0.6f, 0.3f));         // Desenha uma caixa
    }

    void FixedUpdate()
    {
        KnockLogica();
        JumpPlayer();
        MoveAnim();
        JumpAnim();
        ControlarSomCorrida();
        ControlarSomPouso();
    }

    public IEnumerator Invencibilidade() 
    {
    isInvencivel = true;
    
    yield return new WaitForSeconds(0.5f); 

    isInvencivel = false;
    
    }
    
    // Função responsável pela lógica de Knockback (empurrão ao tomar dano)
    void KnockLogica()              
    {
        if(KBCount < 0)                                                         // Verifica se o tempo do Knockback acabou
        {
            KBforce = kbForceNormal;
            Move();                                                             // Permite o jogador se mover normalmente
        }
        else
        {
            if(isKnockRight == true)                                            // Se o inimigo estiver à direita do player
            {
                rbPlayer.linearVelocity = new Vector2(-KBforce, KBforce);       // Empurra o jogador para a esquerda e para cima
            }
            if(isKnockRight == false)                                           // Se o inimigo estiver à esquerda do player
            {
                rbPlayer.linearVelocity = new Vector2(KBforce, KBforce);        // Empurra o jogador para a direita e para cima
            }
        }
        KBCount -= Time.deltaTime;                                              // diminui o tempo para o player não ficar em estado de Knockback
    }

    void Move()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        rbPlayer.linearVelocity = new Vector2(xMove * speed, rbPlayer.linearVelocity.y);

        if (xMove > 0)              // Vai para Direita
        {
            transform.eulerAngles = new Vector2(0, 0);
           
        }
        else if (xMove < 0)         // Vai para Esquerda 
        {
            transform.eulerAngles = new Vector2(0, 180);
            
        }
        
    }
    
    void MoveAnim()                 // Animação de RUN/IDLE
    {
        anim.SetFloat("HorizontalAnim", rbPlayer.linearVelocity.x);
    }


    void JumpPlayer()               //Pulo do Jogador
    {
        if (isJump){
        rbPlayer.linearVelocity = Vector2.up * jumpForce;
        isJump = false;

        pulou = true;
        if (AudioManager.Instance != null)
            AudioManager.Instance.TocarPulo();
        }

    }


    void JumpAnim()                 // Animação do pulo
    {
        anim.SetFloat("VerticalAnim", rbPlayer.linearVelocity.y);
        anim.SetBool("groundCheck", inFloor);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

    if (collision.gameObject.tag == "FIreBox")
    {
        coracao.vida -= 1;
        AudioManager.Instance.TocarHit();
    }
    }
    
    //Função para receber os DANOS
    public void receberDano (int dano) 
    {
        coracao.vida -= dano;
        AudioManager.Instance.TocarHit();
    }

    public void pararJogador()
    {
        rbPlayer.linearVelocity = Vector2.zero;
        
        isJump = false;

        anim.SetFloat("HorizontalAnim", 0);

    }

    // =====Funçoes para controlar o som de corrida do jogador=======
    void ControlarSomCorrida()
    {
        bool estaCorrendo = Mathf.Abs(rbPlayer.linearVelocity.x) > 0.1f && inFloor && KBCount < 0;

         if (estaCorrendo)
        {
            AudioManager.Instance.TocarCorrida();
        }
        else
        {
            AudioManager.Instance.PararCorrida();
        }
    }
    
    void ControlarSomPouso()
    {
        if (!inFloor && rbPlayer.linearVelocity.y < 0)
        {
            velocidadeQueda = rbPlayer.linearVelocity.y;
        }

        if (!estavaNoChao && inFloor)
        {
            if (pulou || velocidadeQueda <= -velocidadeMinimaPouso)
            {
                AudioManager.Instance.TocarPouso();
            }

            pulou = false;
            velocidadeQueda = 0;
        }

        estavaNoChao = inFloor;
    }
}
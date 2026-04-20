using UnityEngine;
using System.Data.Common;
using System.Collections.Generic; 
using System.Collections;
using System.Reflection;
using UnityEngine.Video;

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
    [SerializeField] private Animator anim;


    

    private void Awake()
    {
        // GetComponent le o componente dentro de jogador
        rbPlayer = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        inFloor = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.6f, 0.3f), 0f, groundLayer);
        Debug.DrawLine(transform.position, groundCheck.position, Color.blue); 

        if (Input.GetButtonDown("Jump") && inFloor)
           isJump = true;
        else if (Input.GetButtonUp("Jump") && rbPlayer.linearVelocity.y > 0)
           rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, rbPlayer.linearVelocity.y * 0.5f);
            
    }

// Para visualizar a caixa de colisão do chão no editor, para ajudar ajustar a posição e o tamanho da caixa de colisão corretamente.
    void OnDrawGizmos()
    {
    Gizmos.color = Color.green;
    Gizmos.DrawWireCube(groundCheck.position, new Vector2(0.6f, 0.3f));
    }

    void FixedUpdate()
    {
        Move();
        JumpPlayer();
        MoveAnim();
        JumpAnim();
    }

    void Move()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        rbPlayer.linearVelocity = new Vector2(xMove * speed, rbPlayer.linearVelocity.y);

        if (xMove > 0) // Vai para Direita
        {
            transform.eulerAngles = new Vector2(0, 0);
           
        }
        else if (xMove < 0) // Vai para Esquerda 
        {
            transform.eulerAngles = new Vector2(0, 180);
            
        }
        
    }
    
    void MoveAnim() // Animação de RUN/IDLE
    {
        anim.SetFloat("HorizontalAnim", rbPlayer.linearVelocity.x);
    }

    void JumpPlayer()//Pulo do Jogador
    {
        if (isJump){
        rbPlayer.linearVelocity = Vector2.up * jumpForce;
        isJump = false;
        }
    }
    void JumpAnim()// Animação do pulo
    {
        anim.SetFloat("VerticalAnim", rbPlayer.linearVelocity.y);
        anim.SetBool("groundCheck", inFloor);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Trap")
        {
            coracao.vida -=3;
        } 
        if(collision.gameObject.tag == "Espeto")
        {
            coracao.vida -=2;
        }
    }

     void OnTriggerEnter2D(Collider2D collision)
    {

    if (collision.gameObject.tag == "FIreBox")
    {
        coracao.vida -= 1;
    }
}
}

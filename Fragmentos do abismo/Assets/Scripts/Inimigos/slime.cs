using System;
using UnityEditor.Search;
using UnityEngine;

public class slime : MonoBehaviour
{
    public float speed;
    public bool ground = true;

    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    public float knockbackForce = 5f;

    public bool rotacao;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // movimento
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // detecta chão
        ground = Physics2D.Linecast( groundCheck.position, transform.position, groundLayer);

        // detecta parede
        bool wall = Physics2D.Raycast( wallCheck.position, transform.right, 0.1f, groundLayer);

        
        // vira se não tiver chão ou tiver parede
        if (!ground || wall)
        {
            speed *= -1;
        }

        // rotação
        if (speed > 0 && !rotacao) 
        {
            Flip();
        }
        else if (speed < 0 && rotacao)
        {
            Flip();
        }

        // debug
        Debug.DrawRay(wallCheck.position, transform.right * 0.1f, Color.red);
    }

    //funcao para virar o slime
    void Flip()
    {
        rotacao = !rotacao;

        Vector3 scale = transform.localScale; 
        scale.x *= -1;
        transform.localScale = scale;
    }


    public void Knockback(Vector2 direcao, float forca)         // Função para aplicar o knockback no slime
    {
        rb.AddForce(direcao * forca, ForceMode2D.Impulse);      
    }

    // Detecta colisao com outro slime 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Slime"))
        {
            speed *= -1;
            Flip();
        }
    }
}
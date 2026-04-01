using System.Data.Common;
using UnityEngine;

public class jogador : MonoBehaviour
{
    Rigidbody2D rbPlayer;
    [SerializeField] float speed = 5f;

    



    private void Awake()
    {
        // GetComponent le o componente dentro de jogador
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        rbPlayer.linearVelocity = new Vector2(xMove * speed, rbPlayer.linearVelocity.y);

        if (xMove > 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        else if (xMove < 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        
    }
}

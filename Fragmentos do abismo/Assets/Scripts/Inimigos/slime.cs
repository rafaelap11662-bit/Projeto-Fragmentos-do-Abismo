using UnityEngine;

public class slime : MonoBehaviour
{
    public float speed;
    public bool ground = true;

    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;

    public bool rotacao;

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

    void Flip()
    {
        rotacao = !rotacao;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
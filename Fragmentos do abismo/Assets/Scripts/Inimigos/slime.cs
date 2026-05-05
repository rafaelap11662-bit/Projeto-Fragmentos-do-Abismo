using UnityEngine;

public class slime : MonoBehaviour
{
    public float speed;
    public bool ground = true;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool rotacao;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        ground = Physics2D.Linecast(groundCheck.position, transform.position, groundLayer);
    

        if(ground == false)
        {
            speed *= -1;
        }

        if(speed > 0 && !rotacao)
        {
            Flip();
        }
        else if(speed < 0 && rotacao)
        {
            Flip();
        }
    }

    void Flip()
    {
        rotacao = !rotacao;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

}

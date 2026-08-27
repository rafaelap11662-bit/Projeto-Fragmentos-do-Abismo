using Unity.VisualScripting;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    public float speed;
    public bool ground = true;
    public SpriteRenderer sprite;

    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;

    public Transform currentTarget;
    public Transform targetA;
    public Transform targetB;

    [SerializeField] private float visao;



    void Start()
    {
        currentTarget = targetA;
    }

    void Update()
    {
        Move();
    }

     void Move()
    {
        // movimento
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        
        // detecta chão
        ground = Physics2D.Linecast( groundCheck.position, transform.position, groundLayer);
        
        // detecta parede
        bool wall = Physics2D.Raycast( wallCheck.position, transform.right, 0.1f, groundLayer);
        
        if(currentTarget == targetA && transform.position.x == targetA.position.x)
        {
            currentTarget = targetB;
        }
        else if(currentTarget == targetB && transform.position.x == targetB.position.x)
        {
            currentTarget = targetA;
        }
        

        if (transform.position.x > currentTarget.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        } 
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visao);
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private float perseguicao = 3f;
    [SerializeField] private float speed;
    [SerializeField] private bool ground = true;
    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private Transform currentTarget;
    [SerializeField] private Transform targetA;
    [SerializeField] private Transform targetB;
    [SerializeField] private Transform alvo;
    [SerializeField] private float visao;
    

    void Start()
    {
        currentTarget = targetA;
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
        transform.position = Vector2.MoveTowards(posicaoAtual, posicaoAlvo, perseguicao * Time.deltaTime);
        MudarDirecao(alvo);
    }

    private void MudarDirecao(Transform destino)
    {
        if (transform.position.x > destino.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
}

using UnityEngine;

public class slime : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool ground = true;
    [SerializeField] private bool rotacao;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Som do Slime")]
    [SerializeField] private AudioClip somAndando;
    [SerializeField] private float volume = 0.3f;
    [SerializeField] private float distanciaMinima = 3f;
    [SerializeField] private float distanciaMaxima = 8f;


    private MorteInimigo morteInimigo;
    private AudioSource audioSlime;


    private void Awake()
    {
        morteInimigo = GetComponent<MorteInimigo>();
    }

    private void Start()
    {
        if (somAndando != null && AudioManager.Instance != null)
        {
            audioSlime = AudioManager.Instance.TocarSomObjetos(somAndando, transform, volume, distanciaMinima, distanciaMaxima);
        }
    }

    private void Update()
    {
        if (morteInimigo != null && morteInimigo.morto == true)
        {
            PararSom();
            return;
        }

        // movimento
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // detecta chão
        ground = Physics2D.Linecast( groundCheck.position, transform.position, groundLayer);

        // detecta parede
        bool wall = Physics2D.Raycast( wallCheck.position, transform.right, 0.2f, groundLayer);

        
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
        Debug.DrawRay(wallCheck.position, transform.right * 0.2f, Color.red);
    }

    //funcao para virar o slime
    private void Flip()
    {
        rotacao = !rotacao;

        Vector3 scale = transform.localScale; 
        scale.x *= -1;
        transform.localScale = scale;
    }


    // Detecta colisao com outro slime 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Slime"))
        {
            speed *= -1;
            Flip();
        }
    }
    
    private void PararSom()
    {
        if (audioSlime != null)
        {
            audioSlime.Stop();
            Destroy(audioSlime.gameObject);
            audioSlime = null;
        }
    }

    private void OnDestroy()
    {
        PararSom();
    }
}
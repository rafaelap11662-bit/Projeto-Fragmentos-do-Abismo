using UnityEngine;

public class MorteInimigo : MonoBehaviour
{
    private Demonoide demonoide;

    private Collider2D col;
    private Rigidbody2D rb;
    [SerializeField] int vidaAtual = 2;
    

    [Header("Animação")]
    [SerializeField] Animator animator;
    [SerializeField] string morteTrigger;

    [Header("Sons")]
    [SerializeField] AudioClip somHit;
    [SerializeField] AudioClip somMorte;

    [Header("Demonoide")]
    [SerializeField]private float chanceEsquiva = 0.3f;

    [Header("Slime")]
    public bool morto = false;


    private void Awake()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        demonoide = GetComponent<Demonoide>();
    }

    public bool danoInimigo(int dano)
    {

        if (demonoide != null && Random.value <= chanceEsquiva)
        {
            demonoide.Esquivar();
            return false;
        }

        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            Morrer();
        }
        else
        {
            AudioManager.Instance.TocarSomInimigo(somHit);
        }
        return true;
    }

    private void Morrer()
    {
        morto = true; 

        AudioManager.Instance.TocarSomInimigo(somMorte);

        rb.bodyType = RigidbodyType2D.Kinematic;
        col.enabled = false;
        animator.SetTrigger(morteTrigger);

        Destroy(gameObject, 0.8f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lava"))
        {
            danoInimigo(100);
        }
    }
}
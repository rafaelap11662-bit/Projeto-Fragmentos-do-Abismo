using UnityEngine;

public class MorteInimigo : MonoBehaviour
{
    private Collider2D col;
    private Rigidbody2D rb;
    [SerializeField] int vidaAtual = 2;
    

    [Header("Animação")]
    [SerializeField] Animator animator;
    [SerializeField] string morteTrigger;

    [Header("Sons")]
    [SerializeField] AudioClip somHit;
    [SerializeField] AudioClip somMorte;
    

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void danoInimigo(int dano)
    {
        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            Morrer();
        }
        else
        {
            AudioManager.Instance.TocarSomInimigo(somHit);
        }
    }

    private void Morrer()
    {
        AudioManager.Instance.TocarSomInimigo(somMorte);

        rb.bodyType = RigidbodyType2D.Dynamic;
        col.enabled = false;
        animator.SetTrigger(morteTrigger);

        Destroy(gameObject, 0.8f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lava"))
        {
            danoInimigo(2);
        }
    }
}
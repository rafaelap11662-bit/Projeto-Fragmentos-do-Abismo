using UnityEngine;

public class MorteInimigo : MonoBehaviour
{
    private Collider2D col;
    private Rigidbody2D rb;
    [SerializeField] int vidaAtual = 2;
    

    [Header("Animação")]
    [SerializeField] Animator animator;
    [SerializeField] string morteTrigger;

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
    }

    void Morrer()
    {
        rb.bodyType = RigidbodyType2D.Static;
        col.enabled = false;
        animator.SetTrigger(morteTrigger);

        Destroy(gameObject, 0.8f);
    }
}
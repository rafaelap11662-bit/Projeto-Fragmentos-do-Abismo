using UnityEngine;

public class KBInimigo : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Knockback")]
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackUp;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void AplicarKnockback(Vector2 direcao)
    {
        rb.linearVelocity = Vector2.zero;

        Vector2 forca = new Vector2( direcao.x * knockbackForce, knockbackUp);

        rb.AddForce(forca, ForceMode2D.Impulse);
    }
}
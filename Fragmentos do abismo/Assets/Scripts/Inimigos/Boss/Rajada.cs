using UnityEngine;

public class Rajada : MonoBehaviour
{
    [SerializeField] private float Velocidade;
    Boss boss;

    private float direcao;

    public void ConfigurarDirecao(float novaDirecao, Boss bossCriador)
    {
        direcao = novaDirecao;
        boss = bossCriador;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * direcao * Velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss.CausarDano(collision);

            Destroy(gameObject, 3f);
        }
    }
}

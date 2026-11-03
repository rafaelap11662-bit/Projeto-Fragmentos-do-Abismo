using UnityEngine;

public class Rajada : MonoBehaviour
{
    Boss boss;
    private float direcao;

    [SerializeField] private float Velocidade;
    [SerializeField] private AudioClip somRajada;


    public void ConfigurarDirecao(float novaDirecao, Boss bossCriador)
    {
        direcao = novaDirecao;
        boss = bossCriador;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * direcao * Velocidade * Time.deltaTime);
        
    }

    private void Start()
    {
        if (somRajada != null && AudioManager.Instance != null)
            AudioManager.Instance.TocarSomBoss(somRajada);
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

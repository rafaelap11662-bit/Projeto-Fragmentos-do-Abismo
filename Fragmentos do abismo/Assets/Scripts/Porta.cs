using UnityEngine;
using UnityEngine.SceneManagement;


public class Porta : MonoBehaviour
{
    [Header("Próxima fase")]
    [SerializeField] private int check;
    [SerializeField] private string nomeProximaFase;

    [Header("Sprites da porta")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite portaFechada;
    [SerializeField] private Sprite portaAberta;

    private bool estaAberta = false;

    private void Start()
    {
        spriteRenderer.sprite = portaFechada;
    }

    private void Update()
    {
        if (!estaAberta && GameController.instance.totalScore >= check)
        {
            AbrirPorta();
        }
    }

    private void AbrirPorta()
    {
        estaAberta = true;
        spriteRenderer.sprite = portaAberta;

        Debug.Log("Porta aberta!");
    }

    void OnTriggerEnter2D(Collider2D collider) // detecta a se o player tem a quantidade de Fragmentos necessarios para passar de fase
    {
        if (collider.gameObject.tag == "Player")
        {
            if (estaAberta)
            {
                SceneManager.LoadScene(nomeProximaFase);
            }
            else
            {
                Debug.Log("Voce precisa de " + check + " fragmentos para conseguir passar");
            }
        }
    }
}

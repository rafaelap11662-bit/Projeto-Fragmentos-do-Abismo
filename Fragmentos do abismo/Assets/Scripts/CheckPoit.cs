using UnityEngine;

public class CheckPoit : MonoBehaviour
{
    [Header("Tocha")]
    [SerializeField] private Animator animTocha;

    [Header("Sons da Tocha")]
    [SerializeField] private AudioClip somTochaContinuo;
    [SerializeField] private AudioClip somTochaAtivacao;
    [SerializeField] private float volume;
    [SerializeField] private float distanciaMinima;
    [SerializeField] private float distanciaMaxima;

    private bool ativado;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ativado) 
            return;
        
        if (collision.gameObject.tag == "Player")
        {
            ativado = true;
            GameController.instance.checkpointAtual = transform;  

            if (animTocha != null)
            {
                animTocha.SetTrigger("Acender");

                if (somTochaAtivacao != null && somTochaContinuo != null)
                {
                    AudioManager.Instance.TocarSomDoObjeto(somTochaAtivacao);
                    AudioManager.Instance.TocarSomObjetos( somTochaContinuo, transform, volume, distanciaMinima, distanciaMaxima);
                }
                

            }

            Debug.Log("Checkpoint ativado!");
        }
    }
}

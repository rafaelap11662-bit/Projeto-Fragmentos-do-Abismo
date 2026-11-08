using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class NarrativaFinal : MonoBehaviour
{
    [Header("Narrativa")]
    [SerializeField] private DialogData narrativa;
    [SerializeField] private AnimacaoTexto animacaoTexto;

    [Header("Tempos")]
    [SerializeField] private float tempo = 3f;

    [Header("Proximo painel")]
    [SerializeField] private GameObject proximoPainel;

    private int indiceAtual = 0;
    [SerializeField] private bool ManterTexto;

    void Start()
    {
       StartCoroutine(ExibirNarrativa()); 
    }

    IEnumerator ExibirNarrativa()
    {
        while (indiceAtual < narrativa.talkScript.Count)
        {
            string texto = narrativa.talkScript[indiceAtual].texto;

            bool terminou = false;

            animacaoTexto.TypeFinished += () => terminou = true;
            animacaoTexto.ConfigurarTexto(texto);

            yield return new WaitUntil(() => terminou);

            animacaoTexto.TypeFinished = null;

            yield return new WaitForSeconds(tempo);
            
            if (!ManterTexto)
            {
                animacaoTexto.textObject.text = "";
            }
            

            indiceAtual++;
        }

        if (proximoPainel != null)
        {
            proximoPainel.SetActive(true);
        }
    }
}

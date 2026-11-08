using System.Collections;
using UnityEngine;

public class NarrativaFinal : MonoBehaviour
{
    [Header("Narrativa")]
    [SerializeField] private DialogData narrativa;
    [SerializeField] private AnimacaoTexto animacaoTexto;

    [Header("Tempos")]
    [SerializeField] private float tempo = 3f;

    private int indiceAtual = 0;

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

            animacaoTexto.textObject.text = "";

            indiceAtual++;
        }
    }
}

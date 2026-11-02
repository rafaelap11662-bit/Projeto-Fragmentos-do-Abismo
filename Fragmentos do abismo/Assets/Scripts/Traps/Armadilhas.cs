using UnityEngine;

public class Armadilhas : MonoBehaviour
{
    [Header("Sons da armadilha")]
    [SerializeField] private AudioClip somAtivacao;
    [SerializeField] private bool iniciarSomAutomaticamente;

    [Header("Configurações de áudio")]
    [SerializeField, Range(0f, 1f)] private float volume = 0.001f;
    [SerializeField] private float distanciaMinima = 10f;
    [SerializeField] private float distanciaMaxima = 20f;

    [Header("Som continuo (FireBox)")]
    [SerializeField] private Transform pontoDoSomFire;
    [SerializeField] private AudioClip somFireBox;

    private AudioSource audioAtual;

    private void Start()
    {
        if (iniciarSomAutomaticamente && somAtivacao != null)
        {
            audioAtual = AudioManager.Instance.TocarSomArmadilhaContinua( somAtivacao, transform, volume, distanciaMinima, distanciaMaxima);
        }
    }


    public void IniciarSomContinuo()
    {
        if (audioAtual != null && audioAtual.isPlaying)
            return;

        if (AudioManager.Instance != null && somFireBox != null && pontoDoSomFire != null)
        {
            audioAtual = AudioManager.Instance.TocarSomArmadilhaContinua( somFireBox,pontoDoSomFire,volume,distanciaMinima,distanciaMaxima);
        }
    }

    public void PararSomContinuo()
    {
        if (audioAtual != null)
        {
            audioAtual.Stop();
            Destroy(audioAtual.gameObject);
            audioAtual = null;
        }
    }

    public void TocarSomAtivacao()
    {
        if (AudioManager.Instance != null && somAtivacao != null)
        {
            AudioManager.Instance.TocarSomArmadilha(somAtivacao,transform.position);
        }
    }
}
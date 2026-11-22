using UnityEngine;

public class Lava : MonoBehaviour
{
    [Header("Sons da Lava")]
    [SerializeField] private AudioClip somAtivacao;

    [Header("Configurações de áudio")]
    [SerializeField, Range(0f, 1f)] private float volume = 0.001f;
    [SerializeField] private float distanciaMinima = 5f;
    [SerializeField] private float distanciaMaxima = 15f;


    
    private void Start()
    {
        if (somAtivacao != null)
        {
            Debug.Log("Posição da Lava: " + transform.position);
            AudioManager.Instance.TocarSomArmadilhaContinua( somAtivacao, transform, volume, distanciaMinima, distanciaMaxima);
        }
    }
}

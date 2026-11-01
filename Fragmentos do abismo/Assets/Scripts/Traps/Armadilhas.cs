using UnityEngine;

public class Armadilhas : MonoBehaviour
{
    [Header("Sons da armadilha")]
    [SerializeField] private AudioClip somAtivacao;

    public void TocarSomAtivacao()
    {
        Debug.Log("SOM DO ESPETO FOI CHAMADO!");
        if (AudioManager.Instance != null && somAtivacao != null)
        {
            AudioManager.Instance.TocarSomArmadilha(somAtivacao,transform.position);
        }
    }
}
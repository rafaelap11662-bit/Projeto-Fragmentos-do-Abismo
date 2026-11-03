using Unity.VisualScripting;
using UnityEngine;

public class Explosao : MonoBehaviour
{
    [SerializeField] private AudioClip somExplosao;
    
    public void ExplosaoSom()
    {
        if (somExplosao != null && AudioManager.Instance != null)
            AudioManager.Instance.TocarSomBoss(somExplosao);
    }

}

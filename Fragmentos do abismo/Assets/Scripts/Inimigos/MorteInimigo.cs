using UnityEngine;

public class MorteInimigo : MonoBehaviour
{
    [SerializeField] int vidaAtual = 2;
    

    [Header("Animação")]
    [SerializeField] Animator animator;
    [SerializeField] string morteTrigger;


    public void danoInimigo(int dano)
    {
        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        

        animator.SetTrigger(morteTrigger);

        Destroy(gameObject, 1f);
    }
}
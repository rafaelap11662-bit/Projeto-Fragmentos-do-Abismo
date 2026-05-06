using UnityEngine;

public class MorteInimigo : MonoBehaviour
{
    [SerializeField] int vidaAtual;

    
    public void danoInimigo(int dano)
    {
        vidaAtual -= dano;
        if(vidaAtual <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

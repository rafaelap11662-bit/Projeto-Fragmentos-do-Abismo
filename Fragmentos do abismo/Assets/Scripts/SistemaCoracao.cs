using UnityEngine;
using UnityEngine.UI;

public class SistemaCoracao : MonoBehaviour
{
    public int vida;            //Quantidade de vida do jogador
    public int vidaMaxima;      //Quantidade de corações jogador tem.

    public Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
       CoracaoLogica();
    }

    void CoracaoLogica()
    {
        vida = Mathf.Clamp(vida, 0, vidaMaxima);

        for (int i = 0; i < coracao.Length; i++)
        {
            coracao[i].sprite = (i < vida) ? cheio : vazio;
            coracao[i].enabled = (i < vidaMaxima);
        }
    }
}

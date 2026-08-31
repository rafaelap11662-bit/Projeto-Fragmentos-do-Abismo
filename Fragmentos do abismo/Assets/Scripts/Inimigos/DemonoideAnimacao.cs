using UnityEngine;

public class DemonoideAnimacao : MonoBehaviour
{
    private Demonoide demonoide;

    void Start()
    {
        demonoide = GetComponentInParent<Demonoide>();
    }

    public void AtacarPlayer()
    {
        demonoide.AtacarPlayer();
        Debug.Log("Atacando player");
    }

    public void LiberarAtaque()
    {
        demonoide.liberarAtaque();
        Debug.Log("Ataque liberado");
    }
}
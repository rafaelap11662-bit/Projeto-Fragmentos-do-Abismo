using UnityEngine;

public class GoblinAnimacao : MonoBehaviour
{
    private Goblin goblin;

    void Start()
    {
        goblin = GetComponentInParent<Goblin>();
    }

    public void AtacarPlayer()
    {
        goblin.AtacarPlayer();
        Debug.Log("Atacando player");
    }

    public void LiberarAtaque()
    {
        goblin.liberarAtaque();
        Debug.Log("Ataque liberado");
    }
}
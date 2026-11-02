using UnityEngine;

public class GoblinAnimacao : MonoBehaviour
{
    private Goblin goblin;
    [SerializeField] private AudioClip somAtack;

    void Start()
    {
        goblin = GetComponentInParent<Goblin>();
    }

    public void AtacarPlayer()
    {
        goblin.AtacarPlayer();
        AudioManager.Instance.TocarSomInimigo(somAtack);
    }

    public void LiberarAtaque()
    {
        goblin.liberarAtaque();
    }
}
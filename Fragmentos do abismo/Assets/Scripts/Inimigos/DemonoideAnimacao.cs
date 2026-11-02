using UnityEngine;

public class DemonoideAnimacao : MonoBehaviour
{
    private Demonoide demonoide;
    [SerializeField] private AudioClip somAtack;

    void Start()
    {
        demonoide = GetComponentInParent<Demonoide>();
    }

    public void AtacarPlayer()
    {
        demonoide.AtacarPlayer();
        AudioManager.Instance.TocarSomInimigo(somAtack);
    }

    public void LiberarAtaque()
    {
        demonoide.liberarAtaque();
    }
}
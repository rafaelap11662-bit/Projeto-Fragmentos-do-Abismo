using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Fontes de áudio")]
    [SerializeField] private AudioSource musica;
    [SerializeField] private AudioSource backGround;
    [SerializeField] private AudioSource somJogador;
    [SerializeField] private AudioSource sfxJogador;
    [SerializeField] private AudioSource somInimigos;
    [SerializeField] private AudioSource somInterface;
    [SerializeField] private AudioSource somDoObjeto;

    [Header("Sons do jogador")]
    [SerializeField] private AudioClip somCorrida;
    [SerializeField] private AudioClip somPulo;
    [SerializeField] private AudioClip somPouso;
    [SerializeField] private AudioClip somHit;

    [Header("Sons do Background")]
    [SerializeField] private AudioClip somBackGround;



    private bool corridaBloqueada = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TocarSomJogador(AudioClip som)
    {
        somJogador.PlayOneShot(som);
    }

    public void TocarSomInimigo(AudioClip som)
    {
        somInimigos.PlayOneShot(som);
    }

    public void TocarSomInterface(AudioClip som)
    {
        somInterface.PlayOneShot(som);
    }

    public void TocarSomBackGround(AudioClip som)
    {
        backGround.PlayOneShot(som);
    }

    private void Start()
    {
        TocarBackGround();
    }

    
    // =========================== Funções para controlar o som do jogador =======================
    public void TocarCorrida()
    {
        if (!somJogador.isPlaying && somCorrida != null)
        {
            somJogador.clip = somCorrida;
            somJogador.loop = true;
            somJogador.Play();
        }
    }

    public void PararCorrida()
    {
        if (somJogador.isPlaying)
        {
            somJogador.Stop();
            somJogador.loop = false;
        }
    }

    public void BloquearCorrida()
    {
        corridaBloqueada = true;
        PararCorrida();
    }

    public void LiberarCorrida()
    {
        corridaBloqueada = false;
    }

    public void TocarPulo()
     {
        if (somPulo != null)
            sfxJogador.PlayOneShot(somPulo);
    }

    public void TocarPouso()
    {
        if (somPouso != null)
            sfxJogador.PlayOneShot(somPouso);
    }
    
    public void TocarHit()
    {
        sfxJogador.PlayOneShot(somHit);
    }





// =========================== Função para tocar o som do Background =======================

    public void TocarBackGround()
    {
        if (somBackGround != null)
        {
            backGround.clip = somBackGround;
            backGround.loop = true;
            backGround.Play();
        }
    }

// =========================== Função para parar o som do Inimigos ==========================

    public void TocarHitInimigo(AudioClip som)
    {
        if (som != null)
            somInimigos.PlayOneShot(som);
    }

    public void TocarMorteInimigo(AudioClip som)
    {
        if (som != null)
            somInimigos.PlayOneShot(som);
    }

// =========================== Função para tocar o som das Armadilhas e alguns Inimigos que utilizam DamagePlayer.cs ==========================

    public void TocarObjetos(AudioClip som)
    {
        somDoObjeto.PlayOneShot(som);
    }

// =========================== Função para tocar o som das Armadilhas ==========================

    public void TocarSomArmadilha(AudioClip som, Vector3 posicao)
    {
        if (som == null) return;

        GameObject objetoSom = new GameObject("SomArmadilha");
        objetoSom.transform.position = posicao;

        AudioSource fonte = objetoSom.AddComponent<AudioSource>();

        fonte.clip = som;
        fonte.spatialBlend = 1f;
        fonte.volume = 1f;
        fonte.minDistance = 10f;
        fonte.maxDistance = 20f;
        fonte.rolloffMode = AudioRolloffMode.Linear;
        fonte.dopplerLevel = 0f;

        fonte.Play();

        Debug.Log("Áudio reproduzindo: " + fonte.isPlaying);
        Debug.Log("Volume: " + fonte.volume);
        Debug.Log("Clip: " + fonte.clip.name);
        Destroy(objetoSom, som.length + 0.2f);
    }
}
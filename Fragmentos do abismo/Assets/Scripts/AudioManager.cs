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
    [SerializeField] private AudioSource somBoss;

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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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

// =========================== Função para tocar o som das Armadilhas ==========================

    public void TocarSomArmadilha( AudioClip som, Vector3 posicao, float volume = 1f, float distanciaMinima = 10f, float distanciaMaxima = 20f)
    {
        if (som == null) return;

        GameObject objetoSom = new GameObject("SomArmadilha");
        objetoSom.transform.position = posicao;

        AudioSource fonte = objetoSom.AddComponent<AudioSource>();

        fonte.clip = som;
        fonte.spatialBlend = 1f;
        fonte.volume = volume;
        fonte.minDistance = distanciaMinima;
        fonte.maxDistance = distanciaMaxima;
        fonte.rolloffMode = AudioRolloffMode.Linear;
        fonte.dopplerLevel = 0f;

        fonte.Play();

        Destroy(objetoSom, som.length + 0.2f);
    }

    public AudioSource TocarSomArmadilhaContinua(AudioClip som, Transform posicao, float volume = 1f, float distanciaMinima = 10f, float distanciaMaxima = 20f)
    {
        GameObject objetoSom = new GameObject("SomContinuo");
        objetoSom.transform.SetParent(posicao);
        objetoSom.transform.localPosition = Vector3.zero;

        AudioSource fonte = objetoSom.AddComponent<AudioSource>();

        fonte.clip = som;
        fonte.loop = true;
        fonte.playOnAwake = false;
        fonte.spatialBlend = 1f;
        fonte.volume = volume;
        fonte.minDistance = distanciaMinima;
        fonte.maxDistance = distanciaMaxima;
        fonte.rolloffMode = AudioRolloffMode.Linear;
        fonte.dopplerLevel = 0f;

        fonte.Play();

        return fonte;

    }

    // =========================== Função para tocar o som da UI (Interface) ==========================

    public void TocarCaixaTexto(AudioClip som)
    {
        somInterface.PlayOneShot(som);
    }

    // =========================== Função para pausar o som ==========================

    public void PausarSonsContinuos(bool pausar)
    {
        AudioSource[] sons = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource som in sons)
        {
            if (som.loop)
            {
                if (pausar)
                    som.Pause();
                else
                    som.UnPause();
            }
        }
    }

    //========================== Função para tocar o som do Boss ==========================

    public void TocarMovimentoBoss(AudioClip som)
    {
        if (som == null || somBoss == null)
            return;

        if (somBoss.clip == som && somBoss.isPlaying)
            return;

        somBoss.Stop();
        somBoss.clip = som;
        somBoss.loop = true;
        somBoss.Play();   
    }

    public void PararMovimentoBoss()
    {
        if (somBoss != null)
            somBoss.Stop();
    }

    public void TocarSomBoss(AudioClip som)
    {
        if (som != null && somBoss != null)
            somBoss.PlayOneShot(som);
    }
}
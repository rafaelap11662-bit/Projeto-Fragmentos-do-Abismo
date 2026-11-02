using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class FallBlock : MonoBehaviour
{
    private Animator anim;
    private Collider2D col;
    private SpriteRenderer sr;

    private bool ativado = false;

    [SerializeField] private float tempoParaVoltar;

    [Header("Sons da armadilha")]
    [SerializeField] private AudioClip SomFallBlock;

    [Header("Configurações de áudio")]
    [SerializeField, Range(0f, 1f)] private float volume = 1f;
    [SerializeField] private float distanciaMinima = 10f;
    [SerializeField] private float distanciaMaxima = 20f;


    void Awake()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collider2D)
    {
        if(collider2D.gameObject.tag == "Player" && !ativado)
        {
            anim.SetTrigger("FallBlock");
            AudioManager.Instance.TocarSomArmadilha(SomFallBlock, transform.position, volume, distanciaMinima, distanciaMaxima);
            ativado = true;
        }
    }

    public void DesativarColisor() //animator event
    {
        col.enabled = false;
    }

    public void DesativarSprite() //animator event
    {
        sr.enabled = false;
        anim.SetTrigger("IntBlock");        
        
        StartCoroutine(VoltarBloco());
    }


    private IEnumerator VoltarBloco()
    {
        yield return new WaitForSeconds(tempoParaVoltar);

        col.enabled = true;
        sr.enabled = true;

        ativado = false;
    }








}

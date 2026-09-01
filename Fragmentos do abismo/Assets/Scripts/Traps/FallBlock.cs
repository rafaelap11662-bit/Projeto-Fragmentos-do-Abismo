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

    void Awake()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collider2D)
    {
        if(collider2D.gameObject.tag == "Player")
        {
            anim.SetTrigger("FallBlock");

            ativado = true;
        }
    }

    public void DesativarColisor()
    {
        col.enabled = false;
    }

    public void DesativarSprite()
    {
        sr.enabled = false;
        anim.SetTrigger("IntBlock");        StartCoroutine(VoltarBloco());
    }


    private IEnumerator VoltarBloco()
    {
        yield return new WaitForSeconds(tempoParaVoltar);

        col.enabled = true;
        sr.enabled = true;

        
        ativado = false;
    }








}

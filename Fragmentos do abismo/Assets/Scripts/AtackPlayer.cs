using UnityEngine;

public class AtackPlayer : MonoBehaviour
{
    private bool atacando;
    public Animator animator;

    public Transform ataquePoint;
    public float ataqueRanger = 0.5f;
    public LayerMask inimigoLayers;


    void Update()
    {
        atacando = Input.GetKeyDown(KeyCode.K);
        if(atacando == true)
        {
            Ataque();
        }
    }


    void Ataque()
    {
        // animação de ataque do jogador
        animator.SetTrigger("Atack01");

        // Ranger de ataque do jogador
        Collider2D[] hitInimigos = Physics2D.OverlapCircleAll(ataquePoint.position, ataqueRanger, inimigoLayers);

        foreach(Collider2D inimigo in hitInimigos) //Dano que o jogador da no inimigo
        {
            inimigo.GetComponent<MorteInimigo>().danoInimigo(1);
        }
    }
   
    // Para conseguir ver o AtaquePoint
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ataquePoint.position, ataqueRanger);
    }*/
}

using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int dano;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<jogador>().receberDano(dano); 
        }
    }
}

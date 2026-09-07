using UnityEngine;

public class Chave : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.instance.temChave = true;
            Destroy(gameObject);
         }
    }
}

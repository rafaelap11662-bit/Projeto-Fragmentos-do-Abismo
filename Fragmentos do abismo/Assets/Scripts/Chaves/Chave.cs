using Unity.VisualScripting;
using UnityEngine;

public class Chave : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private DialogData chave;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.instance.temChave = true;
            SistemaDialog.Instance.StartDialog(chave);
            anim.SetTrigger("ChaveColetada");
            
            Destroy(gameObject, 0.6f);
         }
    }
}

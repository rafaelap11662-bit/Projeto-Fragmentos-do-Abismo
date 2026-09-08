using UnityEngine;

public class PortaChave : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private DialogData dialogData;
    [SerializeField] private DialogData fragChaveColetada;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.CompareTag("Player"))
            {
                return;
            }
            if (GameController.instance.temChave)
            {
                anim.SetTrigger("PortaFim");
                SistemaDialog.Instance.StartDialog(fragChaveColetada);
                Destroy(gameObject, 1.9f);
            }
            else
            {
                SistemaDialog.Instance.StartDialog(dialogData);
            }   

        }
    }
}

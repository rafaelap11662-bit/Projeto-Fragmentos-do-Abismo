using UnityEngine;

public class PortaChave : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private DialogData dialogData;
    [SerializeField] private DialogData fragChaveColetada;

    private bool chaveColetada = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameController.instance.temChave && !chaveColetada)
            {
                chaveColetada = true;
                anim.SetTrigger("PortaFim");
                SistemaDialog.Instance.StartDialog(fragChaveColetada);
                Destroy(gameObject, 1.9f);
            }
            if (!GameController.instance.temChave)
            {
                SistemaDialog.Instance.StartDialog(dialogData);
            }   

        }
    }
}

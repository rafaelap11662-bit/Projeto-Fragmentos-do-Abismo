using UnityEngine;

public class PortaChave : MonoBehaviour
{
    public DialogData dialogData;

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
                Destroy(gameObject);
            }
            else
            {
                SistemaDialog.Instance.StartDialog(dialogData);
            }   

        }
    }
}

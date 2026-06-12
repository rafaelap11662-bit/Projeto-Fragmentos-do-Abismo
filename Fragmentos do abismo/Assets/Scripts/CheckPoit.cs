using UnityEngine;

public class CheckPoit : MonoBehaviour
{
    private bool ativado;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ativado) 
            return;
        
        if (collision.gameObject.tag == "Player")
        {
            ativado = true;
            GameController.instance.checkpointAtual = transform;  

            Debug.Log("Checkpoint ativado!");
        }
    }
}

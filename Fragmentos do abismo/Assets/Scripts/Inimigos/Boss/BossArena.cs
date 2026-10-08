using UnityEngine;

public class BossArena : MonoBehaviour
{
    [SerializeField] private GameObject spawnBoss;
    [SerializeField] private GameObject barreiraBoss;
    [SerializeField] private Transform checkPointBoss;


    private bool batalhaIniciada = false;  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        return;

        if (batalhaIniciada)
            return;

        batalhaIniciada = true;

        barreiraBoss.SetActive(true);
        spawnBoss.SetActive(true); 

        GameController.instance.checkpointAtual = checkPointBoss; 
    }
}

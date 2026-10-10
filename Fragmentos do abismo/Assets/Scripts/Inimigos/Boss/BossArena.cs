using UnityEngine;

public class BossArena : MonoBehaviour
{
    [Header("OBJETOS DA ARENA")]
    [SerializeField] private GameObject spawnBoss;
    [SerializeField] private GameObject barreiraBoss;
    [SerializeField] private Transform checkPointBoss;

    [Header("BOSS")]
    [SerializeField] private Boss boss;

    private bool batalhaIniciada = false;  


    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (!collision.CompareTag("Player"))
            return;

        if (batalhaIniciada)
            return;

        batalhaIniciada = true;

        GameController.instance.bossArenaAtiva = this;
        GameController.instance.checkpointAtual = checkPointBoss; 

        barreiraBoss.SetActive(true);
        spawnBoss.SetActive(true); 
    }

    public void AbrirBarreira()
    {
        barreiraBoss.SetActive(false);

        if (GameController.instance != null) 
        {
            GameController.instance.bossArenaAtiva = null;
        }
    }

    public void ReiniciarBatalha()
    {
        Debug.Log("ReiniciarBatalha foi chamado!");

        batalhaIniciada = true;

        barreiraBoss.SetActive(true);
        spawnBoss.SetActive(true); 

        GameController.instance.bossArenaAtiva = this;
        GameController.instance.checkpointAtual = checkPointBoss;

        if(boss != null)
        {
            Debug.Log("Referência do Boss encontrada!");
            boss.ReiniciarBoss();
        }
        else
        {
            Debug.Log("Referência do Boss encontrada!");
        }
    }

    public Transform GetCheckpointBoss()    
    {
        return checkPointBoss;
    }
}

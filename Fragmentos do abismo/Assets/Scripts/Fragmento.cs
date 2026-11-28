using UnityEngine;


public class Fragmento : MonoBehaviour
{
    private SpriteRenderer sr;
    private EdgeCollider2D pikup; 
    
    public GameObject coleta;                                   
    public int Score = 1;                                       
    public DialogData dialogData;                               

    [Header("=========== Fase 3 ===========")]
    [Header("Fragmento Final ")]
    [SerializeField] private bool fragmentoFinal;
    [SerializeField] private Boss boss;

    [SerializeField] private bool fragmentosFase3;
    [SerializeField] private BossArena bossArena;
    
    
    void Start()
    {  
        sr = GetComponent<SpriteRenderer>();
        pikup = GetComponent<EdgeCollider2D>();
    }


    void OnTriggerEnter2D(Collider2D collider)                
    {
        if(collider.gameObject.tag == "Player")
        {
            SistemaDialog.Instance.StartDialog(dialogData);     
            sr.enabled = false;                                 
            pikup.enabled = false;                              
            coleta.SetActive(true);                             

            GameController.instance.totalScore += Score;        
            GameController.instance.UpdateScoreText();          

            if (fragmentoFinal && boss != null)
                {
                    boss.AbrirPortal();
                }
            
            if (fragmentosFase3 && bossArena != null)
            {
                bossArena.AbrirBarreira();
            }

            Destroy(gameObject, 0.3f);  
        }
    }
}

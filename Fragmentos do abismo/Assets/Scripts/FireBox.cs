using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject Fire;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FogoAtivado();
        FogoDesativado(); 
    }

    void FogoAtivado()
    {
        Fire.SetActive(true);
    }
    
    void FogoDesativado()
    {
        Fire.SetActive(false);
    }
}

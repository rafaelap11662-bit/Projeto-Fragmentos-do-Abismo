using Unity.VisualScripting;
using UnityEditor.Build.Content;
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

    // Update is called once per frame
    void Update()
    {
        
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

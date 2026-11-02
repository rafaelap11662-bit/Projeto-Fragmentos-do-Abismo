using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject Fire;
    [SerializeField] private Armadilhas armadilhas;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FogoAtivado();
        FogoDesativado(); 
    }

    void FogoAtivado()
    {
        Fire.SetActive(true);
        armadilhas.IniciarSomContinuo();
    }
    
    void FogoDesativado()
    {
        Fire.SetActive(false);
        armadilhas.PararSomContinuo();
    }
}

using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Porta : MonoBehaviour
{
    private CircleCollider2D circle;
    [SerializeField] private int check;
    [SerializeField] private string nomeProximaFase;

    void Start()
    {   
        circle = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider) // detecta a se o player tem a quantidade de Fragmentos necessarios para passar de fase
    {
        if (collider.gameObject.tag == "Player")
        {
            if(GameController.instance.totalScore == check)
            {
                SceneManager.LoadScene(this.nomeProximaFase);
            }
            else
            {
                Debug.Log("Voce precisa de " + check + " fragmentos para conseguir passar");
            }
        }
    }
}

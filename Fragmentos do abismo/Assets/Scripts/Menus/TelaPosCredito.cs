using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaPosCredito : MonoBehaviour
{
    [SerializeField] private AudioClip somBotao;

    public void CarregarMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
        AudioManager.Instance.TocarSomInterface(somBotao);
    }
}

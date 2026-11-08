using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaPosCredito : MonoBehaviour
{
    public void CarregarMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}

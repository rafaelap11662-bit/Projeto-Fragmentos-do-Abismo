using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void IniciarJogo()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Fase1");
    }

    public void SairJogo()
    {
        Application.Quit();
        Debug.Log("Fecho o joguin");
    }

    public void ContinuarJogo()
    {
        string faseSalva = PlayerPrefs.GetString("FaseSalva", "Fase1");
        Time.timeScale = 1f;
        SceneManager.LoadScene(faseSalva);
    }
}

using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private AudioClip somBotao;

    public void IniciarJogo()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Fase1");
        AudioManager.Instance.TocarSomInterface(somBotao);
    }

    public void SairJogo()
    {
        Application.Quit();
        Debug.Log("Fecho o joguin");
        AudioManager.Instance.TocarSomInterface(somBotao);
    }

    public void ContinuarJogo()
    {
        string faseSalva = PlayerPrefs.GetString("FaseSalva", "Fase1");
        Time.timeScale = 1f;
        SceneManager.LoadScene(faseSalva);
        AudioManager.Instance.TocarSomInterface(somBotao);
    }
}

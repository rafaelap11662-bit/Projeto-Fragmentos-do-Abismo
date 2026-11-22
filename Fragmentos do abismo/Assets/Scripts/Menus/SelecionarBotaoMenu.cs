using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SelecionarBotaoMenu : MonoBehaviour
{
    [SerializeField] private GameObject primeiroBotao;

    private GameController gameController;
    private bool menuEstavaAberto;

    [Header("Botões dos painéis")]
    public GameObject botaoContinuar;
    public GameObject botaoReiniciar;

    void Awake()
    {
        gameController = GetComponent<GameController>();
    }

    void Update()
    {   
        if (EventSystem.current == null)
            return;

        bool menuAberto = gameController != null && ((gameController.painelPauseMenu != null && gameController.painelPauseMenu.activeInHierarchy) || (gameController.painelGameOver != null && gameController.painelGameOver.activeInHierarchy));

        if (menuAberto && !menuEstavaAberto)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        menuEstavaAberto = menuAberto;

        if (Gamepad.current == null)
            return;

        bool navegou = Gamepad.current.dpad.ReadValue().sqrMagnitude > 0 || Gamepad.current.leftStick.ReadValue().sqrMagnitude > 0.25f;

        if (!navegou)
            return;

        if (EventSystem.current.currentSelectedGameObject != null) 
            return;

        GameObject botaoInicial = primeiroBotao;
        
        if(gameController != null)
        {
            if (gameController.painelGameOver.activeInHierarchy)
                botaoInicial = botaoReiniciar;

            else if (gameController.painelPauseMenu.activeInHierarchy)
                botaoInicial = botaoContinuar;   
        }
    
        if (botaoInicial != null)
            EventSystem.current.SetSelectedGameObject(botaoInicial);
    }
}

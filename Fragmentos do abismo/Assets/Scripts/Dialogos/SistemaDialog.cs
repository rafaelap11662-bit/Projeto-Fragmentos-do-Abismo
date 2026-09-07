using UnityEngine;

public enum STATE
{
    DISABLED,
    WAITING,
    TYPING
}

public class SistemaDialog : MonoBehaviour
{
    public static SistemaDialog Instance;
    [SerializeField] jogador jogador;
    [SerializeField] AtackPlayer ataque;

    [Header("Referencias")]
    public DialogData dialogData;
    public GameObject dialogBox;
    [SerializeField] AnimacaoTexto typeText;

    private int currentText = 0; 
    private bool finished = false;  

    [SerializeField] private DialogUi dialogUi;

    STATE state;

    void Awake()
    {
        Instance = this;

        
        typeText.TypeFinished += OnTypeFinished;
    }

    void Start()
    {
        state = STATE.DISABLED; 
    }

    


    void Update()
    {
        if(state == STATE.DISABLED) return; 

        if(state == STATE.WAITING)
        {
            Waiting(); 
        }
        else if (state == STATE.TYPING)
        {
            Typing();
        }
    }



    public void StartDialog(DialogData dialog)
    {
        if(dialog == null)
        {
            Debug.LogWarning("Dialogo nulo");
            return;
        }

        if(dialog.talkScript == null || dialog.talkScript.Count == 0)
        {
            Debug.LogWarning("Dialogo sem falas");
            return;
        }

        dialogData = dialog;

        currentText = 0;
        finished = false;


        Next();
    }



    void Next()
    {
        if(currentText == 0)
        {
            dialogUi.Enable();
        }
        
        jogador.pararJogador();
        ataque.enabled = false;
        jogador.enabled = false;

        typeText.fullText = dialogData.talkScript[currentText++].texto; 

        if(currentText >= dialogData.talkScript.Count) finished = true;

        typeText.StartTyping();
        state = STATE.TYPING;
    }



    void OnTypeFinished()
    {
        state = STATE.WAITING;
    }



    void Waiting()
    {
        if(Input.GetKeyDown(KeyCode.Return))

        if (!finished)
        {
            Next();
        }
        else
        {
            dialogUi.Disable();
            state = STATE.DISABLED;
            jogador.enabled = true;    
            ataque.enabled = true;       
            currentText = 0;
            finished = false;
        }
        
    }



    void Typing()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            typeText.Skip();
            state = STATE.WAITING;
        }
    }
}

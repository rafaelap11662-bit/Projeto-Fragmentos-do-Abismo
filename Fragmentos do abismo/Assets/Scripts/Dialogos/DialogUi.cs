using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUi : MonoBehaviour
{
    Image caixaDialogo;
    TextMeshProUGUI dialogText;

    public float speed = 10f;
    bool open = false;

    [Header("Sons da armadilha")]
    [SerializeField] private AudioClip caixaTexto;
    [SerializeField] private AudioClip caixaTextoInverso;

    void Awake()
    {
        caixaDialogo = transform.GetChild(0).GetComponent<Image>();
        dialogText   = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        if (open)
        {
            caixaDialogo.fillAmount = Mathf.Lerp(caixaDialogo.fillAmount, 1, speed * Time.deltaTime);
            
        }
        else
        {
            caixaDialogo.fillAmount = Mathf.Lerp(caixaDialogo.fillAmount, 0, speed * Time.deltaTime);
        }
    }

    public void Enable()
    {
        caixaDialogo.fillAmount = 0;
        open = true;
        
        AudioManager.Instance.TocarCaixaTexto(caixaTexto);
        AudioManager.Instance.BloquearCorrida();
    }

    public void Disable()
    {
        open = false;
        dialogText.text = "";

        AudioManager.Instance.TocarCaixaTexto(caixaTextoInverso);
        AudioManager.Instance.LiberarCorrida();
    }
}

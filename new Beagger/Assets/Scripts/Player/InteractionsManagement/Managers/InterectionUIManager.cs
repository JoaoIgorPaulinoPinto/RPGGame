using TMPro;
using UnityEngine;

public class InteractionUIManager : MonoBehaviour
{
    public static InteractionUIManager Instance { get; private set; }

    [SerializeField]Animator animator;
    [SerializeField] GameObject UI;
    [SerializeField] TextMeshProUGUI lbl_funcName;

    private void Awake()
    {
        // Verifica se j� existe uma inst�ncia da classe
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Se sim, destrua este objeto
        }
        else
        {
            Instance = this; // Caso contr�rio, esta se torna a inst�ncia principal
            DontDestroyOnLoad(gameObject); // Opcional: faz com que este objeto n�o seja destru�do ao carregar uma nova cena
        }
    }

    public void ShowUI(GameObject target)
    {
        if (target.TryGetComponent<InteractableGameObject>(out InteractableGameObject i))
        {
            GeneralUIManager.Instance.animator.SetBool("UIInteract", true);
            UI.SetActive(true);
            lbl_funcName.text = i.interactionName;
        }
    }

    public void HideUI()
    {
        GeneralUIManager.Instance.animator.SetBool("UIInteract", false);
        UI.SetActive(false);
    }
}

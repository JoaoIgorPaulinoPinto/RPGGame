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
        // Verifica se já existe uma instância da classe
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Se sim, destrua este objeto
        }
        else
        {
            Instance = this; // Caso contrário, esta se torna a instância principal
            DontDestroyOnLoad(gameObject); // Opcional: faz com que este objeto não seja destruído ao carregar uma nova cena
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpSystem : MonoBehaviour
{
    [SerializeField]AudioSource audioSource;
    [SerializeField]AudioClip clipAlert;    
    [SerializeField]AudioClip clipMessage;
    [SerializeField]AudioClip clipInfo;

    public static PopUpSystem Instance { get; private set; }

    private void Awake()
    {      
        // Verifica se já existe uma instância do PlayerControlsManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destrói a nova instância se outra já existir
        }
        else
        {
            Instance = this; // Define esta como a instância ativa
            DontDestroyOnLoad(gameObject); // Mantém o objeto ao trocar de cena, se necessário
        }

    }

    [Header("UI")]
    [SerializeField] Sprite icon_alert;
    [SerializeField] Sprite icon_info;
    [SerializeField] Sprite icon_message;

    [Header("Managers")]
    [Space]
    [SerializeField] GameObject prefab;
    [SerializeField] Transform parent;

    public void SendMsg(string msg, MessageType type, float? timewait)
    {
        float time = timewait ?? 3f;
        PopUpCard card = Instantiate(prefab, parent).GetComponent<PopUpCard>();
       
        switch (type){
            case MessageType.Alert: card.SetValues(msg,icon_alert, time); audioSource.clip = clipAlert; break;
            case MessageType.Information: card.SetValues(msg, icon_info, time); audioSource.clip = clipMessage; break;
            case MessageType.Message: card.SetValues(msg, icon_message, time); audioSource.clip = clipInfo;  break;
        }
        card.TryGetComponent<Animator>(out Animator animator);
        //animator.SetBool("PopUp", true);
        audioSource.Play();
    }

}

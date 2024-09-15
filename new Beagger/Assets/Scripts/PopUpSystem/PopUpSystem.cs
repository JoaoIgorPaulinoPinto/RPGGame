using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class PopUpSystem : MonoBehaviour
{
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

    public void SendMsg(string msg, MessageType type, float timewait)
    {
        PopUpCard card = Instantiate(prefab, parent).GetComponent<PopUpCard>();
       
        switch (type){
            case MessageType.Alert: card.SetValues(msg,icon_alert, timewait); break;
            case MessageType.Information: card.SetValues(msg, icon_info, timewait); break;
            case MessageType.Message: card.SetValues(msg, icon_message, timewait); break;
        }
    }
}

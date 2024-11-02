using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dialog;
public class NPCDialog : InteractableGameObject, IInteractable
{
    public Dialog dialogInfo;   
    public QuestsData? questsData;
    [SerializeField]NPCData npcData;
    public void Interact()
    {
        PlayerControlsManager.Instance.realease = false;
        DialogSystem.Instance.SetData(dialogInfo, npcData, questsData);
        DialogSystem.Instance.StartDialog();
    }
}

using Pada1.BBCore.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    
public class BTReferences : MonoBehaviour
{
    public Transform agent;


    public bool ready;
    public bool inAction;

    public float movespeed;

    public List<Transform> availablePoints;

    public int index;

    public Transform pointTarget; 
    public NPCAction selectedAction;

    [System.Serializable]
    public class NPCAction
    {
        public NPCActionType actionType;
        public bool enable;
        public float duration;
        public NPCInteractionCompatibilities[] NPCInteractionCompatibilities;
    }

    public List<NPCAction> actions;
    public void SetSelectedAction(NPCActionType actionType, bool enable, float duration, NPCInteractionCompatibilities[] compatibilities)
    {
        selectedAction.actionType = actionType;
        selectedAction.enable = enable;
        selectedAction.duration = duration;
        selectedAction.NPCInteractionCompatibilities = compatibilities;
    }
}

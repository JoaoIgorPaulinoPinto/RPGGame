using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public bool inAction;
    public bool STOP;

    [System.Serializable]
    public class NPCAction
    {
        public NPCActionType actionType;
        public bool enable;

        [Range(5,20)]
        public float Maxduration;
        [Range(0,5)]
        public float Minduration;

        public float duration = 0;
        public NPCInteractionCompatibilities[] NPCInteractionCompatibilities;
    }

    public List<NPCAction> actions;
    public List<NPCAction> actionsCompatibles;

    NPCAction currentAction;

    public NPCMovimentation NPCMovimentation;

    public void StartInteraction(NPCInteractionCompatibilities type)
    {
        foreach (NPCAction action in actions) { 
            action.duration = Random.Range(action.Minduration, action.Maxduration);
        }
        FindInteraction(type);
    }
    void FindInteraction(NPCInteractionCompatibilities type)
    {
        foreach (var action in actions)
        {
            if (action.enable)
            {
                foreach (var item in action.NPCInteractionCompatibilities)
                {
                    if (item == type)
                    {
                        if (!actionsCompatibles.Contains(action)) { actionsCompatibles.Add(action); }
                    }
                }
            }
        }
        ChooseAction();
    }
    void ChooseAction()
    {
        currentAction = actionsCompatibles[Random.Range(0, actionsCompatibles.Count)];
        if (currentAction != null) { PlayAction(currentAction.actionType); }
    }

    public void PlayAction(NPCActionType actionType)
    {
        switch (actionType)
        {
            case NPCActionType.Talk: StartCoroutine(Talk()); break;
            case NPCActionType.Steal: StartCoroutine(Steal()); break;   
            case NPCActionType.Buy: StartCoroutine(Buy()); break;
            case NPCActionType.Sell: StartCoroutine(Sell()); break;
        }
    }
    void finishAction(Transform? t)
    {
        if (t == null)
        {
            actionsCompatibles.Clear();
            inAction = false;
            currentAction = null;
            NPCMovimentation.NextPointAfterAction(null);
            STOP = false;
        }
        else
        {

            actionsCompatibles.Clear();
            inAction = false;
            currentAction = null;
            STOP = false;
            NPCMovimentation.NextPointAfterAction(t);
        }
    }
    IEnumerator Talk()
    {
        inAction = true;
        NPCMovimentation.readyToGo = false;
        if (NPCMovimentation.currentTarget.GetComponent<NPCMovimentation>().inMoviment)
        {

            NPCMovimentation.currentTarget.GetComponent<NPCMovimentation>().currentTarget = transform;

            yield return new WaitForSeconds(currentAction.duration);
            finishAction(null);
        }
        else
        {
            finishAction(null);
        }
    }

    IEnumerator Steal()
    {
        inAction = true;
        NPCMovimentation.readyToGo = false;
        //comeco logica 

        // fim logica 
        yield return new WaitForSeconds(currentAction.duration);
        finishAction(null);

    }
    IEnumerator Buy()
    {
        inAction = true;
        NPCMovimentation.readyToGo = false;
        //comeco logica     

        // fim logica 
        yield return new WaitForSeconds(currentAction.duration);
        finishAction(null);

    }
    IEnumerator Sell()
    {
        inAction = true;
        NPCMovimentation.readyToGo = false;
        //comeco logica 

        // fim logica 
        yield return new WaitForSeconds(currentAction.duration);
        finishAction(null);

    }
}

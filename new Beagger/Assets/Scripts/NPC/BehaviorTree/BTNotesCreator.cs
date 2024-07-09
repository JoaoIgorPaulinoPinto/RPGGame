//using Pada1.BBCore;
//using Pada1.BBCore.Framework;
//using Pada1.BBCore.Tasks;
//using UnityEngine;
//using static BTReferences;
//using Pada1.BBEditor;

//// Movimento
//[Action("/NPC/Moviment/Walk")]
//public class Moviment : BasePrimitiveAction
//{
//    [InParam("Reference")]
//    public BTReferences references;

//    public override TaskStatus OnUpdate()
//    {
//        if (references.ready)
//        {
//            if (references.index >= references.defaultRoutinePoints.Count)
//            {
//                references.index = 0;
//                references.pointTarget = null;
                
//                //task completed
//            }

//            references.pointTarget = references.defaultRoutinePoints[references.index];

//            Transform target = references.pointTarget;
            
//            references.agent.position = Vector2.MoveTowards(references.agent.position, target.position, references.movespeed * Time.deltaTime);

//            if (Vector2.Distance(references.agent.position, target.position) <= 1)
//            {
//                Debug.Log(references.agent.name + " - Reached target point.");
//                references.index++;
//                return TaskStatus.COMPLETED;
//            }
//            else
//            {
//                //Debug.Log(references.agent.name + " - Moving towards target.");
//                return TaskStatus.RUNNING;
//            }
//        }
//        else
//        {
//            Debug.LogWarning(references.agent.name + " - Reference not ready.");
//            return TaskStatus.ABORTED;
//        }
//    }
//}

//// Ações
//[Action("/NPC/Actions/Talk")]
//public class ActionsTalk : BasePrimitiveAction
//{
//    [InParam("Reference")]
//    public BTReferences references;

//    private float elapsedTime;

//    public override void OnStart()
//    {
//        base.OnStart();
//        elapsedTime = 0f;

//        // Reiniciar a Behavior Tree ao iniciar a conversa
       
//    }
//    public override TaskStatus OnUpdate()
//    {
//        if (references.ready)
//        {
//            references.inAction = true;
//            BTReferences.NPCAction action = references.selectedAction;

//            Debug.Log(references.agent.name + " - Action: " + action.duration);
//            if (action != null)
//            {
//                if (elapsedTime >= action.duration)
//                {
//                    Debug.Log(references.agent.name + " - Action completed.");
//                    references.inAction = false;
//                    return TaskStatus.COMPLETED;
//                }
//                else
//                {
//                    if (Vector2.Distance(references.agent.position, references.pointTarget.position) < 3)
//                    {
//                        references.pointTarget.GetComponent<BTReferences>().SetNewMovimentTarget(references.agent);
//                        references.pointTarget.GetComponent<BTReferences>().selectedAction = references.selectedAction;
//                    }
//                    elapsedTime += Time.deltaTime;
//                    Debug.Log(references.agent.name + " - Action in progress. Elapsed time: " + elapsedTime);
//                    return TaskStatus.RUNNING;

//                }
//            }
//            else
//            {
//                Debug.LogWarning(references.agent.name + " - No valid action found.");
//                references.inAction = false;
//                return TaskStatus.FAILED;
//            }
//        }
//        else
//        {
//            Debug.LogWarning(references.agent.name + " - Reference not ready.");
//            references.inAction = false;
//            return TaskStatus.ABORTED;
//        }
//    }
//}


//[Action("/NPC/Actions/Steal")]
//public class ActionsSteal : BasePrimitiveAction
//{
//    [InParam("Reference")]
//    public BTReferences references;

//    private float elapsedTime;

//    public override void OnStart()
//    {
//        base.OnStart();
//        elapsedTime = 0f;
//    }

//    public override TaskStatus OnUpdate()
//    {
//        if (references.inAction)
//        {
//            references.inAction = true;
//            BTReferences.NPCAction foundAction = references.actions.Find(action => action.enable);

//            if (foundAction != null && foundAction.enable)
//            {
//                if (elapsedTime >= foundAction.duration)
//                {
//                    Debug.Log(references.agent.name + " - Action completed.");
//                    references.inAction = false;
//                    return TaskStatus.COMPLETED;
//                }
//                else
//                {
//                    elapsedTime += Time.deltaTime;
//                    Debug.Log(references.agent.name + " - Action in progress. Elapsed time: " + elapsedTime);
//                    return TaskStatus.RUNNING;
//                }
//            }
//            else
//            {
//                Debug.LogWarning(references.agent.name + " - No valid or enabled action found.");
//                references.inAction = false;
//                return TaskStatus.FAILED; // or TaskStatus.ABORTED, depending on your logic
//            }
//        }
//        else
//        {
//            Debug.LogWarning(references.agent.name + " - Reference not ready."); 
//            references.inAction = false;
//            return TaskStatus.ABORTED;
//        }
//    }
//}

//[Action("/NPC/Actions/Buy")]
//public class Buy : BasePrimitiveAction
//{
//    [InParam("Reference")]
//    public BTReferences references;

//    private float elapsedTime;

//    public override void OnStart()
//    {
//        base.OnStart();
//        elapsedTime = 0f;
//    }

//    public override TaskStatus OnUpdate()
//    {
//        if (references.inAction)
//        {
//            references.inAction = true;
//            BTReferences.NPCAction foundAction = references.actions.Find(action => action.enable);

//            if (foundAction != null && foundAction.enable)
//            {
//                if (elapsedTime >= foundAction.duration)
//                {
//                    Debug.Log(references.agent.name + " - Action completed.");
//                    references.inAction = false;
//                    return TaskStatus.COMPLETED;
//                }
//                else
//                {
//                    elapsedTime += Time.deltaTime;
//                    Debug.Log(references.agent.name + " - Action in progress. Elapsed time: " + elapsedTime);
//                    return TaskStatus.RUNNING;
//                }
//            }
//            else
//            {
//                Debug.LogWarning(references.agent.name + " - No valid or enabled action found.");
//                references.inAction = false;
//                return TaskStatus.FAILED; // or TaskStatus.ABORTED, depending on your logic
//            }
//        }
//        else
//        {
//            Debug.LogWarning(references.agent.name + " - Reference not ready.");
//            references.inAction = false;
//            return TaskStatus.ABORTED;
//        }
//    }
//}

//[Action("/NPC/Actions/Sell")]
//public class Sell : BasePrimitiveAction
//{
//    [InParam("Reference")]
//    public BTReferences references;

//    private float elapsedTime;

//    public override void OnStart()
//    {
//        base.OnStart();
//        elapsedTime = 0f;
        
//    }

//    public override TaskStatus OnUpdate()
//    {
//        if (references.inAction)
//        {
//            references.inAction = true;
//            BTReferences.NPCAction foundAction = references.actions.Find(action => action.enable);
//            Debug.Log("FUNDACTION________________ " + foundAction.actionType[0]);
//            if (foundAction != null && foundAction.enable)
//            {
//                if (elapsedTime >= foundAction.duration)
//                {
//                    Debug.Log(references.agent.name + " - Action completed.");
//                    references.inAction = false;
//                    return TaskStatus.COMPLETED;
//                }
//                else
//                {
//                    elapsedTime += Time.deltaTime;
//                    Debug.Log(references.agent.name + " - Action in progress. Elapsed time: " + elapsedTime);
//                    return TaskStatus.RUNNING;
//                }
//            }
//            else
//            {
//                Debug.LogWarning(references.agent.name + " - No valid or enabled action found.");
//                references.inAction = false;
//                return TaskStatus.FAILED; // or TaskStatus.ABORTED, depending on your logic
//            }
//        }
//        else
//        {
//            Debug.LogWarning(references.agent.name + " - Reference not ready.");
//            references.inAction = false;
//            return TaskStatus.ABORTED;
//        }
//    }
//}

//// Condições
//[Condition("NPC/Verifications/isTalk")]
//public class isTalk : ConditionBase
//{
//    [InParam("Reference")]
//    BTReferences references;

//    public override bool Check()
//    {
//        if (references.selectedAction != null)
//        {
//            for (int i = 0; i < references.selectedAction.actionType.Length; i++)
//            {
//                if (references.selectedAction.actionType[i] == NPCActionType.Talk)
//                {
//                    if (references.pointTarget.GetComponent<BTReferences>().inAction == false)
//                    {
//                        return true;
//                    }
//                    else return false;  

//                }
//            }
//        }
//        return false;
//    }
//}

//[Condition("NPC/Verifications/isSteal")]
//public class isSteal : ConditionBase
//{
//    [InParam("Reference")]
//    BTReferences references;

//    public override bool Check()
//    {
//        if (references.selectedAction != null)
//        {
//            for (int i = 0; i < references.selectedAction.actionType.Length; i++)
//            {
//                if (references.selectedAction.actionType[i] == NPCActionType.Steal)
//                {
//                    return true;
//                }
//            }
//        }
//        return false;
//    }
//}

//[Condition("NPC/Verifications/isBuy")]
//public class isBuy : ConditionBase
//{
//    [InParam("Reference")]
//    BTReferences references;

//    public override bool Check()
//    {
//        if (references.selectedAction != null)
//        {
//            for (int i = 0; i < references.selectedAction.actionType.Length; i++)
//            {
//                if (references.selectedAction.actionType[i] == NPCActionType.Buy)
//                {
//                    return true;
//                }
//            }
//        }
//        return false;
//    }
//}

//[Condition("NPC/Verifications/isSell")]
//public class isSell : ConditionBase
//{
//    [InParam("Reference")]
//    BTReferences references;

//    public override bool Check()
//    {
//        if (references.selectedAction != null)
//        {
//            for (int i = 0; i < references.selectedAction.actionType.Length; i++)
//            {
//                if (references.selectedAction.actionType[i] == NPCActionType.Sell)
//                {
//                    return true;
//                }
//            }
//        }
//        return false;
//    }
//}

//[Action("NPC/Verifications/ChooseRandomAction")]
//public class RandomAction : BasePrimitiveAction
//{
//    [InParam("Reference")]
//    BTReferences references;

//    public override TaskStatus OnUpdate()
//    {
//        if (references.ready)
//        {
//            NPCAction action = references.actions[Random.Range(0, references.actions.Count)];
//            if (action != null)
//            {
//                for (int i = 0; i < action.NPCInteractionCompatibilities.Length; i++)
//                {
//                    if (action.NPCInteractionCompatibilities[i] == references.pointTarget.GetComponent<NPCLocomotionPoint>().locomotionPointType)
//                    {
//                        if (action.enable)
//                        {
//                            references.SetSelectedAction(action.actionType, action.enable, action.duration, action.NPCInteractionCompatibilities);
//                            Debug.Log(references.agent.name + " - Selected action: " + references.selectedAction.actionType[0]);
//                            return TaskStatus.COMPLETED;
//                        }
//                        else
//                        {
//                            Debug.LogWarning(references.agent.name + " - Action not enabled.");
//                            return TaskStatus.FAILED; // or TaskStatus.RUNNING, depending on your logic
//                        }
//                    }
//                }
//            }
//            else
//            {
//                Debug.LogWarning(references.agent.name + " - No valid action found.");
//            }
//            Debug.LogWarning(references.agent.name + " - No compatible action found for target.");
//            return TaskStatus.FAILED; // or TaskStatus.RUNNING, depending on your logic
//        }
//        else
//        {
//            Debug.LogWarning(references.agent.name + " - Reference not ready.");
//            return TaskStatus.ABORTED;
//        }
//    }
//}

//[Condition("NPC/Verifications/InteractionCompatibility")]
//public class InteractionCompatibility : ConditionBase
//{
//    [InParam("Reference")]
//    BTReferences references;

//    public override bool Check()
//    {
//        if (references.selectedAction != null && references.pointTarget != null)
//        {
//            NPCInteractionCompatibilities pointType = references.pointTarget.GetComponent<NPCLocomotionPoint>().locomotionPointType;
//            foreach (var npc in references.selectedAction.NPCInteractionCompatibilities)
//            {
//                if (npc == pointType)
//                {
//                    return true;
//                }
//            }
//        }
//        else
//        {
//            Debug.LogWarning(references.agent.name + " - Selected action or point target is null.");
//        }
//        Debug.Log(references.agent.name + " - Interaction compatibility: false");
//        return false;
//    }
//}

//[Action("NPC/Actions/RestartRotine")]
//public class RestartRotine : BasePrimitiveAction
//{
//    [InParam("Reference")]
//    BTReferences references;

//    public override TaskStatus OnUpdate()
//    {
//        references.ready = false;
//        references.ready = true;
//        Debug.Log(references.agent.name + " - Restarting routine.");
//        return TaskStatus.COMPLETED;
//    }
//}

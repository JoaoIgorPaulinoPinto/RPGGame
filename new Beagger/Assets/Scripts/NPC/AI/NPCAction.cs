using UnityEngine;
using static NPCBehaviorManager;

public class NPCAction : MonoBehaviour
{
    public virtual void ExecuteAction(GameObject agent, Task task, bool called)
    {
        switch (task.taskType)
        {
            case NPCTaskType.Talk:
                Talk(agent, task, called);
                break;
            case NPCTaskType.Work:
                Work(agent, task);
                break;
            case NPCTaskType.Buy:
                Buy(agent, task);
                break;
            case NPCTaskType.Sell:
                Sell(agent, task);
                break;
          /* case NPCTaskType.Steal:
                Steal(agent, task);
                break;*/
        }
    }

    private void Talk(GameObject agent, Task task, bool called)
    {
        if (!called)
        {
            var targetNPC = task.targetPosition.GetComponent<NPCBehaviorManager>();
            if (targetNPC != null)
            {
                targetNPC.SetImmediateTask(new Task("Conversando", task.duration, agent.transform,   task.taskType));
            }
        }
        else
        {
            //Debug.Log($"{agent.name} está conversando com {task.targetPosition.name}...");
        }

    }

    private void Work(GameObject agent, Task task)
    {
        //Debug.Log($"{agent.name} está trabalhando em {task.targetPosition.name}...");
    }

    private void Buy(GameObject agent, Task task)
    {
        //Debug.Log($"{agent.name} está comprando em {task.targetPosition.name}...");
    }

    private void Sell(GameObject agent, Task task)
    {
        //Debug.Log($"{agent.name} está vendendo em {task.targetPosition.name}...");
    }

    private void Steal(GameObject agent, Task task)
    {
        //Debug.Log($"{agent.name} está roubando de {task.targetPosition.name}...");
    }
}

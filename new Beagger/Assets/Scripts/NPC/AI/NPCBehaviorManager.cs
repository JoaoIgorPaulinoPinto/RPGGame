using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviorManager : MonoBehaviour
{
    [System.Serializable]
    public class Task
    {
        public string actionName;
        public float duration;
        public Transform targetPosition;
        public NPCTaskType taskType;
        public bool finalized;

        public Task(string actionName, float duration, Transform targetPosition, NPCTaskType taskType)
        {
            this.actionName = actionName;
            this.duration = duration;
            this.targetPosition = targetPosition;
            this.taskType = taskType;
            this.finalized = false;
        }
    }

    [System.Serializable]
    public class Routine
    {
        public string routineName;
        public List<Task> tasks = new List<Task>();
        public bool finalized;
    }

    [System.Serializable]
    public class NPC
    {
        public bool released;
        public string NPCName;
        public float moveSpeed;

        public bool randomic;
        public List<Transform> points;

        public NavMeshAgent navMeshAgent;
    }

    public List<Routine> routines;
    public NPC agent;
    public float distanceTrigger;
    public Task taskFocus;
    private Routine routineFocus;
    private Coroutine currentActionCoroutine;

    private Rigidbody2D rb;
    [SerializeField] NPCAction npcAction;

    private void Awake()
    {
        if (agent.randomic)
        {
            GenerateRandomTasks();
        }
   
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        npcAction = GetComponent<NPCAction>(); // Ação atribuída automaticamente.
        agent.navMeshAgent = GetComponent<NavMeshAgent>();
        agent.released = true;
        ChooseTask();

        
    }
    public void GenerateRandomTasks()
    {
        foreach (var routine in routines)
        {
            routine.tasks.Clear(); // Limpa tarefas antigas se existirem
            int taskCount = Random.Range(5, 8); // Número de tarefas na rotina

            for (int i = 0; i < taskCount; i++)
            {
                Task newTask = TryGenerateTask(); // Tenta gerar uma tarefa válida com até 3 tentativas

                if (newTask != null)
                {
                    routine.tasks.Add(newTask); // Adiciona a tarefa válida à rotina
                }
                else
                {
                    // Se nenhuma tarefa válida foi gerada, trate conforme necessário
                    Debug.LogWarning("Não foi possível gerar uma tarefa válida após várias tentativas.");
                }
            }
            ChooseTask();
            routine.finalized = false; // Resetando a flag de finalização da rotina
        }
    }

    private Task TryGenerateTask()
    {

        //print("Tetando");
        NPCTaskType taskType = (NPCTaskType)Random.Range(0, System.Enum.GetValues(typeof(NPCTaskType)).Length);
        Transform randomPoint = null;
        if (agent.points.Count > 0)
        {
            randomPoint = agent.points[Random.Range(0, agent.points.Count)];
           

        }
            NPCInteractionPoint interactionPoint = randomPoint.GetComponent<NPCInteractionPoint>();
        if (interactionPoint != null)
            {
                // Verifica se o ponto é compatível com a tarefa
                if ((taskType == NPCTaskType.Talk && (interactionPoint.PointType == NPCInteractionPointType.NPC || interactionPoint.PointType == NPCInteractionPointType.Player)) ||
                    (taskType == NPCTaskType.Buy && interactionPoint.PointType == NPCInteractionPointType.Comerce) ||
                    (taskType == NPCTaskType.Sell && interactionPoint.PointType == NPCInteractionPointType.Comerce))
                {
                    string actionName = $"{taskType.ToString()}";
                    float duration = Random.Range(1f, 3f); // Duração aleatória entre 5 e 20 segundos
                    return new Task(actionName, duration, interactionPoint.Point, taskType); // Retorna a tarefa válida
                }
                else
                {
                    return TryGenerateTask();
                }
            }
            else
            {
                return TryGenerateTask();
            }

    }


    public void ChooseTask()
    {

        if(routines.Count > 0)
        {
            foreach (var item in routines)
            {
                if(item.tasks.Count > 0)
                {
                    foreach (var routine in routines)
                    {
                        if (!routine.finalized)
                        {
                            routineFocus = routine;
                            foreach (var task in routine.tasks)
                            {
                                if (!task.finalized)
                                {
                                    taskFocus = task;
                                    //print($"Tarefa {task.actionName} escolhida.");
                                    GoToTaskPosition();
                                    return;
                                }
                            }
                            routine.finalized = true; // Marca a rotina como finalizada após todas as tarefas serem concluídas
                        }
                    }
                    // Reiniciar rotinas e gerar novas tarefas
                    foreach (var routine in routines)
                    {
                        routine.finalized = false;
                        for (int i = 0; i < routine.tasks.Count; i++)
                        {
                            routine.tasks[i].finalized = false;
                        }
                    }
                    taskFocus = null;
                   // print("Nenhuma tarefa disponível.");
                    ChooseTask(); // Recomeçar o processo de escolha de tarefas

                }
            }
        }
        
    }

    public void GoToTaskPosition()
    {
        if (taskFocus == null || taskFocus.targetPosition == null)
        {
            //  print("Nenhuma tarefa para realizar.");
            return;
        } 
        else if (agent.released && taskFocus != null)
        {
            if(taskFocus.targetPosition != null)
            {
                StartCoroutine(MoveToPosition(taskFocus.targetPosition.position));
            } 
          
        }
    } 

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        if(taskFocus!= null)
        {
            while (Vector2.Distance(taskFocus.targetPosition.position, transform.position) > distanceTrigger)
            {
                agent.navMeshAgent.SetDestination(taskFocus.targetPosition.position);
                yield return null;
            }

            rb.velocity = Vector2.zero; // Parar o movimento ao alcançar o destino.
            Action(false);
        }
       
    }

    public void Action(bool called)
    {
        if (taskFocus.targetPosition == null)
        {
            GenerateRandomTasks();
            return;
        }
        if (currentActionCoroutine != null)
        {
            StopCoroutine(currentActionCoroutine);
        }

        if (taskFocus != null)
        {
            npcAction?.ExecuteAction(gameObject, taskFocus, called);
            currentActionCoroutine = StartCoroutine(ActionCoroutine());
        }
    }

    private IEnumerator ActionCoroutine()
    {
        float timer = 0f;
        while (timer < taskFocus.duration)
        {
            if (!agent.released)
            {
                //print($"{gameObject.name} foi interrompido.");
                yield break;
            }
            //print("em ação");

            timer += Time.deltaTime;
            yield return null;
        }

       // print($"{taskFocus.actionName} finalizado.");
        taskFocus.finalized = true;
        ChooseTask();
    }

    public void SetImmediateTask(Task newTask)
    {
        //print($"{gameObject.name}, chamado para conversar");

        if (currentActionCoroutine != null)
        {
            StopCoroutine(currentActionCoroutine);
        }

        taskFocus = newTask;
        routineFocus = null;
        Action(true);
    }


    private void Update()
    {
        Vector3 rot = new Vector3(0, 0, 0);
        transform.rotation =  Quaternion.Euler(rot);
    }
}
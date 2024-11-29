using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehavior : MonoBehaviour
{
    public Transform pointToSleep;
    public float sleepTime;
    public float wakeTime;

    public NavMeshAgent agent;
    private LocomotionPointManager locomotionPointManager;
    private ActionManager actionManager;
    private Collider npcCollider;
    private SpriteRenderer npcRenderer;

    private Transform currentTarget;
    private bool isSleeping = false;

    public float minActionTime = 2f; // Tempo mínimo da ação
    public float maxActionTime = 5f; // Tempo máximo da ação

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        locomotionPointManager = GetComponent<LocomotionPointManager>();
        actionManager = GetComponent<ActionManager>();
        npcCollider = GetComponent<Collider>();
        npcRenderer = GetComponent<SpriteRenderer>();

        locomotionPointManager.FindLocomotionPoints();
        StartCoroutine(MoveToRandomPoints());
    }

    private IEnumerator MoveToRandomPoints()
    {
        while (true)
        {
            float currentHour = GetCurrentHour();

            // Verifica se é hora de dormir
            if (currentHour > sleepTime || currentHour < wakeTime)
            {
                // Caso esteja dormindo, continua no ponto de dormir
                if (!isSleeping)
                {
                    // Move para o ponto de dormir
                    agent.SetDestination(pointToSleep.position);

                    // Aguarda o NPC chegar ao ponto de dormir
                    while (!agent.pathPending && agent.remainingDistance > agent.stoppingDistance)
                    {
                        yield return null;
                    }

                    // Entra no estado de sono
                    EnterSleepState();
                }

                // Aguarda até o horário de acordar
                while (currentHour > sleepTime || currentHour < wakeTime)
                {
                    currentHour = GetCurrentHour();
                    yield return null; // Verifica a cada frame
                }

                // Sai do estado de sono
                ExitSleepState();
            }

            // Caso não seja hora de dormir, realiza a locomoção normal
            if (locomotionPointManager.locomotionPoints.Count == 0)
            {
                yield break;
            }

            currentTarget = ChooseRandomPoint();

            if (currentTarget != null)
            {
                agent.SetDestination(currentTarget.position);

                // Aguarda o NPC chegar ao ponto
                while (!agent.pathPending && agent.remainingDistance > agent.stoppingDistance)
                {
                    yield return null;
                }
                DisableNPCComponents();
                // Simula "entrada" no ponto de ação
                PerformAction();

                // Define o tempo de espera antes de continuar
                float actionTime = Random.Range(minActionTime, maxActionTime);
                yield return new WaitForSeconds(actionTime);
                EnableNPCComponents();
            }
        }
    }

    private void EnterSleepState()
    {
        isSleeping = true;
        DisableNPCComponents();
    }

    private void ExitSleepState()
    {
        isSleeping = false;
        EnableNPCComponents();
    }

    private void DisableNPCComponents()
    {
        if (npcCollider != null) npcCollider.enabled = false;
        if (npcRenderer != null) npcRenderer.enabled = false;
        if (agent != null) agent.isStopped = true;
    }

    private void EnableNPCComponents()
    {
        if (npcCollider != null) npcCollider.enabled = true;
        if (npcRenderer != null) npcRenderer.enabled = true;
        if (agent != null) agent.isStopped = false;
    }

    private void PerformAction()
    {
        if (actionManager != null)
        {
            var chosenAction = actionManager.ChooseRandomAction();
            actionManager.ExecuteAction(chosenAction);
        }

    }

    private Transform ChooseRandomPoint()
    {
        if (locomotionPointManager.locomotionPoints.Count == 0) return null;

        int randomIndex = Random.Range(0, locomotionPointManager.locomotionPoints.Count);
        return locomotionPointManager.locomotionPoints[randomIndex];
    }

    private float GetCurrentHour()
    {
        return (TimeController.Instance.dayCount * 24f +
               (TimeController.Instance.dayTimer / (float)TimeController.Instance.dayDuration) * 24f) % 24f;
    }

    public void GoToLocation(Vector3 target)
    {
        agent.SetDestination(target);
    }

    public bool HasReachedDestination()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0); // Impede rotação visual indesejada
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehavior : MonoBehaviour
{
    public NavMeshAgent agent;
    private LocomotionPointManager locomotionPointManager;

    public float minActionTime = 2f; // Tempo m�nimo da a��o
    public float maxActionTime = 5f; // Tempo m�ximo da a��o
    private Transform currentTarget;

    private ActionManager actionManager; // Refer�ncia ao ActionManager
    private Collider npcCollider;
    private SpriteRenderer npcRenderer; // Presumindo que seja 2D, para 3D, pode ser Renderer

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        locomotionPointManager = GetComponent<LocomotionPointManager>();
        actionManager = GetComponent<ActionManager>(); // Obt�m o ActionManager
        npcCollider = GetComponent<Collider>();
        npcRenderer = GetComponent<SpriteRenderer>(); // Ou Renderer se for 3D

        locomotionPointManager.FindLocomotionPoints();
        StartCoroutine(MoveToRandomPoints());
    }

    private IEnumerator MoveToRandomPoints()
    {
        while (true)
        {
            if (locomotionPointManager.locomotionPoints.Count == 0)
            {
                Debug.LogWarning("Nenhum ponto de locomo��o dispon�vel.");
                yield break;
            }

            currentTarget = ChooseRandomPoint();

            if (currentTarget != null)
            {
                agent.SetDestination(currentTarget.position);

                // Aguarda o NPC chegar ao ponto
                while (Vector3.Distance(transform.position, currentTarget.position) > agent.stoppingDistance)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(1f);
                // Simula a "entrada" no estabelecimento desativando o colisor e a renderiza��o
                DisableNPCComponents();
                PerformAction();

                // Define um tempo de a��o aleat�rio (simula o tempo dentro do estabelecimento)
                float actionTime = Random.Range(minActionTime, maxActionTime);

                // Aguarda o tempo da a��o
                yield return new WaitForSeconds(actionTime);

                // Reativa o NPC ap�s a a��o (como se tivesse sa�do do estabelecimento)
                EnableNPCComponents();

                // Ap�s ser reativado, escolhe outro ponto para ir
                currentTarget = ChooseRandomPoint();
            }
        }
    }

    // M�todo para desativar os componentes visuais e de colis�o do NPC
    private void DisableNPCComponents()
    {
        if (npcCollider != null) npcCollider.enabled = false;
        if (npcRenderer != null) npcRenderer.enabled = false;
        agent.enabled = false; // Desativa o NavMeshAgent para que ele pare de se mover
    }

    // M�todo para reativar os componentes visuais e de colis�o do NPC
    private void EnableNPCComponents()
    {
        if (npcCollider != null) npcCollider.enabled = true;
        if (npcRenderer != null) npcRenderer.enabled = true;
        agent.enabled = true; // Reativa o NavMeshAgent para que ele volte a se mover
    }

    // Escolhe uma a��o aleat�ria para o NPC ao chegar no ponto
    private void PerformAction()
    {
        if (actionManager != null)
        {
            // Escolhe uma a��o aleat�ria entre comprar e vender
            ActionManager.NPCActionType chosenAction = actionManager.ChooseRandomAction();

            // Executa a a��o
            actionManager.ExecuteAction(chosenAction);
        }
        else
        {
            Debug.LogWarning("Nenhum ActionManager foi encontrado.");
        }
    }

    private Transform ChooseRandomPoint()
    {
        if (locomotionPointManager.locomotionPoints.Count == 0) return null;

        int randomIndex = Random.Range(0, locomotionPointManager.locomotionPoints.Count);
        return locomotionPointManager.locomotionPoints[randomIndex];
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

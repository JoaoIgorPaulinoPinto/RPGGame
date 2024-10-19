using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimationController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    // Parâmetros do Blend Tree no Animator
    private float moveX;
    private float moveY;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        // Verifica se o agente está em movimento
        if (agent.velocity.magnitude > 0.1f)
        {
            // Normaliza a velocidade do agente para obter a direção
            Vector3 velocity = agent.velocity.normalized;

            // Definir os valores X e Y com base na direção do movimento
            moveX = velocity.x;
            moveY = velocity.z;
            // Definir os parâmetros X e Y no Animator
            animator.SetFloat("X", moveX * 10);
            animator.SetFloat("Y", moveY  * 10);
            print("X : " + velocity.x );
            print("Y : " + velocity.y );
        }
        else
        {
            // Se o agente estiver parado, definir os valores de X e Y como zero
            animator.SetFloat("X", 0f);
            animator.SetFloat("Y", 0f);
        }
       
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Estrutura para representar um ponto de patrulha com o tempo de espera
    [System.Serializable]
    public class Point
    {
        public Transform patrolPoint;
        public float patrolTime;

        public Point(Transform patrolPoint, float patrolTime)
        {
            this.patrolPoint = patrolPoint;
            this.patrolTime = patrolTime;
        }
    }

    public Transform player; // Referência ao jogador
    public List<Point> locomotionPoints; // Lista de pontos de patrulha
    public float chaseDistance = 10f; // Distância onde o inimigo começa a perseguir o jogador
    public float patrolSpeed = 2f; // Velocidade de patrulha
    public float chaseSpeed = 4f; // Velocidade de perseguição
    public float stopDistance = 1f; // Distância para parar de se mover

    private int currentPatrolIndex = 0; // Índice do ponto de patrulha atual
    private float patrolTimer = 0f; // Temporizador de patrulha
    private bool isChasing = false; // Se o inimigo está perseguindo o jogador
    private NavMeshAgent agent; // Componente NavMeshAgent para controlar o movimento

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Inicializa o NavMeshAgent
        agent.speed = patrolSpeed; // Define a velocidade de patrulha inicialmente
        StartPatrol(); // Começa a patrulha
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (this.isChasing)
        {
            ChasePlayer(); // Executa perseguição
        }
        else
        {
            Patrol(); // Executa patrulha
        }
        // Verifica a distância do inimigo até o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
      

        // Verifica se o inimigo deve começar a perseguir ou voltar à patrulha
        if (distanceToPlayer <= chaseDistance && !this.isChasing)
        {
            this.isChasing = true;
            StartChasing(); // Inicia a perseguição
        }
        else if (distanceToPlayer > chaseDistance && this.isChasing)
        {
            this.isChasing = false;
            StartPatrol(); // Retorna à patrulha
        }

        // Se está perseguindo, chama o método de perseguição

    }

    // Função chamada ao iniciar a patrulha
    private void StartPatrol()
    {
        if (!isChasing && locomotionPoints.Count > 0)
        {
            ChooseNewPatrolPoint(); // Escolhe um novo ponto de patrulha
        }
    }

    // Função chamada ao iniciar a perseguição
    private void StartChasing()
    {
        // Define a velocidade de perseguição
        agent.speed = chaseSpeed; // Ajusta a velocidade para perseguir o jogador
    }

    // Função de patrulha
    private void Patrol()
    {
        if (!isChasing && locomotionPoints.Count > 0)
        {
            patrolTimer += Time.deltaTime;

            // Se o tempo de patrulha for maior ou igual ao tempo definido para o ponto de patrulha atual
            if (patrolTimer >= locomotionPoints[currentPatrolIndex].patrolTime)
            {
                // Mover para o próximo ponto de patrulha
                ChooseNewPatrolPoint();
                patrolTimer = 0f; // Resetar o temporizador
            }
        }
    }

    // Função para perseguir o jogador
    private void ChasePlayer()
    {
        // Continuar perseguindo o jogador
        agent.SetDestination(player.position); // Define o destino como a posição do jogador
    }

    // Função para escolher um novo ponto de patrulha
    private void ChooseNewPatrolPoint()
    {
        if (locomotionPoints.Count > 0)
        {
            currentPatrolIndex = Random.Range(0, locomotionPoints.Count);
            agent.SetDestination(locomotionPoints[currentPatrolIndex].patrolPoint.position); // Define o destino de patrulha
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o inimigo atingiu o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Implementar a lógica de ataque, conforme necessário
        }
    }
}

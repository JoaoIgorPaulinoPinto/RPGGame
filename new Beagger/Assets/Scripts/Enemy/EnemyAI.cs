using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public EnemyData enemyData;

    [SerializeField] NavMeshAgent agent;
    public Transform player;

    [System.Serializable]
    public class Point
    {
        public Transform point; // Local de patrulha
        public float timeWait;  // Tempo de espera no ponto
    }

    public List<Point> locomotionPoints;

    private int currentPointIndex = 0;  // Índice do ponto de patrulha atual
    private Point currentPoint;
    private float waitTimer = 0f;
    private bool waiting = false;

    bool following = false;
    bool idle = true;

    [SerializeField] float patrolSpeed = 2f; // Velocidade de patrulha
    [SerializeField] float chaseSpeed = 4f;  // Velocidade de perseguição
    [SerializeField] float radius;
    [SerializeField] float maxPlayerDistance;
    [SerializeField] LayerMask layer;

    [SerializeField] float stopFollowWaitTime = 2f; // Tempo de espera ao parar de seguir
    private float stopFollowTimer = 0f; // Temporizador ao parar de seguir
    private bool waitAfterFollowing = false; // Se deve esperar após parar de seguir

    private float attackCooldown = 1f; // Tempo de cadência de ataque
    private float lastAttackTime = 0f; // Tempo do último ataque

    private void Start()
    {
        agent.speed = patrolSpeed; // Configura a velocidade inicial para patrulha
        SetNextPatrolPoint(); // Define o primeiro ponto de patrulha
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Manager();
    }

    void Manager()
    {
        if (PlayerNearby())
        {
            FollowPlayer();
        }
        else
        {
            if (following) // Se o inimigo estava seguindo o jogador e ele sumiu
            {
                StopFollowing();
            }

            // Espera um tempo antes de voltar a patrulhar
            if (waitAfterFollowing)
            {
                stopFollowTimer += Time.deltaTime;
                if (stopFollowTimer >= stopFollowWaitTime)
                {
                    waitAfterFollowing = false; // Finaliza a espera
                    stopFollowTimer = 0f; // Reseta o temporizador
                    Patrol(); // Volta a patrulhar
                }
            }
            else
            {
                Patrol(); // Volta a patrulhar diretamente se não estiver esperando
            }
        }
    }

    void Patrol()
    {
        if (idle && !waiting)
        {
            SetNextPatrolPoint(); // Define o próximo ponto de patrulha
            idle = false;
            agent.SetDestination(currentPoint.point.position);
        }
        else if (currentPoint != null && Vector3.Distance(agent.transform.position, currentPoint.point.position) <= 1f)
        {
            if (!waiting)
            {
                waiting = true;
                waitTimer = 0f; // Resetando o temporizador
            }

            waitTimer += Time.deltaTime;

            if (waitTimer >= currentPoint.timeWait)
            {
                waiting = false; // Reseta o estado de espera
                idle = true; // Permite mover para o próximo ponto
            }
        }
    }

    void FollowPlayer()
    {
        following = true;
        agent.speed = chaseSpeed; // Configura a velocidade para perseguição
        agent.SetDestination(player.position);
        idle = false; // Para de patrulhar enquanto segue o jogador
    }

    void StopFollowing()
    {
        following = false;
        idle = true; // Não patrulhar imediatamente
        waitAfterFollowing = true; // Inicia a espera
        agent.ResetPath(); // Para o movimento atual
        stopFollowTimer = 0f; // Reseta o temporizador de espera
    }

    bool PlayerNearby()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, layer);
        foreach (var item in hitColliders)
        {
            if (item.CompareTag("Player"))
            {
                return true; // Jogador dentro do raio
            }
        }
        return false; // Jogador fora do raio
    }

    private void SetNextPatrolPoint()
    {
        // Define o próximo ponto de patrulha e reinicia o índice ao chegar ao fim
        currentPoint = locomotionPoints[currentPointIndex];
        currentPointIndex = (currentPointIndex + 1) % locomotionPoints.Count; // Cicla os pontos
    }

    private void OnDrawGizmos()
    {
        // Define a cor do gizmo para visualizar o raio
        Gizmos.color = Color.red;

        // Desenha um círculo ao redor do inimigo para representar o raio de detecção
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Verifica se o tempo de cadência passou antes de permitir o ataque
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                // Atualiza o tempo do último ataque
                lastAttackTime = Time.time;

                // Executa o ataque
                collision.gameObject.TryGetComponent(out IHitable hitable);
                hitable.Hited(enemyData.atackDamage, transform, enemyData.atackStanTime, enemyData.weapon);
            }
        }
    }
}

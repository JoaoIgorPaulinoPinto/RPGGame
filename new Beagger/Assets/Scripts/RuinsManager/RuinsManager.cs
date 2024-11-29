using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinsManager : MonoBehaviour
{
    [SerializeField] int quant; // Quantidade de inimigos a serem gerados
    [SerializeField] GameObject prefab; // Prefab do inimigo
    [SerializeField] Transform parent; // Transform de pai para os inimigos
    [SerializeField] List<GameObject> enemies; // Lista de inimigos ativos
    [SerializeField] Transform[] spawnPoints; // Pontos de spawn para os inimigos
    [SerializeField] List<Transform> locomotionPoints; // Pontos de patrulha

    [Space]
    [SerializeField] Trunk trunk; // Referência ao baú
    [SerializeField] List<ItemData> items; // Itens que podem aparecer no baú

    private void OnEnable()
    {
        StartCoroutine(WaitForTimeController());
    }

    private IEnumerator WaitForTimeController()
    {
        while (TimeController.Instance == null)
        {
            yield return null;
        }

        // Assina os eventos para responder a mudanças de tempo
        TimeController.Instance.OnDayPassed += AlternateTrunkItems;
        TimeController.Instance.OnDayPassed += RespawnEnemies;
    }

    private void OnDisable()
    {
        if (TimeController.Instance != null)
        {
            TimeController.Instance.OnDayPassed -= AlternateTrunkItems;
            TimeController.Instance.OnDayPassed -= RespawnEnemies;
        }
    }

    public void AlternateTrunkItems()
    {
        trunk.items.Clear();
        int itemCount = Random.Range(3, items.Count); // Garante que pelo menos 3 itens sejam escolhidos

        for (int j = 0; j < itemCount; j++)
        {
            int i = Random.Range(0, items.Count);
            if (i < items.Count) // Garantir que o item não se repita
            {
                trunk.items.Add(new TrunkItems(items[i], Random.Range(1, 4))); // Adiciona itens ao baú
            }
        }
    }

    public void RespawnEnemies()
    {
        int aliveEnemies = 0;

        // Conta inimigos ativos
        foreach (var enemy in enemies)
        {
            if (enemy.activeInHierarchy)
            {
                aliveEnemies++;
            }
        }

        // Verifica se é necessário gerar mais inimigos
        int enemiesToSpawn = quant - aliveEnemies;

        if (enemiesToSpawn <= 0)
        {
            return;
        }

        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);
        List<Transform> availableLocomotionPoints = new List<Transform>(locomotionPoints);

        // Gera novos inimigos
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            if (availableSpawnPoints.Count == 0)
                break;

            Transform spawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
            availableSpawnPoints.Remove(spawnPoint); // Remove ponto de spawn para não repetir

            // Instancia um novo inimigo
            GameObject newEnemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            enemies.Add(newEnemy);
            newEnemy.transform.SetParent(parent);

            // Configura o AI do inimigo
            EnemyAI enemyAI = newEnemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.player = GameObject.FindGameObjectWithTag("Player").transform;

                // Cria uma lista de pontos de locomoção exclusivos para esse inimigo
                List<EnemyAI.Point> patrolPoints = new List<EnemyAI.Point>();
                List<Transform> shuffledLocomotionPoints = new List<Transform>(availableLocomotionPoints);
                shuffledLocomotionPoints.Remove(spawnPoint); // Remove o ponto de spawn da lista de locomoção

                // Embaralha os pontos de patrulha para garantir variação
                ShuffleList(shuffledLocomotionPoints);

                // Atribui pontos de locomoção ao inimigo
                foreach (Transform locPoint in shuffledLocomotionPoints)
                {
                    patrolPoints.Add(new EnemyAI.Point(locPoint, Random.Range(2f, 5f)));
                }

                enemyAI.locomotionPoints = patrolPoints;
            }
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    private void Update()
    {
        // Tecla para testar a alternância de itens e respawn de inimigos
        if (Input.GetKeyDown(KeyCode.O))
        {
            AlternateTrunkItems();
            RespawnEnemies();
        }
    }
}

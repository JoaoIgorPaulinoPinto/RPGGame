using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinsManager : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform parent;
    [SerializeField] GameObject[] enemies;
    [SerializeField] Transform[] spawnPoints; // Pontos de nascimento
    [SerializeField] List<Transform> locomotionPoints; // Pontos de patrulha

    [Space]
    [SerializeField] Trunk trunk;
    [SerializeField] List<ItemData> items;

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
        int o;
        do
        {
            o = Random.Range(0, items.Count);
        }
        while (o <= 2);

        for (int j = 0; j < o; j++)
        {
            int i = Random.Range(0, 2);
            if (i == 1)
            {
                trunk.items.Add(new TrunkItems(items[j], Random.Range(1, 4)));
            }
        }
    }

    public void RespawnEnemies()
    {
        int aliveEnemies = 0;

        foreach (var enemy in enemies)
        {
            if (enemy.activeInHierarchy)
            {
                aliveEnemies++;
            }
        }

        int enemToSpawn = Mathf.Max(3 - aliveEnemies, 0);

        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);
        List<Transform> availableLocomotionPoints = new List<Transform>(locomotionPoints);

        for (int i = 0; i < enemToSpawn; i++)
        {
            // Seleciona um ponto de nascimento único
            Transform spawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
            availableSpawnPoints.Remove(spawnPoint);

            // Cria o inimigo
            GameObject newEnemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            newEnemy.transform.SetParent(parent);

            // Configura o player e pontos de locomoção do inimigo
            EnemyAI enemyAI = newEnemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.player = GameObject.FindGameObjectWithTag("Player").transform;

                // Configura pontos de locomoção únicos, excluindo o ponto de nascimento
                List<EnemyAI.Point> enemyPatrolPoints = new List<EnemyAI.Point>();
                List<Transform> shuffledLocPoints = new List<Transform>(availableLocomotionPoints);
                shuffledLocPoints.Remove(spawnPoint); // Remove o ponto de nascimento dos pontos de patrulha

                // Embaralha e seleciona pontos para patrulha
                ShuffleList(shuffledLocPoints);
                foreach (Transform locPoint in shuffledLocPoints)
                {
                    EnemyAI.Point point = new EnemyAI.Point
                    {
                        point = locPoint,
                        timeWait = Random.Range(2f, 5f)
                    };
                    enemyPatrolPoints.Add(point);
                }

                enemyAI.locomotionPoints = enemyPatrolPoints;
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
        if (Input.GetKeyDown(KeyCode.O))
        {
            AlternateTrunkItems();
            RespawnEnemies();
        }
    }
}

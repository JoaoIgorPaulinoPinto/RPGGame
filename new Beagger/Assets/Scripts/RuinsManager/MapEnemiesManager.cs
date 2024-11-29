using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyAI;

public class MapEnemiesManager : MonoBehaviour
{
    [SerializeField] int quant;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform parent;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] Transform[] spawnPoints; // Pontos de nascimento
    [SerializeField] int maxEnemies = 3; // Limite máximo de inimigos

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
        TimeController.Instance.OnDayPassed += RespawnEnemies;
    }

    private void OnDisable()
    {
        if (TimeController.Instance != null)
        {
            TimeController.Instance.OnDayPassed -= RespawnEnemies;
        }
    }

    public void RespawnEnemies()
    {
        int alive = 0;

        // Contando os inimigos ativos na cena
        foreach (var enemy in enemies)
        {
            if (enemy.activeInHierarchy)
            {
                alive++;
            }
        }

        // Verifica se há menos de maxEnemies e calcula a quantidade de inimigos a serem criados
        int enemiesToSpawn = quant - alive;

        // Se não há necessidade de respawn, saímos da função
        if (enemiesToSpawn <= 0)
        {
            return;
        }
        else
        {
            // Lista de pontos de spawn disponíveis
            List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

            for (int i = 0; i < quant; i++)
            {
                // Seleciona um ponto de nascimento único
                Transform spawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
                availableSpawnPoints.Remove(spawnPoint); // Remove o ponto da lista para evitar reutilização

                // Cria o novo inimigo
                GameObject newEnemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
                enemies.Add(newEnemy);
                newEnemy.transform.SetParent(parent);

                // Configura os pontos de patrulha do novo inimigo
                EnemyAI enemyAI = newEnemy.GetComponent<EnemyAI>();
                for (int j = 0; j < 4; j++)
                {
                    Transform clp = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
                    
                }
            }
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            RespawnEnemies(); // Testar respawn manualmente ao pressionar 'O'
        }
    }
}

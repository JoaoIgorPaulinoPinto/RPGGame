using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGenerator : MonoBehaviour
{
    [SerializeField] List<ItemData> possibleItems; // Lista de itens poss�veis para a quest
    [SerializeField] int minReward = 50; // Valor m�nimo da recompensa
    [SerializeField] int maxReward = 200; // Valor m�ximo da recompensa
    [SerializeField] int minQuestQuant = 1; // N�mero m�nimo de quests a serem geradas por vez
    [SerializeField] int maxQuestQuant = 4; // N�mero m�ximo de quests a serem geradas por vez
    [SerializeField] int minTime = 60; // Tempo m�nimo (em segundos) para completar a quest
    [SerializeField] int maxTime = 300; // Tempo m�ximo (em segundos) para completar a quest
    [SerializeField] int minItems = 1; // N�mero m�nimo de itens necess�rios para a quest
    [SerializeField] int maxItems = 5; // N�mero m�ximo de itens necess�rios para a quest

    public List<Quest> generatedQuests = new List<Quest>(); // Lista de quests geradas

    private void OnEnable()
    {
        StartCoroutine(WaitForTimeController());
    }

    private IEnumerator WaitForTimeController()
    {
        // Aguarda at� que o TimeController esteja inicializado
        while (TimeController.Instance == null)
        {
            yield return null; // Espera um frame antes de verificar novamente
        }

        // Inscrever-se no evento OnDayPassed do TimeController
        TimeController.Instance.OnDayPassed += GenerateMultipleQuests;
    }

    private void OnDisable()
    {
        // Remover inscri��o do evento OnDayPassed ao desativar o script
        if (TimeController.Instance != null)
        {
            TimeController.Instance.OnDayPassed -= GenerateMultipleQuests;
        }
    }

    // Fun��o para gerar uma nova quest automaticamente
    public Quest GenerateRandomQuest()
    {
        Quest newQuest = new Quest();

        // Sorteia um nome e descri��o aleat�rios para a quest
        newQuest.questName = "Coleta de " + possibleItems[Random.Range(0, possibleItems.Count)].itemName;
        newQuest.description = "Colete os itens necess�rios para completar esta miss�o.";

        // Sorteia a recompensa dentro do intervalo definido
        newQuest.reward = Random.Range(minReward, maxReward);

        // Sorteia o tempo limite para completar a quest
        newQuest.time = Random.Range(minTime, maxTime);

        // Define a quantidade de itens necess�rios para a quest
        int numberOfItems = Random.Range(minItems, maxItems);
        newQuest.necessaryItems = new List<QuestItem>(); // Ajustando para usar QuestItem

        // Adiciona os itens sorteados na lista de itens necess�rios para a quest
        for (int i = 0; i < numberOfItems; i++)
        {
            ItemData randomItem = possibleItems[Random.Range(0, possibleItems.Count)];
            int quantity = Random.Range(1, 5); // Define uma quantidade aleat�ria do item
            newQuest.necessaryItems.Add(new QuestItem(randomItem)); // Usa QuestItem em vez de ItemData
        }

        newQuest.accepted = false;
        newQuest.completed = false;

        generatedQuests.Add(newQuest); // Adiciona a quest � lista de quests geradas

        return newQuest; // Retorna a nova quest gerada
    }

    // Fun��o para gerar m�ltiplas quests
    public void GenerateMultipleQuests()
    {
        for (int i = 0; i < Random.Range(minQuestQuant, maxQuestQuant); i++)
        {
            GenerateRandomQuest();
        }
        AddQuests();
    }

    public void AddQuests()
    {
        if (TryGetComponent<QuestsData>(out QuestsData qstDt))
        {
            qstDt.quest.Clear();
            foreach (var item in generatedQuests)
            {
                qstDt.quest.Add(item);
            }
            // Limpa as quests geradas depois de adicion�-las � lista de quests
            generatedQuests.Clear();
        }
    }
}

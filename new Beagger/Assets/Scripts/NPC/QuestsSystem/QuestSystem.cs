using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Quest
{
    public QuestType questType;
    public string questName;
    public string description;
    public List<ItemData> necessaryItens;
    public List<ItemData> deliveredItens;
    public int reward;
    public int time;

    public bool acepted;
    public bool completed;
}

public class QuestSystem : MonoBehaviour
{
    // Singleton instance
    private static QuestSystem _instance;

    public static QuestSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<QuestSystem>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<QuestSystem>();
                    singletonObject.name = typeof(QuestSystem).ToString() + " (Singleton)";
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Optionally, keep the singleton persistent between scenes
        }
    }

    public QuestSystemUIManager UI;
    public Quest quest;

    public void StartUI(QuestsData data, NPCData npcData)
    {
        quest = null;


        UI.quests = data.quest;
        UI.npcInfo = npcData;
        UI.UpdateUI();

    }
    public bool QuestAccepted(Quest AcceptedQuest)
    {
        // Verifica se já há uma quest aceita
        if (this.quest != null && this.quest.acepted)
        {
            PopUpSystem.Instance.SendMsg("Você já está em uma quest...", MessageType.Alert, 3f);
            UI.UpdateUI();
            return false;
        }

        // Se não houver uma quest ativa, aceita a nova
        quest = AcceptedQuest;
        quest.acepted = true;
        UI.QuestAccepted(AcceptedQuest);
        StartQuest();
        PopUpSystem.Instance.SendMsg("Quest Aceita!", MessageType.Message, 3f);
        UI.UpdateUI();
        return true;
    }


    void StartQuest()
    {
        float time = 0;
        time += Time.deltaTime;

        if (time > UI.selectedQuest.time)
        {
            PopUpSystem.Instance.SendMsg("O tempo para a quest acabou", MessageType.Alert, 3f);
            UI.selectedQuest.acepted = false;
            UI.selectedQuest.deliveredItens.Clear();
            UI.selectedQuest.completed = false;
            UI.selectedQuest = null;
        }
    }
    public bool Deliver()
    {
        // Verifica se a quest é nula
        if (UI.selectedQuest == null)
        {
            PopUpSystem.Instance.SendMsg("Não há quest ativa para entregar itens.", MessageType.Alert, 3f);
            return false;
        }

        // Verifica se o inventário está inicializado
        if (Inventory.Instance == null || Inventory.Instance.inventory == null)
        {
            PopUpSystem.Instance.SendMsg("Inventário não está disponível.", MessageType.Alert, 3f);
            return false;
        }

        // Checa se os itens necessários estão inicializados
        if (UI.selectedQuest.necessaryItens == null)
        {
            PopUpSystem.Instance.SendMsg("Itens necessários não estão definidos.", MessageType.Alert, 3f);
            return false;
        }

        // Inicializa a lista de entregas se estiver nula
        if (UI.selectedQuest.deliveredItens == null)
        {
            UI.selectedQuest.deliveredItens = new List<ItemData>();
        }

        // Restante do código
        foreach (var inventoryItem in Inventory.Instance.inventory)
        {
            // Verifica se o item do inventário não é nulo
            if (inventoryItem == null || inventoryItem.item == null)
                continue; // Ignora itens nulos

            foreach (var questItem in UI.selectedQuest.necessaryItens)
            {
                // Verifica se o item da quest não é nulo
                if (questItem == null)
                    continue; // Ignora itens nulos

                // Compara os nomes dos itens
                if (questItem.itemName == inventoryItem.item.itemName)
                {
                    // Adiciona o item à lista de entregas se não estiver já na lista
                    if (!UI.selectedQuest.deliveredItens.Contains(questItem))
                    {
                        UI.selectedQuest.deliveredItens.Add(questItem);
                    }
                }
            }
        }

        // Verifica se todos os itens necessários foram entregues
        if (!Check())
        {
            PopUpSystem.Instance.SendMsg("Você não tem todos itens necessários ainda", MessageType.Alert, 3f);
            return false;
        }
        else
        {
            foreach (var item in UI.selectedQuest.deliveredItens)
            {
                Inventory.Instance.RemoveItem(item, null);
            }
            // Finaliza a quest e dá recompensa
            PlayerStts.Instance.money += UI.selectedQuest.reward;
            UI.selectedQuest.acepted = false;
            UI.selectedQuest.deliveredItens.Clear();
            quest.completed = true;
            UI.selectedQuest = null;
            UI.UpdateUI();
            PopUpSystem.Instance.SendMsg("Quest finalizada com sucesso", MessageType.Message, 3f);

      
            return true;
        }
    }
    bool Check()
    {
        // Verifica se há uma quest selecionada e se ela foi aceita
        if (UI.selectedQuest == null || !UI.selectedQuest.acepted)
        {
            return false; // Retorna falso se não há quest selecionada ou não foi aceita
        }

        // Verifica se os itens necessários estão inicializados
        if (UI.selectedQuest.necessaryItens == null || UI.selectedQuest.deliveredItens == null)
        {
            return false; // Retorna falso se as listas de itens estão nulas
        }

        // Verifica se todos os itens necessários foram entregues
        bool allItemsDelivered = UI.selectedQuest.necessaryItens.All(item =>
            item != null && UI.selectedQuest.deliveredItens.Contains(item));

        return allItemsDelivered; // Retorna o resultado da verificação
    }


}

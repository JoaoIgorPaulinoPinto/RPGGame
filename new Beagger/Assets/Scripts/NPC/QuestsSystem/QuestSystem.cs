using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] QuestSystemUIManager UI;
    Quest quest;

    public void StartUI(QuestsData data, NPCData npcData)
    {
        UI.quests = data.quest; 
        UI.npcInfo = npcData;
        UI.UpdateUI();
    }

    public bool QuestAccepted(Quest quest)
    {
        if (this.quest != null)
        {

            PopUpSystem.Instance.SendMsg("Você já está em uma quest...", MessageType.Alert, 3f);
            UI.UpdateUI();
            return false;
        }
        else
        {
            UI.QuestAccepted(quest);
            this.quest = quest;
            this.quest.acepted = true;
            StartQuest();
            PopUpSystem.Instance.SendMsg("Quest Aceita!", MessageType.Message, 3f);
            UI.UpdateUI();
            return true;
        }
    }

    void StartQuest()
    {
        float time = 0;
        time += Time.deltaTime;

        if (time > quest.time)
        {
            PopUpSystem.Instance.SendMsg("O tempo para a quest acabou", MessageType.Alert, 3f);
            quest.acepted = false;
            quest.deliveredItens.Clear();
            quest.completed = false;
            quest = null;
        }
    }

    public bool Deliver()
    {
        foreach (var Inventoryitem in Inventory.Instance.inventory)
        {
            foreach (var questItem in quest.necessaryItens)
            {
                if (questItem.itemName == Inventoryitem.item.itemName)
                {
                    quest.deliveredItens.Add(questItem);
                }
            }
        }
        if (!check())
        {
            PopUpSystem.Instance.SendMsg("Você não tem todos itens necessarios ainda", MessageType.Alert, 3f);
            return false;
        }
        else
        {
            PlayerStts.Instance.money += quest.reward;
            quest.acepted = false;
            quest.deliveredItens.Clear();
            quest.completed = true;
            quest = null;
            UI.UpdateUI();
            PopUpSystem.Instance.SendMsg("Quest finalizada com sucesso", MessageType.Message, 3f);
            return true;
        }
    }

    bool check()
    { 
        bool r = true;
            
        foreach (var item in quest.necessaryItens)
        {
            if (!quest.deliveredItens.Contains(item))
            {
                r = false;
            }
        }

        return r;
        
    }
}

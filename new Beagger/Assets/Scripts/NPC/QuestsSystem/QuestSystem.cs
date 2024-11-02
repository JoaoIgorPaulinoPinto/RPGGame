using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestItem
{
    public ItemData item;

    public QuestItem(ItemData item)
    {
        this.item = item;
    }
}

[System.Serializable]
public class Quest
{
    public QuestType questType;
    public string questName;
    public string description;
    public List<QuestItem> necessaryItems;
    public int reward;
    public int time;

    public bool accepted;
    public bool completed;
}

public class QuestSystem : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip accept;
    [SerializeField] private AudioClip cantAccept;
    [SerializeField] private AudioClip cantFinalize;
    [SerializeField] private AudioClip finalize;

    public static QuestSystem Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public QuestSystemUIManager UI;
    public List<Quest> activeQuests = new List<Quest>(); // Modificado para lista de quests ativas

    public void StartUI(QuestsData data, NPCData npcData)
    {
        activeQuests.Clear(); // Limpa as quests ativas ao iniciar a UI
        UI.quests = data.quest;
        UI.npcInfo = npcData;
        UI.UpdateUI();
    }

    public bool QuestAccepted(Quest acceptedQuest)
    {
        if (activeQuests.Contains(acceptedQuest))
        {
            PopUpSystem.Instance.SendMsg("Essa quest j� foi aceita.", MessageType.Alert, 3f);
            UI.UpdateUI();
            PlaySound(cantAccept);
            return false;
        }

        acceptedQuest.accepted = true;
        activeQuests.Add(acceptedQuest); // Adiciona a quest � lista de quests ativas
        UI.QuestAccepted(acceptedQuest);
        PopUpSystem.Instance.SendMsg("Quest Aceita!", MessageType.Message, 3f);
        UI.UpdateUI();
        PlaySound(accept);
        return true;
    }

    public bool Deliver(Quest selectedQuest)
    {
        // Lista tempor�ria para armazenar itens que ser�o removidos do invent�rio
        List<QuestItem> tempRemovedItems = new List<QuestItem>();

        // Verifica se o invent�rio possui todos os itens necess�rios para a quest selecionada
        if (!CheckAndRemoveItems(selectedQuest, tempRemovedItems))
        {
            // Se faltar algum item, devolve os itens ao invent�rio
            foreach (var item in tempRemovedItems)
            {
                Inventory.Instance.AddItem(item.item);
            }

            PopUpSystem.Instance.SendMsg("Voc� n�o tem todos os itens necess�rios ainda", MessageType.Alert, 3f);
            PlaySound(cantFinalize);
            return false;
        }

        // Finaliza a quest e recompensa o jogador
        PlayerStts.Instance.money += selectedQuest.reward;
        selectedQuest.accepted = false;
        selectedQuest.completed = true;
        activeQuests.Remove(selectedQuest); // Remove a quest da lista de quests ativas
        UI.UpdateUI();
        PopUpSystem.Instance.SendMsg("Quest finalizada com sucesso", MessageType.Message, 3f);
        PlaySound(finalize);
        return true;
    }

    bool CheckAndRemoveItems(Quest selectedQuest, List<QuestItem> tempRemovedItems)
    {
        foreach (var requiredItem in selectedQuest.necessaryItems)
        {
            if (Inventory.Instance.inventory.Contains(Inventory.Instance.SearchItem(requiredItem.item)))
            {
                // Adiciona o item � lista tempor�ria e remove do invent�rio
                tempRemovedItems.Add(new QuestItem(requiredItem.item));
                Inventory.Instance.RemoveItem(requiredItem.item, null);
            }
            else
            {
                return false; // Faltou um item, ent�o n�o pode finalizar a quest
            }
        }

        return true; // Todos os itens necess�rios est�o presentes
    }

    void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}

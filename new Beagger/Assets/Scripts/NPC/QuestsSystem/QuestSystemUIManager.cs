using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestSystemUIManager : MonoBehaviour
{
    public GameObject questUI;
    public GameObject dialogUI;

    public NPCData npcInfo;
    [SerializeField] TextMeshProUGUI lbl_NPCName;
    [SerializeField] GameObject prefabTaskButton;
    [SerializeField] Transform prefabTaskButtonParent;

    List<GameObject> cards = new List<GameObject>();
    public List<Quest> quests;
    public Quest selectedQuest;

    public void QuestAccepted(Quest quest)
    {
        selectedQuest = quest;
        UpdateUI();
    }

    public void OpenUI()
    {
        GeneralUIManager.Instance.animator.SetBool("QuestPanal", true);
        UpdateUI();
    }

    public void UpdateUI()
    {
        lbl_NPCName.text = npcInfo.NPCName;
        foreach (var card in cards)
        {
            Destroy(card);
        }
        cards.Clear();

        if (quests != null)
        {
            foreach (var quest in quests)
            {
                if (!quest.completed)
                {
                    GameObject newButton = Instantiate(prefabTaskButton, prefabTaskButtonParent);
                    QuestCard newQuestCard = newButton.GetComponent<QuestCard>();
                    newQuestCard.SetValues(quest);
                    cards.Add(newButton);
                }
            }
        }
    }

    public void CloseUI()
    {
        GeneralUIManager.Instance.animator.SetBool("QuestPanal", false);
        questUI.SetActive(false);
        dialogUI.SetActive(true);
        GeneralReferences.Instance.DialogSystem.isDialogActive = true;
    }
}

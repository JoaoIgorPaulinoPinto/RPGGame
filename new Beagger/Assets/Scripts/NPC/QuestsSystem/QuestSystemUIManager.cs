using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Quest;
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
        UpdateUI(); // Atualiza a UI após definir a missão selecionada
    }

    public void OpenUI()
    {
        GeneralUIManager.Instance.animator.SetBool("QuestPanal", true);
        UpdateUI();
    }
    public void UpdateUI()
    {
        lbl_NPCName.text = npcInfo.NPCName;
        // Limpa as cartas anteriores
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
    // Recria as cartas com base nas missões atuais


    public void CloseUI()
    {
        GeneralUIManager.Instance.animator.SetBool("QuestPanal", false);
        questUI.SetActive(false);
        dialogUI.SetActive(true);
        GeneralReferences.Instance.DialogSystem.isDialogActive = true;
    }
}
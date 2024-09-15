    using System.Collections.Generic;
    using TMPro;
    using Unity.VisualScripting;
    using UnityEngine;
    using static Quest;
    public class QuestSystemUIManager : MonoBehaviour
    {

        public NPCData npcInfo;
        [SerializeField] TextMeshProUGUI lbl_NPCName;


        [SerializeField] GameObject prefabTaskButton;
        [SerializeField] Transform prefabTaskButtonParent;

         List<GameObject> cards = new List<GameObject>();
  

        public List<Quest> quests;

        Quest selectedQuest;
    public void QuestAccepted(Quest quest)
    {
        selectedQuest = quest;
        UpdateUI(); // Atualiza a UI após definir a missão selecionada
    }

    public void OpenUI()
    {
       
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

        // Recria as cartas com base nas missões atuais
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

    public void CloseUI()
        {
            //PlayerControlsManager.Instance.realease = true;
        }
    }
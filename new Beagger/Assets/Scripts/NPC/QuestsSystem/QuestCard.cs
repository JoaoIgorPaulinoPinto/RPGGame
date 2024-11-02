using TMPro;
using UnityEngine;

public class QuestCard : MonoBehaviour
{
    [SerializeField] AudioClip clip_click;
    [SerializeField] AudioClip clip_mouseEnter;
    [SerializeField] GameObject btnAcpt;
    [SerializeField] GameObject btnEntregar;
    [SerializeField] Transform itensListParent;
    [SerializeField] GameObject itensPrefab;
    [SerializeField] TextMeshProUGUI lbl_reconpensa;
    [SerializeField] TextMeshProUGUI lbl_Questtext;
    [SerializeField] Quest quest;

    public void SetValues(Quest quest)
    {
        lbl_reconpensa.text = "Recompensa: R$" + quest.reward.ToString();
        lbl_Questtext.text = quest.description;
        this.quest = quest;

        foreach (var item in quest.necessaryItems)
        {
            GameObject NI = Instantiate(itensPrefab, itensListParent);
            NI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.item.itemName;
        }

        UpdateButtons();
    }

    public void UpdateButtons()
    {
        if (this.quest.accepted)
        {
            btnAcpt.SetActive(false);
            btnEntregar.SetActive(true);
        }
        else
        {
            btnAcpt.SetActive(true);
            btnEntregar.SetActive(false);
        }
    }

    public void Accept()
    {
        QuestSystem.Instance.QuestAccepted(quest);
        UpdateButtons();
    }

    public void Deliver()
    {
        bool success = QuestSystem.Instance.Deliver(quest);
        if (success)
        {
            UpdateButtons();
        }
    }

    public void OnPointerEnter()
    {
        GeneralReferences.Instance.UIAudioSource.clip = clip_mouseEnter;
        GeneralReferences.Instance.UIAudioSource.Play();
    }

    public void OnPointerClick()
    {
        GeneralReferences.Instance.UIAudioSource.clip = clip_click;
        GeneralReferences.Instance.UIAudioSource.Play();
    }
}

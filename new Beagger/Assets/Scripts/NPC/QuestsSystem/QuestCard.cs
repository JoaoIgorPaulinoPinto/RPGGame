using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        lbl_reconpensa.text = "Reconpensa: R$" +  quest.reward.ToString();
        lbl_Questtext.text = quest.description;
        this.quest = quest;

        foreach (var item in quest.necessaryItens)
        {
            GameObject NI = Instantiate(itensPrefab, itensListParent);
            NI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.itemName;
        }


        UpdateButtons();
    }
    public void UpdateButtons()
    {

        if (this.quest.acepted)
        {
            btnAcpt.SetActive(false);
            btnEntregar.SetActive(true);
            print("botao entregar");
        }
        else
        {
            print("botao aceitar");
            btnAcpt.SetActive(true);
            btnEntregar.SetActive(false);
        }
    }
    public void Accept()
    {
        QuestSystem.Instance.QuestAccepted(quest);
        UpdateButtons(); // Atualiza os botões após aceitar a missão
    }
    public void Deliver()
    {
        bool success = QuestSystem.Instance.Deliver();
        if (success)
        {
            UpdateButtons(); // Atualiza os botões após entregar a missão
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
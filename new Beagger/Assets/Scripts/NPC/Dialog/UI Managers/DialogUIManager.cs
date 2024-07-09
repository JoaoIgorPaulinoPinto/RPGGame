using UnityEngine;
using TMPro;

public class DialogUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogTextUI;
    [SerializeField] private TextMeshProUGUI characterNameUI;
    [SerializeField] private GameObject btnNext;
    [SerializeField] private GameObject btnClose;


    public void SetCharacterName(string characterName)
    {
        characterNameUI.text = characterName;
    }

    public void AppendDialogText(char character)
    {
        dialogTextUI.text += character;
    }

    public void ClearDialog()
    {
        dialogTextUI.text = "";
    }

    public void DisableNextButton()
    {
        btnNext.SetActive(false);
    }

    public void EnableCloseButton()
    {
        btnClose.SetActive(true);
    }


}   
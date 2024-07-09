using UnityEngine;
using UnityEngine.UI;

public class DialogCanvasManager : MonoBehaviour
{
    [SerializeField] Button btnStartDialog;
    [SerializeField] Button btnNextPage;
    [SerializeField] GameObject panel;

    public void EnableStartDialogButton(DialogManager dialogManager)
    {
        if (!dialogManager.isOnConversation)
        {
            btnStartDialog.gameObject.SetActive(true);
            btnNextPage.gameObject.SetActive(true);
            btnStartDialog.onClick.AddListener(dialogManager.StartDialog);

            btnNextPage.onClick.AddListener(dialogManager.NextPage);
        }
      
    }
    public void DisableStartDialogButton(DialogManager dialogManager)
    {

            btnStartDialog.gameObject.SetActive(false);
            btnStartDialog.onClick.RemoveListener(dialogManager.StartDialog);

            btnNextPage.onClick.RemoveListener(dialogManager.NextPage);
            panel.SetActive(false);
            dialogManager.EndDialog();



    }
    
}

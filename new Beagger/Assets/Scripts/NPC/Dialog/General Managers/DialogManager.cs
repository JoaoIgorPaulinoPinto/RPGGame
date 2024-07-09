using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static NPCDialogInfo;

public class DialogManager : MonoBehaviour
{
    [Header("Scriptable Object deste dialogo")]
    [SerializeField] private NPCDialogInfo npcInfo; // Changed variable name to npcInfo

    [SerializeField] private DialogUIManager uiManager;

     List<Dialog> dialogs = new List<Dialog>(); // Changed variable name to dialogs
     List<Page> pages = new List<Page>();

    [SerializeField] private CameraMovimentation cam;

    private int currentPage;

    private Coroutine typingCoroutine;

    public bool isOnConversation;

    public void StartDialog()
    {
        isOnConversation = true;
        List<Dialog> expandedDialogs = new List<Dialog>();

        if (npcInfo.dialogtexts.Count > 0)
        {
            foreach (var item in npcInfo.dialogtexts)
            {
                for (int i = 0; i < item.rarityIndex; i++)
                {
                    expandedDialogs.Add(item);
                }
            }
        }

        expandedDialogs = npcInfo.dialogtexts ;

        if (npcInfo.dialogtexts.Count > 0)
        {
            foreach (var item in npcInfo.dialogtexts)
            {
                if (item.once && !item.oncePageIsPlayed)
                {
                    dialogs.Clear(); // Clear previous dialogs
                    dialogs.Add(item); // Add current dialog
                    SetPages();
                    SetCharIndo();
                    cam.isUsing = true;
                    currentPage = -1;
                    NextPage();
                    item.oncePageIsPlayed = true;
                    return;
                }
            }

            var randomDialog = npcInfo.dialogtexts[Random.Range(0, npcInfo.dialogtexts.Count)];
            dialogs.Clear(); // Clear previous dialogs
            dialogs.Add(randomDialog); // Add random dialog
            SetPages();
            SetCharIndo();
            cam.isUsing = true;
            currentPage = -1;
            NextPage();
        }
    }

    public void EndDialog()
    {
        isOnConversation = false;

        cam.isUsing = false;
        pages.Clear();
        uiManager.ClearDialog();
        
    }

    public void NextPage()
    {
        currentPage++;

        if (currentPage < pages.Count)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(pages[currentPage].message, dialogs[0].letterDelay));
        }
        else
        {
            // Se chegamos ao final do diálogo, desativamos o botão "Next"
            uiManager.DisableNextButton();
            uiManager.EnableCloseButton();
        }
    }

    private IEnumerator TypeText(string text, float delay)
    {
        uiManager.ClearDialog(); // Limpa o texto da caixa de diálogo antes de começar a escrever o próximo texto
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < text.Length; i++)
        {
            uiManager.AppendDialogText(text[i]); // Adiciona cada caractere à caixa de diálogo
            yield return new WaitForSeconds(delay);
        }
    }

    private void SetPages()
    {
        pages.Clear();
        foreach (var item in dialogs[0].pages) // Accessing the first dialog
        {
            pages.Add(item);
        }
    }

    private void SetCharIndo()
    {
        uiManager.SetCharacterName(npcInfo.NPCName); // Define o nome do personagem na caixa de diálogo
    }
}

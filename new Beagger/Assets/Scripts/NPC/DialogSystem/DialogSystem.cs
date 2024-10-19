using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class Dialog
{
    public string[] pages; 
}

public class DialogSystem : MonoBehaviour
{
    [SerializeField]AudioSource audioSource;
    [SerializeField]AudioClip clip;

    NPCData npc;

    [SerializeField] QuestsData qstData;
    
    public TextMeshProUGUI lbl_text; 
    public TextMeshProUGUI lbl_name; 
    public GameObject UI;
    public GameObject questsUI;
    public GameObject btnShowQuests;

  
    public static DialogSystem Instance { get; private set; }

    public Dialog dialogInfo; 
    public float typingSpeed = 0.05f;

    private int currentPageIndex = 0;
    private bool isTyping = false; 
    public bool isDialogActive = false; 


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

    // Inicia o diálogo
    public void StartDialog()
    {
       GeneralUIManager.Instance.animator.SetBool("DialogCanvas", true);



        // btnShowQuests.SetActive(false);
        questsUI.SetActive(false);

        isDialogActive = true;
        UI.SetActive(true);
        lbl_name.text = npc.NPCName;

        currentPageIndex = 0; 
        StartCoroutine(TypeText(dialogInfo.pages[currentPageIndex]));
    }


    // Função para digitar o texto gradualmente
    private IEnumerator TypeText(string text)
    {
        
        isTyping = true;
        lbl_text.text = ""; 

        foreach (char letter in text.ToCharArray())
        {
            audioSource.clip = clip;
            audioSource.Play();
            lbl_text.text += letter; 
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    // Função para passar para a próxima página do diálogo
    public void NextPage()
    {
        if (qstData)
        {
            if (currentPageIndex >= dialogInfo.pages.Length - 2)
            {
                btnShowQuests.SetActive(true);
            }
            else
            {
                btnShowQuests.SetActive(false);
            }

        }
        else
        {
            btnShowQuests.SetActive(false);
        }
        if (isTyping)
        {
            StopAllCoroutines(); // Para a corrotina atual
            lbl_text.text = dialogInfo.pages[currentPageIndex];
            isTyping = false;
        }
        else if (currentPageIndex < dialogInfo.pages.Length - 1)
        {
            currentPageIndex++;
            StopAllCoroutines(); // Garante que nenhuma corrotina antiga esteja rodando
            StartCoroutine(TypeText(dialogInfo.pages[currentPageIndex]));

           
        }
        else
        {
            EndDialog();
        }
    }


    // Função para encerrar o diálogo
    private IEnumerator EndDialogCoroutine()
    {
        qstData = null;
        npc = null;
        isDialogActive = false;
       
        GeneralUIManager.Instance.animator.SetBool("DialogCanvas", false);
        Debug.Log("Dialog ended."); 

        yield return new WaitForSeconds(0.3f);
        UI.SetActive(false);
        lbl_text.text = "";
        PlayerControlsManager.Instance.realease = true;
        
    }

    public void EndDialog()
    {
        isDialogActive =false;
        StartCoroutine(EndDialogCoroutine());
    }
    public void SetData(Dialog data, NPCData npcData, QuestsData? questData)
    {
        this.dialogInfo = data;
        this.npc = npcData;
        this.qstData = questData;
    }

    public bool IsDialogActive
    {
        get { return isDialogActive; }
    }

    public void ShowQuests()
    {
        questsUI.SetActive(true);
        isDialogActive = false;
        if(qstData != null)
        {
            QuestSystem.Instance.StartUI(qstData, npc);
        }
     
      
    }

    private void Update()
    {
        if (isDialogActive)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)){
                if (currentPageIndex == dialogInfo.pages.Length - 1)
                {
                    EndDialog();
                }
                else
                {
                    NextPage();
                }
            }
          
        }
    }
}

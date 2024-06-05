using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogManager : MonoBehaviour
{
    //CRIAR MAIS PRA FRENTE POSSIBILIDADES DE DIALOGOS DIFERENTES
   
    [SerializeField]CameraMovimentation cam;

    [System.Serializable]
    public class Page
    {
        public string message;
    }
    public List<Page> pages = new List<Page>();

    public string NPCName;
    public int currrentPage;

    [SerializeField] TextMeshProUGUI labelTxt;
    [SerializeField] TextMeshProUGUI labelName;

    [SerializeField] GameObject btnNext;    
   
    [SerializeField] GameObject btnClose;    
    [SerializeField] GameObject dialogScreen;    
    [SerializeField] float letterDelay;


    private Coroutine typingCoroutine;
    public void NextPage()
    {
        currrentPage++;

        if (currrentPage < pages.Count)
        { 
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(pages[currrentPage].message, letterDelay));
        }
    }
    private void Update()
    {
        if (currrentPage >= (pages.Count -1))
        {
            btnClose.SetActive(true);
            btnNext.SetActive(false);
        }
        else
        {
            btnClose.SetActive(false);
            btnNext.SetActive(true);
        }
    }
    private IEnumerator TypeText(string text, float delay)
    {
        labelTxt.text = "";
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < text.Length; i++)
        {
            labelTxt.text += text[i];
            yield return new WaitForSeconds(delay);
        }

    }
    public void StartDialog()
    {
        currrentPage = -1; // Start at -1 so that the first call to NextPage sets it to 0
        NextPage();
        SetCharIndo();
        cam.isUsing = true;
    }

    void SetCharIndo()
    {
        labelName.text = NPCName;
    }
    public void EndDialog()
    {
        cam.isUsing = false;
    }
}

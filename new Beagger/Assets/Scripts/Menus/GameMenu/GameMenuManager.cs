using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject menuScreen;
    [SerializeField] GameObject background;
    public GameObject[] screens;
    public bool isOpen;

    public void OpenGameMenu()
    {
        if (PlayerControlsManager.Instance.realease)
        {
            GeneralUIManager.Instance.SetAnimatioState("GameMenu", true);
            PlayerControlsManager.Instance.realease = false;
            background.SetActive(true);
            isOpen = true;
            ActiveOrUnactiveScreen(menuScreen);
            TimeController.Instance.stop = true;
        }

    }

    public void BackToGame()   //volta ao jogo
    {
        if (!PlayerControlsManager.Instance.realease)
        {

            GeneralUIManager.Instance.SetAnimatioState("GameMenu", false);
            StartCoroutine(closeGameMenu());
            PlayerControlsManager.Instance.realease = true;
            isOpen = false;

            background.SetActive(false);


        }
    }
    IEnumerator closeGameMenu()
    {
        yield return new WaitForSecondsRealtime(0.3f);  // Usa o tempo real, ignorando o Time.timeScale
        TimeController.Instance.stop = false;
        ActiveOrUnactiveScreen(null);
    }
    public void OpenOptions(GameObject screen) // abre as configurações
    {
        

        ActiveOrUnactiveScreen(screen);
    }

    public void OpenCalendary(GameObject screen)    // abre o calendário
    {
        ActiveOrUnactiveScreen(screen);
    }

    public void BackToMainMenu()   //volta para o menu principal
    {
       
        SceneManager.LoadScene("MainMenuScene"); 
    }

    void ActiveOrUnactiveScreen(GameObject? screen)
    {


        foreach (var item in screens)
        {
            item.SetActive(false);
        }
        if (screen)
        {
            screen.SetActive(true);
        }
    }
}

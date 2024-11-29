using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    

    public Animator animator;

    public GameObject[] screens;

    public void Play()
    {
        SceneManager.LoadScene("ComingCutScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits(GameObject screen)
    {
        ActiveOrUnactiveScreen(screen);
        screen.TryGetComponent<CreditsManager>(out CreditsManager cdts);
        cdts.Open();
    }

    public void ConfigScreen(GameObject screen)
    {
        ActiveOrUnactiveScreen(screen);
        screen.TryGetComponent<ConfigurationScreenManager>(out ConfigurationScreenManager cfgScreen);
        cfgScreen.LoadSettings();
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
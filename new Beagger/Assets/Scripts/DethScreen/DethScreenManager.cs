using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DethScreenManager : MonoBehaviour
{

    [SerializeField] GameObject UI;
    [SerializeField] GameObject gameHud;

    public void BackToMainMenu()
    {

    }
    public void Open()
    {
        TimeController.Instance.stop = true;
        gameHud.SetActive(false);
        UI.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FastTravelUIManager : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] Button btnAccept;
    [SerializeField] Button btnDeny;
    [SerializeField] TextMeshProUGUI lbl_msg;
    [SerializeField] TextMeshProUGUI lbl_price;

    [SerializeField] FastTravelSystem FastTravelSystem;
    public void StartUI(float price, string message, FastTravelSystem fastTravelSystem)
    {
        FastTravelSystem = fastTravelSystem;
        btnAccept.onClick.RemoveAllListeners();
        btnDeny.onClick.RemoveAllListeners();
        btnAccept.onClick.AddListener(Accept);
        btnDeny.onClick.AddListener(Deny);
        lbl_msg.text = message;
        lbl_price.text = "Preço: " + price.ToString("C2");
        UI.SetActive(true);
        GeneralUIManager.Instance.animator.SetBool("FastTravel", true);
        PlayerControlsManager.Instance.realease = false;
    }
    public void Accept()
    {
        FastTravelSystem.Accepted();
    }
    public void Deny()
    {
        FastTravelSystem.Denied();
        CloseUI();

    }
    public void CloseUI()
    {
        StartCoroutine(closeUI());
    }
    IEnumerator closeUI()
    {
        GeneralUIManager.Instance.animator.SetBool("FastTravel", false);
        yield return new WaitForSeconds(0.4f);
        UI.SetActive(false);
        PlayerControlsManager.Instance.realease = true;
    }
}

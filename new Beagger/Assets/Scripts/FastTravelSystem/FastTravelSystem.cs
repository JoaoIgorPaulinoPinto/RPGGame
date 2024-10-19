using System;
using System.Collections;
using UnityEngine;

public class FastTravelSystem : InteractableGameObject, IInteractable
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip goingClip;
    [SerializeField] AudioClip trainClip;
    [SerializeField] AudioClip arrivingClip;

    public Transform targetPoint;
    public float price;
    public string message;

    [SerializeField] FastTravelUIManager uiMangager;

    public void Interact()
    {
        uiMangager.StartUI(price, message);
    }
    public void Accepted()
    {
        if(PlayerStts.Instance.money >= price)
        {
            StartCoroutine(teleportRotine());
            uiMangager.CloseUI();
        }
        else
        {
            uiMangager.CloseUI();
            PopUpSystem.Instance.SendMsg("Você não possui dinheiro suficiente", MessageType.Alert, null);
            Denied();
        }
    }

    IEnumerator teleportRotine()
    {
        audioSource.clip = goingClip;
        audioSource.Play();
        GeneralUIManager.Instance.animator.SetBool("Teleporting", true);
        audioSource.clip = trainClip;
        audioSource.Play();
        yield return new WaitForSeconds(2);
        PlayerStts.Instance.playerBody.position = targetPoint.position;
        yield return new WaitForSeconds(10);

        audioSource.clip = arrivingClip;
        audioSource.Play();
        yield return new WaitForSeconds(0.3f);

        GeneralUIManager.Instance.animator.SetBool("Teleporting", false);
    }
    public void Denied()
    {
       //o trem vai embora
    }
}

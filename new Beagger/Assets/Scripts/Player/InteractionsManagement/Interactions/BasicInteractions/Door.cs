using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableGameObject, IInteractable
{
    public bool locked;

    [SerializeField]AudioSource audioSource;
    [SerializeField]AudioClip open;
    [SerializeField]AudioClip close;

    public Door otherSide;
    public Transform point;
    public GameObject interior;
    public bool onInterior;
   
    public void Interact()
    {
        if (!locked)
        {
            StartCoroutine(teleportRotine());
        }
        else
        {
            PopUpSystem.Instance.SendMsg("Parece que está fechado...", MessageType.Message, null);  
        }
    }
    IEnumerator teleportRotine()
    {
        PlayerControlsManager.Instance.realease = false;
        otherSide.gameObject.SetActive(true);

        GeneralUIManager.Instance.animator.SetBool("Teleporting", true);
        audioSource.clip = open;
        audioSource.Play();

        yield return new WaitForSeconds(0.5f);
        
        PlayerStts.Instance.playerBody.position = otherSide.point.position;
        yield return new WaitForSeconds(0.5f);
        audioSource.clip = close;
        audioSource.Play();
        GeneralUIManager.Instance.animator.SetBool("Teleporting", false);
       
        if (onInterior)
        {
            interior.SetActive(false);
            
        }
        else
        {
            interior.SetActive(true);
        }
        gameObject.SetActive(false);

        PlayerControlsManager.Instance.realease = true;
    }
}

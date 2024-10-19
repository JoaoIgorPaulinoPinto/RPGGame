using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtentionMsg :  InteractableGameObject, IInteractable
{
    [SerializeField] string msg;
    [SerializeField] MessageType msgType;
    [SerializeField] float timeToWait;

    public void Interact()
    {
        PopUpSystem.Instance.SendMsg(msg, msgType, timeToWait);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Interact();
        }
    }
}

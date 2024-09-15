using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableGameObject, IInteractable
{
    public void Interact()
    {
        print("Porta aberta");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : InteractableGameObject, IInteractable
{
    public TrunkSystem trunk;
    public float maxWeight;
    public List<TrunkItems> items;
    public void Interact()
    {  
        trunk.Use(maxWeight,items, gameObject);
    }

}

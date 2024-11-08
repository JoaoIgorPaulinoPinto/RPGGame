using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : InteractableGameObject, IInteractable
{
    public TrunkSystem trunkSys;
    public float maxWeight;
    public List<TrunkItems> items;
    public void Interact()
    {
        trunkSys = GeneralReferences.Instance.TrunkSystem;
        trunkSys.Use(maxWeight,items, this);
    }

}

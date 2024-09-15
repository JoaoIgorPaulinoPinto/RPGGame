using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : InteractableGameObject, IInteractable
{
  

    [SerializeField]GameObject UI;
    public StoreInfo StoreInfo;
    [SerializeField] ComerceUI comerceUI;
    public void Interact()
    {
        PurchaseSystem purchaseSystem;
        TryGetComponent<PurchaseSystem>(out purchaseSystem);

        SaleSystem saleSystem;
        TryGetComponent<SaleSystem>(out saleSystem);


        purchaseSystem.Store = StoreInfo;
        saleSystem.Store = StoreInfo;

        comerceUI.SetStoreValues(purchaseSystem, saleSystem);

        comerceUI.Open();
    }
}

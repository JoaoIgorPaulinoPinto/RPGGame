using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : InteractableGameObject, IInteractable
{
    
    public int timeClose;
    public int timeOpen;
    [SerializeField]GameObject UI;
    public StoreInfo StoreInfo;
    [SerializeField] ComerceUI comerceUI;
    public void Interact()
    {
        if (TimeController.Instance.dayTimer < timeClose || TimeController.Instance.dayTimer < timeOpen)
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
        else
        {
            PopUpSystem.Instance.SendMsg("Parece que está fechado...",MessageType.Message,null);
        }
       
    }
}

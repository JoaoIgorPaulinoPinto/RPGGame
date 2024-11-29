using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : InteractableGameObject, IInteractable
{
    // COLOCAR ISSO NO OBJETO PORTA, PARA PODER E NAO PODER ENTRAR DEPENDENDO DO HORARIO

    [HideInInspector] public bool isOpen = true;

    [SerializeField]GameObject UI;
    public StoreInfo StoreInfo;
    [SerializeField] ComerceUI comerceUI;
    public void Interact()
    {
        if (isOpen)
        {
            PurchaseSystem purchaseSystem;
            TryGetComponent<PurchaseSystem>(out purchaseSystem);

            SaleSystem saleSystem;
            TryGetComponent<SaleSystem>(out saleSystem);

            if (purchaseSystem)
            {
                purchaseSystem.Store = StoreInfo;
            }
            if (saleSystem)
            {
                saleSystem.Store = StoreInfo;

            }

            comerceUI.SetStoreValues(purchaseSystem, saleSystem);

            comerceUI.Open();

        }
        else
        {
            PopUpSystem.Instance.SendMsg("Parece que está fechado...", MessageType.Message, null);
        }

    }


}

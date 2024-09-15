using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComerceUI : MonoBehaviour
{
    [SerializeField] PlayerControlsManager playerControlsManager;

    public GameObject GameHUD;

    public GameObject UIPurchase;
    public GameObject UISale;

    PurchaseSystemUIManager pur;
    SaleSystemUIManager sale;
    PurchaseSystem purSys;
    SaleSystem saleSys;
    public void SetStoreValues(PurchaseSystem purInfo, SaleSystem saleInfo)
    {
        purSys = purInfo;
        saleSys = saleInfo;

        UIPurchase.TryGetComponent<PurchaseSystemUIManager>(out pur);
    
        UISale.TryGetComponent<SaleSystemUIManager>(out sale);

        if (pur != null && sale != null)
        {
            pur.purchaseSystem = purInfo;

            sale.saleSystem = saleInfo;
        }
    }
    public void SetTabAsActive(GameObject button)
    {
        pur.UpdateUI();
        pur.UpdateUI();
        saleSys.SetInventoryItemsAsProducts();

        sale.UpdateUI();
        if (button == UIPurchase)
        {

            UIPurchase.SetActive(true); UISale.SetActive(false);



        }
        else if (button == UISale)
        {
            

            UIPurchase.SetActive(false); UISale.SetActive(true);




        }
        else
        {
            print("ERRO<<TELA NAO ENCONTRADA>>");
        }
    }

    public void Open()
    {
        SetTabAsActive(UISale); 
        playerControlsManager.realease = false;
        GameHUD.SetActive(false);
    }
    public void Close()
    {
        playerControlsManager.realease = true;
        UIPurchase.SetActive(false);
        UISale.SetActive(false);

        GameHUD.SetActive(true) ;
    }
}

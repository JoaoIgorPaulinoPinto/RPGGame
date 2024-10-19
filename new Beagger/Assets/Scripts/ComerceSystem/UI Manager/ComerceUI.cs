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

        UIPurchase.TryGetComponent<PurchaseSystemUIManager>(out PurchaseSystemUIManager i);
        pur = i;
        UISale.TryGetComponent<SaleSystemUIManager>(out SaleSystemUIManager a);
        sale = a;

        if (pur != null && sale != null)
        {
            pur.purchaseSystem = purInfo;

            sale.saleSystem = saleInfo;
        }
    }
    public void SetTabAsActive(GameObject button)
    {

        if(!sale || !saleSys || !sale)
        {
            return;
        }else
        {
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
      
    }

    public void Open()  
    {
        GeneralUIManager.Instance.animator.SetBool("Comerce", true);
        pur.UpdateUI();

        saleSys.SetInventoryItemsAsProducts();

        sale.UpdateUI();
        SetTabAsActive(UISale); 
        playerControlsManager.realease = false;
        GameHUD.SetActive(false);
    }
    public void Close()
    {
        StartCoroutine(waitToClose());
    }
    IEnumerator waitToClose()
    {
        GeneralUIManager.Instance.animator.SetBool("Comerce", false);

        GameHUD.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerControlsManager.realease = true;
        UIPurchase.SetActive(false);
        UISale.SetActive(false);

    }
}

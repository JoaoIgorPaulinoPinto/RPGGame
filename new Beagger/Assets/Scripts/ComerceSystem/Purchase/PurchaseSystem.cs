using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoreInfo
{ 
    public string StoreName;
    public string SellerName;
    public StoreType StoreType;


    public StoreInfo (string storeName, string sellerName, StoreType storeType)
    {
        this.StoreName = storeName;
        this.SellerName = sellerName;
        this.StoreType = storeType;
    }
}

public class PurchaseSystem : MonoBehaviour
{
    [SerializeField] AudioClip noMoney;

    [SerializeField] AudioSource audioSource;
    [Space]

    [SerializeField]ProductsGeneralTable productsGeneralTable;

    public StoreInfo Store;
        
    

    PurchaseSystemUIManager UIManager;

    public List<Product> selectedProducts = new List<Product>();
    
    public List<Product> products = new List<Product>();

    public float totalValue = 0;
    
    public void PricesCorrection()
    {
        if (products.Count > 0)
        {
            foreach (var item in products)
            {
                Product foundProduct = productsGeneralTable.products.Find(product => item.item.itemName == product.item.itemName);
                item.price = foundProduct.price;
            }
        }
      
    }

    public void UpdateTotalValue()
    {
        totalValue = 0;
        foreach (var item in selectedProducts)
        {
            totalValue += item.price;
        }
    }
    public void PurchaseCompleted()
    {
        if(totalValue > PlayerStts.Instance.money)
        {
            print("Voce não possui dinheiro suficiente");
            UIManager.audioSource.clip = noMoney;
            audioSource.Play();

        }
        else
        {
            foreach (var item in selectedProducts)
            {
                
                if (Inventory.Instance.AddItem(item.item)) { }
                else
                {
                    print("Você não possui espaço no inventario");
                    Instantiate(item.item.prefab, Inventory.Instance.transform.position, Quaternion.identity);
                    return;
                }
            }

            selectedProducts.Clear();
            PlayerStts.Instance.money -= totalValue;
        }
      
    }
    public void PurchaseCanceled()
    {
        selectedProducts.Clear();
    }
}

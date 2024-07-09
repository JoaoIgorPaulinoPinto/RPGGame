using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Store
{
    public string StoreName;
    public string SellerName;
    public StoreType StoreType;

}

public class PurchaseSystem : MonoBehaviour
{
    ProductsGeneralTable productsGeneralTable;

    public Store Store;

    public PlayerStts playerStts;
    public Inventory inventory;

    PurchaseSystemUIManager UIManager;

    public List<Product> selectedProducts = new List<Product>();
    public List<Product> products = new List<Product>();

    public float totalValue = 0;
    
    public void PricesCorrection()
    {
        foreach (var item in products)
        {
            Product foundProduct = productsGeneralTable.products.Find(product => item.item.itemName == product.item.itemName);
            item.price = foundProduct.price;
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
        if(totalValue > playerStts.money)
        {
            print("Voce não possui dinheiro suficiente");
        }
        else
        {
            foreach (var item in selectedProducts)
            {
                if (inventory.AddItem(item.item)) { } else { print("Você não possui espaço no inventario"); return;}
            }

            selectedProducts.Clear();
            playerStts.money -= totalValue;
        }
        
    }
    public void PurchaseCanceled()
    {
        selectedProducts.Clear();
    }
}

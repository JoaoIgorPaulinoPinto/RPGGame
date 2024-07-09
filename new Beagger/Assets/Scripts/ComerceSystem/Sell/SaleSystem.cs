using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaleSystem : MonoBehaviour
{
     [SerializeField] ProductsGeneralTable productsGeneralTable;

    public Store Store;

    public PlayerStts playerStts;
    public Inventory inventory;

    PurchaseSystemUIManager UIManager;

    public List<Product> selectedProducts = new List<Product>();
    public List<Product> inventoryProducts = new List<Product>();

    public float totalValue = 0;

    public void SetInventoryItemsAsProducts()
    {
        inventoryProducts.Clear();
        selectedProducts.Clear();
        foreach (var item in inventory.inventory)
        {
            for (int i = 0; i < item.quant; i++)
            {
                Product foundProduct = productsGeneralTable.products.Find(product => item.item.itemName == product.item.itemName);

                Product newProduct = new Product(foundProduct.item, foundProduct.price);

                inventoryProducts.Add(newProduct);
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
    public void SaleCompleted()
    {
        totalValue = 0;

        foreach (var item in selectedProducts)
        {
            totalValue += item.price;
            inventory.RemoveItem(inventory.SerachForItem(item.item));
        }
        selectedProducts.Clear();
        playerStts.money += totalValue;
    }
    public void SaleCanceled()
    {
        selectedProducts.Clear();
    }
}

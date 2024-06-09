using System.Collections.Generic;
using UnityEngine;

using static ProductsGeneralTable;

public class BuyItems : MonoBehaviour
{
    [SerializeField] ProductsGeneralTable table;
    [SerializeField] BuyItemsUIManager UIManager;
    public PlayerStts playerStts;
    [SerializeField] Inventory inventory;


    public List<Item> selectedProducts = new List<Item>();
    public List<Product> Products = new List<Product>();

    public void FinalizePurchase()
    {
        if (selectedProducts.Count > 0)
        {
            for (int i = selectedProducts.Count - 1; i >= 0; i--)
            {
                inventory.AddItem(selectedProducts[i]);
                var item = selectedProducts[i];
                playerStts.money -= SearchForItemInProduct(item).price;
                selectedProducts.RemoveAt(i);
            }
        }
    }
    
    public void AddToBuyList(Item item)
    {
        selectedProducts.Add(item);
    }

    public void RemoveFromBuyList(Item item)
    {
        selectedProducts.Remove(item);

    }

    public Product SearchForItemInProduct(Item itemToSearch)
    {
        foreach (var product in Products)
        {
            if (product.item == itemToSearch)
            {
                return product;
            }
        }
        return null;
    }
    private void StartProductTable()
    {

        foreach (var item in Products)
        {
            Product product = table.products.Find(i => i.item.itemName == item.item.itemName);
            if (product != null)
            {
                item.price = product.price;
            }
        }
    }

    private void Start()
    {
        StartProductTable();
    }
}

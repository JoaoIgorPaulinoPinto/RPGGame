using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItems : MonoBehaviour
{
    [SerializeField] BuyItemsUIManager UIManager;
    public PlayerStts playerStts;
    [SerializeField] Inventory inventory;

    [System.Serializable]
    public class Product
    {
        public Item item;
        public int price;
    }

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
}

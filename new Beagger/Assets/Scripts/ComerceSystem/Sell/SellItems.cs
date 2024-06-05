using System.Collections.Generic;
using UnityEngine;


public class SellItems : MonoBehaviour
{
    [SerializeField] SellItemsUIManager UIManager;
    public PlayerStts playerStts;


    [System.Serializable]
    public class Product
    {
        public Item item;
        public int price;
    }
    public List<Item> selectedProducts = new List<Item>();   
    
    public List<Product> Products = new List<Product>();

    public void FinalizeSele()
    {
        if (selectedProducts.Count > 0)
        {
            // Percorre a lista do final para o início
            for (int i = selectedProducts.Count - 1; i >= 0; i--)
            {
                var item = selectedProducts[i];
                playerStts.money += SerachForItemInProduct(item).price;
                selectedProducts.RemoveAt(i);
            }
        }
    }
    public void AddToSellList(Item item)
    {
        selectedProducts.Add(item);
        print("adicionado 2");

    }
    public void RemoveFromSellList(Item item)
    {
        selectedProducts.Remove(item);
    }
    public  Product SerachForItemInProduct(Item itemToSearch)
    {
        Product itemReturned = null;

        foreach (var item in Products)
        {
            if (item != null && item.item == itemToSearch)
            {
                itemReturned = item;
                break;
            }
        }
        return itemReturned;
    }
}

using System;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PricesManager : MonoBehaviour
{
    
    [SerializeField] SellItems sellItemsManager;
    [SerializeField] BuyItems buyItemsManager;
    public void SellChancePrices(float index)
    {
        index = Mathf.Clamp(index, -10, 10);

        foreach (var item in sellItemsManager.Products)
        {
            item.price += index * item.price / 100;
        }
    }
    public void BuyChancePrices(float index)
    {

        index = Mathf.Clamp(index, -10, 10);
        foreach (var item in buyItemsManager.Products)
        {
            item.price += index* item.price / 100;
        }
    }

    public void ChangePricesOfType(string? itemName, float index)
    {


        index = Mathf.Clamp(index, -10, 10);

        if (itemName == null)
        {
            itemName = "";
        }
        foreach (var item in buyItemsManager.Products)
        {
            if (item.item.itemName == itemName)
            {
                item.price += index * item.price / 100;
            }
        }
        foreach (var item in sellItemsManager.Products)
        {
            if (item.item.itemName == itemName)
            {
                item.price += index * item.price / 100;
            }
        }
    }

    public void SetGeneralChanges(float index)
    {

        index = Mathf.Clamp(index, -10, 10);
        foreach (var item in buyItemsManager.Products)
        {
            item.price += index * item.price / 100;
        }
        foreach (var item in sellItemsManager.Products)
        {
            item.price += index * item.price / 100;
        }
    }
   [SerializeField] TextMeshProUGUI txt1, txt3, txt4, txt2;
    public void DEBUG()
    {
       /*txt1.text = $"preço do{buyItemsManager.Products[0].item.itemName}: R${buyItemsManager.Products[0].price.ToString("00.00")}";
        txt2.text = $"preço do{buyItemsManager.Products[1].item.itemName}: R${buyItemsManager.Products[1].price.ToString("00.00")}";

        txt3.text = $"preço do{sellItemsManager.Products[0].item.itemName}: R${sellItemsManager.Products[0].price.ToString("00.00")}";
        txt4.text = $"preço do{sellItemsManager.Products[1].item.itemName}: R${sellItemsManager.Products[1].price.ToString("00.00")}";*/

    }
    private void Update()
    {
        DEBUG();
    }
}

using System;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PricesManager : MonoBehaviour
{
    [SerializeField] ProductsGeneralTable productsTable;
    public void ChangePricesOfType(string? itemName, float index)
    {
        index = Mathf.Clamp(index, -10, 10);    

        if (itemName == null)
        {
            itemName = "";
        }
        foreach (var item in productsTable.products)
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
        foreach (var item in productsTable.products)
        {
            item.price += index * item.price / 100;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public ProductsGeneralTable ProductsGeneralTable;

    public void AjustPrices()
    {
        foreach (var product in ProductsGeneralTable.products)
        {
           
            float adjustmentFactor = 1 + (500 - product.quant) / 1000f;

           
            float newPrice = product.price * adjustmentFactor;

            product.price = Mathf.Max(newPrice, 2);
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            AjustPrices();
        }
    }
}

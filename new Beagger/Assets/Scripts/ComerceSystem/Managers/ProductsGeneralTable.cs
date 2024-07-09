using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product
{
    public Item item;
    public float price;

    public Product(Item _item, float _price)
    {
        item = _item;
        price = _price;
    }
}
public class ProductsGeneralTable : MonoBehaviour
{
    public List<Product> products;
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class ProductsGeneralTable : MonoBehaviour
{
    [System.Serializable]
    public class Product
    {
        public Item item;
        public float price;
    }

    public List<Product> products;
}

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product
{
    public ItemData item;      // O item associado ao produto
    public float price;    // Pre�o atual do produto
    public int quant;
    public Product(ItemData _item, float _price)
    {
        item = _item;
        price = _price;
    }
}
public class ProductsGeneralTable : MonoBehaviour
{
    public int minProdQuant; // Quantidade m�nima de estoque
    public int demand;       // Demanda geral para todos os produtos
    public List<Product> products; // Lista de produtos no mercado

    // M�todo para ajustar os pre�os com base na demanda atual
    public void AdjustPrices()
    {
        foreach (var product in products)
        {
            // Calcula o fator de ajuste de pre�o com base na demanda e quantidade em estoque
            float adjustmentFactor = 1 + (demand - product.quant) / 1000f;

            // Ajusta o pre�o
            float newPrice = product.price * adjustmentFactor;

            // Define um pre�o m�nimo de 2
            product.price = Mathf.Max(newPrice, 2);

            // Se a quantidade de estoque estiver muito baixa, aumenta um pouco a quantidade
            if (product.quant < minProdQuant)
            {
                product.quant += minProdQuant;

                // Recalcula o pre�o com o fator de ajuste
                product.price *= adjustmentFactor;
                product.price = Mathf.Max(product.price, 2);
            }
        }
    }
    public void ReplaceProducts()
    {
        foreach (var product in products) {
            product.quant = demand;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product
{
    public ItemData item;      // O item associado ao produto
    public float price;    // Preço atual do produto
    public int quant;
    public Product(ItemData _item, float _price)
    {
        item = _item;
        price = _price;
    }
}
public class ProductsGeneralTable : MonoBehaviour
{
    public int minProdQuant; // Quantidade mínima de estoque
    public int demand;       // Demanda geral para todos os produtos
    public List<Product> products; // Lista de produtos no mercado

    // Método para ajustar os preços com base na demanda atual
    public void AdjustPrices()
    {
        foreach (var product in products)
        {
            // Calcula o fator de ajuste de preço com base na demanda e quantidade em estoque
            float adjustmentFactor = 1 + (demand - product.quant) / 1000f;

            // Ajusta o preço
            float newPrice = product.price * adjustmentFactor;

            // Define um preço mínimo de 2
            product.price = Mathf.Max(newPrice, 2);

            // Se a quantidade de estoque estiver muito baixa, aumenta um pouco a quantidade
            if (product.quant < minProdQuant)
            {
                product.quant += minProdQuant;

                // Recalcula o preço com o fator de ajuste
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

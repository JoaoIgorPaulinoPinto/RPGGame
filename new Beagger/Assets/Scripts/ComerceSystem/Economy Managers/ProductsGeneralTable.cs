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
    public int demand; // Demanda geral para todos os produtos (pode ser ajustada conforme necessário)
    public List<Product> products; // Lista de produtos no mercado

    public void RegulatorSupplyAndDemand()
    {
        foreach (var product in products)
        {
            // Calcula a relação entre demanda e oferta
            float supplyDemandRatio = (float)demand / product.quant;

            // Ajusta o preço com base na relação oferta-demanda
            if (supplyDemandRatio > 1)
            {
                // Se a demanda for maior que a oferta, aumenta o preço
                product.price *= supplyDemandRatio;
            }
            else
            {
                // Se a oferta for maior que a demanda, reduz o preço
                product.price /= supplyDemandRatio;
            }

            // Evita que o preço caia abaixo de um certo limite
            product.price = Mathf.Max(product.price, 1.0f);
        }
    }

    private void Update()
    {
        
    }
}

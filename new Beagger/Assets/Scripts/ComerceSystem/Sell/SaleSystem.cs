using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaleSystem : MonoBehaviour
{
    [SerializeField] ProductsGeneralTable productsGeneralTable;

    public StoreInfo Store;

    [SerializeField] SaleSystemUIManager UIManager;

    public List<Product> selectedProducts = new List<Product>();
    public List<Product> inventoryProducts = new List<Product>();

    public float totalValue = 0;

    // Dicion�rio para acessar os produtos da tabela rapidamente
    private Dictionary<string, Product> productDictionary;

    void Awake()
    {
        // Inicializa o dicion�rio de produtos para acesso r�pido
        productDictionary = new Dictionary<string, Product>();
        foreach (var product in productsGeneralTable.products)
        {
            productDictionary[product.item.itemName] = product;
        }
    }

    public void SetInventoryItemsAsProducts()
    {
        inventoryProducts.Clear();
        selectedProducts.Clear();

        if (Inventory.Instance.inventory.Count > 0)
        {
            foreach (var item in Inventory.Instance.inventory)
            {
                // Usa o dicion�rio para acessar o produto rapidamente
                if (productDictionary.TryGetValue(item.item.itemName, out Product foundProduct))
                {
                    // Adiciona uma entrada para cada unidade do item no invent�rio
                    for (int i = 0; i < item.quant; i++)
                    {
                        Product newProduct = new Product(foundProduct.item, foundProduct.price);
                        inventoryProducts.Add(newProduct);
                    }
                }
            }
            // Atualiza a UI ap�s definir os produtos
            UIManager.UpdateUI();
        }
    }

    public void UpdateTotalValue()
    {
        totalValue = 0;
        foreach (var item in selectedProducts)
        {
            totalValue += item.price;
        }
    }

    public void SaleCompleted()
    {
        // Atualiza o valor total com base nos produtos selecionados
        UpdateTotalValue();

        foreach (var item in selectedProducts)
        {
            // Remove os itens vendidos do invent�rio
            Inventory.Instance.RemoveItem(item.item, null);
        }

        // Adiciona o dinheiro ao jogador
        PlayerStts.Instance.money += totalValue;

        // Limpa a lista de produtos selecionados e reseta o valor total
        selectedProducts.Clear();
        totalValue = 0;

        // Atualiza a UI ap�s completar a venda
        UIManager.UpdateUI();
    }

    public void SaleCanceled()
    {
        // Limpa a lista de produtos selecionados
        selectedProducts.Clear();

        // Atualiza a UI ap�s cancelar a venda
        UIManager.UpdateUI();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    // Enum para representar as ações possíveis
    public enum NPCActionType
    {
        Buy,
        Sell
    }

    // Função que escolhe uma ação aleatoriamente entre comprar e vender
    public NPCActionType ChooseRandomAction()
    {
        int randomAction = Random.Range(0, 2); // Gera 0 ou 1
        return (NPCActionType)randomAction;
    }

    // Função para executar a ação escolhida
    public void ExecuteAction(NPCActionType actionType)
    {
        switch (actionType)
        {
            case NPCActionType.Buy:
                Buy();
                break;
            case NPCActionType.Sell:
                Sell();
                break;
        }
    }

    private void Buy()
    {
        // Escolhe um produto aleatório para compra
        EconomyManager.instance.ProductsGeneralTable.products[Random.Range(0, EconomyManager.instance.ProductsGeneralTable.products.Count)].quant--;
    }

    private void Sell()
    {
        List<Product> products = EconomyManager.instance.ProductsGeneralTable.products;

        // Escolhe um produto com mais chances se estiver em alta demanda (baixa quantidade)
        Product chosenProduct = ChooseProductBasedOnDemand(products);

        if (chosenProduct != null)
        {
            // Aumenta a quantidade do produto (NPC está vendendo)
            chosenProduct.quant++;
            Debug.Log("NPC está vendendo " + chosenProduct.item.name);
        }
        else
        {
            Debug.Log("Nenhum produto disponível para venda.");
        }
    }

    // Função para escolher o produto com base na demanda (baixa quantidade)
    private Product ChooseProductBasedOnDemand(List<Product> products)
    {
        float totalWeight = 0;
        List<float> weights = new List<float>();

        // Calcula o peso inversamente proporcional à quantidade em estoque
        foreach (var product in products)
        {
            float weight = 1.0f / Mathf.Max(product.quant, 1); // Evita divisão por zero
            weights.Add(weight);
            totalWeight += weight;
        }

        // Gera um número aleatório para escolher um produto com base nos pesos
        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0;

        for (int i = 0; i < products.Count; i++)
        {
            cumulativeWeight += weights[i];

            if (randomValue <= cumulativeWeight)
            {
                return products[i];
            }
        }

        return null;
    }
}

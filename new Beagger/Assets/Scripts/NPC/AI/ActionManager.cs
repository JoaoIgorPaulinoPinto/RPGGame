using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    // Enum para representar as a��es poss�veis
    public enum NPCActionType
    {
        Buy,
        Sell
    }

    // Fun��o que escolhe uma a��o aleatoriamente entre comprar e vender
    public NPCActionType ChooseRandomAction()
    {
        int randomAction = Random.Range(0, 2); // Gera 0 ou 1
        return (NPCActionType)randomAction;
    }

    // Fun��o para executar a a��o escolhida
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
        // Escolhe um produto aleat�rio para compra
        EconomyManager.instance.ProductsGeneralTable.products[Random.Range(0, EconomyManager.instance.ProductsGeneralTable.products.Count)].quant--;
    }

    private void Sell()
    {
        List<Product> products = EconomyManager.instance.ProductsGeneralTable.products;

        // Escolhe um produto com mais chances se estiver em alta demanda (baixa quantidade)
        Product chosenProduct = ChooseProductBasedOnDemand(products);

        if (chosenProduct != null)
        {
            // Aumenta a quantidade do produto (NPC est� vendendo)
            chosenProduct.quant++;
            Debug.Log("NPC est� vendendo " + chosenProduct.item.name);
        }
        else
        {
            Debug.Log("Nenhum produto dispon�vel para venda.");
        }
    }

    // Fun��o para escolher o produto com base na demanda (baixa quantidade)
    private Product ChooseProductBasedOnDemand(List<Product> products)
    {
        float totalWeight = 0;
        List<float> weights = new List<float>();

        // Calcula o peso inversamente proporcional � quantidade em estoque
        foreach (var product in products)
        {
            float weight = 1.0f / Mathf.Max(product.quant, 1); // Evita divis�o por zero
            weights.Add(weight);
            totalWeight += weight;
        }

        // Gera um n�mero aleat�rio para escolher um produto com base nos pesos
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

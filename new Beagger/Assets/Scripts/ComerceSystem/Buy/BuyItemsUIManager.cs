using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ComerceBuyInventoryItemsSlot;

public class BuyItemsUIManager : MonoBehaviour
{
    [Space]
    // Elemento de texto para exibir o valor total da compra
    [SerializeField] private TextMeshProUGUI txtvalor;
    [Space]

    // Referências ao sistema central de compras e ao inventário
    [SerializeField] private BuyItems central;
    [SerializeField] private Inventory inventory;
    [Space]

    // Objetos pai para os itens da lista de compra e dos produtos, e prefabs para os slots
    [SerializeField] private GameObject buyListParent;
    [SerializeField] private GameObject buyListslotPrefab;
    [SerializeField] private GameObject productsListItemsParent;
    [SerializeField] private GameObject productSlotPrefab;
    [Space]

    // Lista para armazenar os itens de exibição do inventário
    [SerializeField] public List<ItemSlot> displayInventoryItems = new List<ItemSlot>();

    // Configura os slots dos produtos no container de produtos
    void SetSlotsOnProductsContainer()
    {
        clearProductsSlots();
        if (central.Products.Count > 0)
        {
            foreach (var product in central.Products)
            {
                // Instancia um novo slot de produto e define suas propriedades com base no item
                ComerceBuyInventoryItemsSlot newSlot = Instantiate(productSlotPrefab, productsListItemsParent.transform).GetComponent<ComerceBuyInventoryItemsSlot>();
                newSlot.SetValues(product.item, 1, this);
            }
        }
    }

    // Atualiza os valores da lista de compra na UI
    public void UpdateBuyListValues()
    {
        clearBuyListSlots();
        UpdateCustValues();
        if (displayInventoryItems.Count > 0)
        {
            foreach (var item in displayInventoryItems)
            {
                // Instancia um novo slot na lista de compra e define suas propriedades com base no item
                var currentSlot = Instantiate(buyListslotPrefab, buyListParent.transform);
                var slot = currentSlot.GetComponent<ComerceBuyInventoryItemsSlot>();
                slot.SetValues(item.item, item.quantity, this);
            }
        }
    }

    // Limpa todos os slots de produtos na UI
    void clearProductsSlots()
    {
        foreach (Transform child in productsListItemsParent.transform)
        {
            if (child != null) { Destroy(child.gameObject); }
        }
    }

    // Limpa todos os slots da lista de compra na UI
    public void clearBuyListSlots()
    {
        foreach (Transform child in buyListParent.transform)
        {
            if (child != null) { Destroy(child.gameObject); }
        }
    }

    // Adiciona um item à lista de compra
    public void AddToBuyList(Item item)
    {
        central.AddToBuyList(item);
        if (central.selectedProducts.Count < 1)
        {
            displayInventoryItems.Add(new ItemSlot(item, 1));
        }
        else
        {
            ItemSlot existingItem = displayInventoryItems.Find(i => i.item.itemName == item.itemName);

            if (existingItem != null)
            {
                existingItem.quantity += 1;
            }
            else
            {
                displayInventoryItems.Add(new ItemSlot(item, 1));
            }
        }
        UpdateBuyListValues();
    }

    // Remove um item da lista de compra
    public void RemoveOfBuyList(Item item)
    {
        central.RemoveFromBuyList(item);
        ItemSlot existingItem = displayInventoryItems.Find(i => i.item.itemName == item.itemName);

        existingItem.quantity -= 1;

        if (existingItem.quantity < 1)
        {
            displayInventoryItems.Remove(existingItem);
        }
        UpdateBuyListValues();
    }

    // Atualiza os valores de custo dos itens selecionados
    public void UpdateCustValues()
    {
        float valor = 0;
        foreach (var item in central.selectedProducts)
        {
            valor += central.SearchForItemInProduct(item).price;
        }
        txtvalor.text = "R$ " + valor.ToString("00.00");
    }

    // Finaliza a compra
    public void FinalizePurchase()
    {
        central.FinalizePurchase();
        // Opcionalmente, você pode atualizar a UI após finalizar a compra
        UpdateBuyListValues();
        UpdateCustValues();
        // Atualizar o dinheiro do jogador, caso necessário
        clearBuyListSlots();
    }

    // Inicializa a UI configurando os slots dos produtos e da lista de compra
    private void StartUI()
    {
        SetSlotsOnProductsContainer();
        UpdateBuyListValues();
    }

    // Método Start, chamado ao iniciar o script
    private void Start()
    {
        StartUI();
    }
}

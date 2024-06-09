using System.Collections.Generic;
using TMPro;
using UnityEngine;

using static ComerceSellInventoryItemsSlot;
using static UnityEditor.Progress;

public class SellItemsUIManager : MonoBehaviour
{
    [Space]
    // Elemento de texto para exibir o valor total
    [SerializeField] private TextMeshProUGUI txtvalor;
    [Space]

    // Refer�ncias ao sistema central de vendas e ao invent�rio
    [SerializeField] private SellItems central;
    [SerializeField] private Inventory inventory;
    [Space]

    // Objetos pai para os itens da UI do invent�rio e do carrinho, e prefabs para os slots
    [SerializeField] private GameObject cartListParent;
    [SerializeField] private GameObject cartListslotPrefab;
    [SerializeField] private GameObject inventoryListItemsParent;
    [SerializeField] private GameObject inventorySlotPrefab;
    [Space]

    // Listas para armazenar itens de exibi��o do invent�rio e do carrinho
    [SerializeField] public List<InventoryListItem> displayInventoryItems = new List<InventoryListItem>();
    [SerializeField] public List<InventoryListItem> displayCartItems = new List<InventoryListItem>();

    // Atualiza o pre�o total dos itens selecionados
    void UpdateTotalPrice()
    {
        float    valor = 0;
        foreach (var i in central.selectedProducts)
        {
            valor += central.SerachForItemInProduct(i).price;
        }
        txtvalor.text = "R$" + valor.ToString();
    }

    // Limpa todos os slots de itens do invent�rio na UI
    private void ClearInventoryItemsSlots()
    {
        foreach (Transform child in inventoryListItemsParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    // Limpa todos os slots de itens do carrinho na UI
    private void ClearCartItemsSlots()
    {
        foreach (Transform child in cartListParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    // Atualiza os slots do invent�rio na UI
    void UpdateInventorySlots()
    {
        ClearInventoryItemsSlots();
        if (displayInventoryItems.Count > 0)
        {
            foreach (var item in displayInventoryItems)
            {
                var currentSlot = Instantiate(inventorySlotPrefab, inventoryListItemsParent.transform);
                var slot = currentSlot.GetComponent<ComerceSellInventoryItemsSlot>();
                slot.SetValues(item.item, item.quantity, this);
            }
        }
    }

    // Configura os slots do invent�rio com base nos itens atuais do invent�rio
    private void SetInventorySlots()
    {
        ClearInventoryItemsSlots();
        foreach (var inventoryItem in inventory.inventory)
        {
            InventoryListItem existingItem = displayInventoryItems.Find(i => i.item.itemName == inventoryItem.item.itemName);
            if (existingItem != null)
            {
                existingItem.quantity = inventoryItem.quant;
            }
            else
            {
                displayInventoryItems.Add(new InventoryListItem(inventoryItem.item, inventoryItem.quant));
            }
        }

        UpdateInventorySlots();
    }

    // Atualiza os slots do carrinho na UI
    private void UpdateCartSlots()
    {
        ClearCartItemsSlots();
        if (displayCartItems.Count > 0)
        {
            foreach (var item in displayCartItems)
            {
                var currentSlot = Instantiate(cartListslotPrefab, cartListParent.transform);
                var slot = currentSlot.GetComponent<ComerceSellInventoryItemsSlot>();
                slot.SetValues(item.item, item.quantity, this);
            }
        }
    }

    // Adiciona um item � lista do carrinho
    public void AddToCartList(Item item)
    {
        print("item adicionado: " + item.itemName);
        InventoryListItem existingItem = displayCartItems.Find(i => i.item.itemName == item.itemName);
        if (existingItem != null)
        {
            existingItem.quantity += 1;
        }
        else
        {
            displayCartItems.Add(new InventoryListItem(item, 1));
        }

        central.AddToSellList(item);
        RemoveFromInventoryList(item);

        UpdateCartSlots();
        UpdateTotalPrice();
    }

    // Remove um item da lista do carrinho
    public void RemoveFromCartList(Item item)
    {
        InventoryListItem existingItem = displayCartItems.Find(i => i.item.itemName == item.itemName);
        if (existingItem != null)
        {
            if (existingItem.quantity > 1)
            {
                existingItem.quantity -= 1;
            }
            else
            {
                displayCartItems.Remove(existingItem);
            }
        }

        central.RemoveFromSellList(item);
        AddToInventoryList(item);

        UpdateCartSlots();
        UpdateInventorySlots();
        UpdateTotalPrice();
    }

    // Adiciona um item � lista de exibi��o do invent�rio
    private void AddToInventoryList(Item item)
    {
        InventoryListItem existingItem = displayInventoryItems.Find(i => i.item.itemName == item.itemName);
        if (existingItem != null)
        {
            existingItem.quantity += 1;
        }
        else
        {
            displayInventoryItems.Add(new InventoryListItem(item, 1));
        }
    }

    // Remove um item da lista de exibi��o do invent�rio
    private void RemoveFromInventoryList(Item item)
    {
        InventoryListItem existingItem = displayInventoryItems.Find(i => i.item.itemName == item.itemName);
        if (existingItem != null)
        {
            if (existingItem.quantity > 1)
            {
                print("item removido: " + existingItem.item.itemName);
                existingItem.quantity -= 1;
            }
            else
            {
                print("item removido: " + existingItem.item.itemName);
                displayInventoryItems.Remove(existingItem);
            }
            UpdateInventorySlots();
        }
    }

    // Finaliza a venda, limpa a lista do carrinho e atualiza a UI
    public void FinalizerSale()
    {
        central.FinalizeSele();
        displayCartItems.Clear();
        UpdateTotalPrice();
        UpdateInventorySlots();
        UpdateCartSlots();
    }

    // Inicializa a UI configurando os slots do invent�rio e do carrinho
    public void StartUI()
    {
        SetInventorySlots();
        UpdateCartSlots();
    }
}

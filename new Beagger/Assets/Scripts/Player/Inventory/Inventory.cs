using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItems
    {
        public ItemData item;
        public int quant;

        public InventoryItems(ItemData item, int quantity)
        {
            this.item = item;
            this.quant = quantity;
        }
    }

    public InventoryUIManager UIManager;
    [SerializeField] public List<InventoryItems> inventory = new List<InventoryItems>();
    public float defaultLimitWeight;
    public float limitWeight;
    public float currentWeight;

    public static Inventory Instance { get; private set; }

    private void Start()
    {
        limitWeight = defaultLimitWeight;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destrói a nova instância se outra já existir
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool AddItem(ItemData item)
    {
        if (!WeightLimitChecker(item.weight))
        {
            Debug.Log("Weight limit exceeded.");
            return false;
        }

        var existingItem = SearchItem(item);
        if (existingItem != null)
        {
            if (item.itemType == ItemType.Tool || item.itemType == ItemType.Bag || item.itemType == ItemType.Weapon)
            {
                var newInventoryItem = new InventoryItems(item, 1);
                inventory.Add(newInventoryItem);
                UIManager.AddToUI(newInventoryItem);
            }
            else
            {
                existingItem.quant++;
                UIManager.UpdateSlotValues(existingItem, null);
            }
        }
        else
        {
            var newInventoryItem = new InventoryItems(item, 1);
            inventory.Add(newInventoryItem);
            UIManager.AddToUI(newInventoryItem);
        }

        RecalculateWeight(); // Recalcula o peso
        return true;
    }

    public bool RemoveItem(ItemData item, InventorySlot? slot)
    {
        bool itemRemoved = false;

        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item.itemName == item.itemName)
            {
                if (inventory[i].quant > 1)
                {
                    inventory[i].quant--;
                    UIManager.UpdateSlotValues(inventory[i], slot);
                }
                else
                {
                    UIManager.RemoveFromUI(inventory[i], slot);
                    inventory.RemoveAt(i);
                }
                itemRemoved = true;
                RecalculateWeight(); // Recalcula o peso
                break;
            }
        }

        return itemRemoved;
    }

    public InventoryItems SearchItem(ItemData item)
    {
        foreach (var inventoryItem in inventory)
        {
            if (item && inventoryItem.item && inventoryItem.item.itemName == item.itemName)
            {
                return inventoryItem;
            }
        }
        return null;
    }

    public bool WeightLimitChecker(float additionalWeight = 0)
    {
        float projectedWeight = currentWeight + additionalWeight;
        return projectedWeight <= limitWeight;
    }

    private void RecalculateWeight()
    {
        currentWeight = 0;
        foreach (var inventoryItem in inventory)
        {
            currentWeight += inventoryItem.item.weight * inventoryItem.quant;
        }
    }

    private void Update()
    {
        // Evita loop infinito removendo itens de forma apropriada
        for (int i = 0; i < inventory.Count && currentWeight > limitWeight; i++)
        {
            if (inventory[i].quant > 0)
            {
                ItemsManager.Instance.DropItem(inventory[i].item, inventory[i].quant);
                RemoveItem(inventory[i].item, null);
            }
        }
    }
}

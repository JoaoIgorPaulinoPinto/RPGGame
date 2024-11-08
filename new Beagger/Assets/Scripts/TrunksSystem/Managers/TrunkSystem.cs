using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TrunkItems
{
    public ItemData item;
    public int quant;

    public TrunkItems(ItemData item, int quant)
    {
        this.item = item;
        this.quant = quant;
    }
}

public class TrunkSystem : MonoBehaviour
{
    public Trunk curerntTrunk;

    public List<TrunkItems> items; // Lista de itens no ba�
    public TrunkUIManager UIManager;

    public float maxWeight;// Limite m�ximo de peso do ba�
    public float currentWeight; // Peso atual do ba�

    public void calcWeight()
    {
        currentWeight = 0;
        foreach (var item in items)
        {
            for (int i = item.quant; i > 0; i--)
            {
                currentWeight += item.item.weight;
            }
            
        }
    }
    public void UpdateCallerData()
    {
        curerntTrunk.items = items;
    }
    public void Use(float maxWeight, List<TrunkItems> items, Trunk trunk)
    {
        this.maxWeight = maxWeight;
        this.items = items;
        calcWeight();
        if (trunk) this.curerntTrunk = trunk;
        UIManager.OpenUI();
        
        UIManager.UpdateUI(items);
        
    }

    public bool AddItemToTrunk(ItemData item)
    {
        float itemWeight = item.weight;
        if (currentWeight + itemWeight > maxWeight)
        {
            Debug.LogWarning("Ba� est� cheio. N�o � poss�vel adicionar mais itens.");
            return false;
        }

        var existingItem = SearchItemInTrunk(item);
        if (existingItem != null)
        {
            if (item.itemType == ItemType.Tool || item.itemType == ItemType.Bag || item.itemType == ItemType.Weapon)
            {
                var newTrunkItem = new TrunkItems(item, 1);
                items.Add(newTrunkItem);
            }
            else
            {
                existingItem.quant++;
            }
        }
        else
        {
            var newTrunkItem = new TrunkItems(item, 1);
            items.Add(newTrunkItem);
        }

        calcWeight();
        Inventory.Instance.RemoveItem(item, null);
        UIManager.UpdateUI(items);
        return true;
    }

    public bool RemoveItemFromTrunk(ItemData item)
    {
        bool itemRemoved = false;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item.itemName == item.itemName)
            {
                if (items[i].quant > 1)
                {
                    items[i].quant--;
                }
                else
                {
                    items.RemoveAt(i);
                    EquipedItemsManager.Instance.ChangeEquipedTool(null);
                }

                calcWeight();// Atualiza o peso total do ba�
                itemRemoved = true;
                break;
            }
        }
        UIManager.UpdateUI(items);
        return itemRemoved;
    }

    public TrunkItems SearchItemInTrunk(ItemData item)
    {
        foreach (var trunkItem in items)
        {
            if (trunkItem.item.itemName == item.itemName)
            {
                return trunkItem;
            }
        }
        return null;
    }

    public bool AddItemToInventory(ItemData item)
    {
        if (Inventory.Instance.AddItem(item))
        {
            RemoveItemFromTrunk(item);
            return true;
        }
        return false;
    }

    public bool RemoveItemFromInventory(ItemData item)
    {
        if (Inventory.Instance.RemoveItem(item, null))
        {
            UIManager.UpdateUI(items);
            return true;
        }
        return false;
    }

    // Mover todos os itens do invent�rio para o ba�
    public void MoveAllToTrunk()
    {
        foreach (var inventoryItem in Inventory.Instance.inventory.ToList()) // Usar ToList para evitar modifica��o durante a itera��o
        {
            for (int i = inventoryItem.quant; i > 0; i--)
            {
                if (AddItemToTrunk(inventoryItem.item))
                {
                    RemoveItemFromInventory(inventoryItem.item);
                }
                else
                {
                    Debug.Log("N�o foi poss�vel mover todos os itens para o ba� devido ao limite de peso.");
                    break;
                }
            }
        }
    }


    // Mover todos os itens do ba� para o invent�rio
    public void MoveAllToInventory()
    {
        foreach (var trunkItem in items.ToList()) // Usar ToList para evitar modifica��o durante a itera��o
        {
            for (int i = trunkItem.quant; i > 0; i--)
            {
                if (Inventory.Instance.AddItem(trunkItem.item))
                {
                    RemoveItemFromTrunk(trunkItem.item);
                }
                else
                {
                    Debug.Log("Invent�rio cheio. N�o foi poss�vel mover todos os itens.");
                    break;
                }
            }

        }
    }

    public void ToInventory(GameObject button)
    {
        button.TryGetComponent<TrunkSlot>(out TrunkSlot slot);
        AddItemToInventory(slot.item);
    }

    public void ToTrunk(GameObject button)
    {
        button.TryGetComponent<TrunkSlot>(out TrunkSlot slot);
        AddItemToTrunk(slot.item);
    }
}

using UnityEngine;
using System.Collections.Generic;
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
public class Inventory : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip addClip;
    [SerializeField] AudioClip NaddClip;


    [SerializeField] InventoryToolsBarManager invToolsBarManager;
    public InventoryUIManager UIManager;
    [SerializeField] public List<InventoryItems> inventory = new List<InventoryItems>();
    public float defaultLimitWeight;
    public float limitWeight;
    public float currentWeight;

    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destr�i a nova inst�ncia se outra j� existir
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        
        limitWeight = defaultLimitWeight;
    }


    public bool AddItem(ItemData item)
    {
        if (!WeightLimitChecker(item.weight))
        {
            PopUpSystem.Instance.SendMsg("Voc� n�o pode carregar mais peso...", MessageType.Alert, null);
            return false;
        }
        var existingItem = SearchItem(item);
        if (existingItem != null)
        {
            if (item.itemType == ItemType.Tool || item.itemType == ItemType.Bag || item.itemType == ItemType.Weapon)
            {
                if (UIManager.VerifyEmpSlots())
                {
                    var newInventoryItem = new InventoryItems(item, 1);
                    inventory.Add(newInventoryItem);
                    UIManager.AddToUI(newInventoryItem);

                }
                else
                {
                    PopUpSystem.Instance.SendMsg("Voc� n�o possui mais espa�os no inventario", MessageType.Alert, null);

                    return false;
                }

              
            }
            else
            {
                existingItem.quant++;
                UIManager.UpdateSlotValues(existingItem, null);

            }
        }
        else 
        {
            if (UIManager.VerifyEmpSlots())
            {
                var newInventoryItem = new InventoryItems(item, 1);
                inventory.Add(newInventoryItem);
                UIManager.AddToUI(newInventoryItem);

              

            }
            else
            {
                PopUpSystem.Instance.SendMsg("Voc� n�o possui mais espa�os no inventario", MessageType.Alert, null);

                return false;
            }
          
        }
       
        RecalculateWeight(); // Recalcula o peso
        invToolsBarManager.UpdateToolsBarData();
        PlayAudio(addClip);
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
                    EquipedItemsManager.Instance.ChangeEquipedTool(null);
                }
                itemRemoved = true;
                RecalculateWeight(); // Recalcula o peso
                break;
            }
        }
        invToolsBarManager.UpdateToolsBarData();
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

    public void RecalculateWeight()
    {
        currentWeight = 0;
        foreach (var inventoryItem in inventory)
        {
            currentWeight += inventoryItem.item.weight * inventoryItem.quant;
        }
        UIManager.UpdateWightLabel();
    }

    private void Update()
    {
        // Evita loop infinito removendo itens de forma apropriada
        for (int i = 0; i < inventory.Count && currentWeight > limitWeight; i++)
        {
            if (inventory[i].quant > 0)
            {
                ItemsManager.Instance.DropItem(inventory[i].item, inventory[i].quant, transform);
                RemoveItem(inventory[i].item, null);
            }
        }
    }
    void PlayAudio(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Inventory;

public class InventoryUIManager : MonoBehaviour
{
    
    [SerializeField] Transform UI;
    [SerializeField] Transform[] slotsParents;
    public List<GameObject> slots = new List<GameObject>();
    [SerializeField] InventorySlotsMovimentation inventorySlotsMovimentation;

    public bool isOpen;

    public bool AddToUI(InventoryItems item)
    {
        bool someNull = false;
        foreach (var slot in slots)
        {
            InventorySlot i = slot.transform.GetChild(0).GetComponent<InventorySlot>();
            if (i.cellItem == null)
            {
                someNull = true;
                i.SetValues(item.item, item.quant);
                break;
            }
            else
            {
                someNull = false;
            }
        }
        //inventoryToolsBarManager.UpdateToolsBarData();
        return someNull;
    }

    public void RemoveFromUI(InventoryItems item, InventorySlot? selectedSlot)
    {
        if (selectedSlot != null)
        {
            selectedSlot.ClearValues();
        }
        else
        {
            foreach (var slot in slots)
            {
                InventorySlot i = slot.transform.GetChild(0).TryGetComponent<InventorySlot>(out i) ? i : null;
                if (i != null && i.cellItem != null)
                {
                    if (item.item.itemName == i.cellItem.itemName)
                    {
                        i.ClearValues();
                        break;
                    }
                }
            }
        }
        //inventoryToolsBarManager.UpdateToolsBarData();
    }

    public void UpdateSlotValues(InventoryItems item, InventorySlot? selectedSlot)
    {
        if (selectedSlot != null)
        {
            selectedSlot.ClearValues();
            selectedSlot.SetValues(item.item, item.quant);
        }
        else
        {
            foreach (var slot in slots)
            {
                InventorySlot i = slot.transform.GetChild(0).TryGetComponent<InventorySlot>(out i) ? i : null;
                if (i != null && i.cellItem != null)
                {
                    if (i.cellItem.itemName == item.item.itemName)
                    {
                        i.ClearValues();
                        i.SetValues(item.item, item.quant);
                        break;
                    }
                }
            }
        }
    }

    public void OpenInventory()
    {
        inventorySlotsMovimentation.StartSlotsSprite();
        UI.gameObject.SetActive(true);
        isOpen = true;
        inventorySlotsMovimentation.StartSlotsSprite();
    }

    public void CloseInventory()
    {
        isOpen = false;
        UI.gameObject.SetActive(false);
    }

    private void Start()
    {
        InitializeSlots();
        inventorySlotsMovimentation.StartSlotsSprite();
    }

    private void InitializeSlots()
    {
        slots.Clear(); // Limpa a lista existente
        for (int i = 0; i < slotsParents.Length; i++)
        {
            foreach (Transform child in slotsParents[i])
            {
                if (child.gameObject)
                {
                    slots.Add(child.gameObject);
                }
            }
        }
    }
}

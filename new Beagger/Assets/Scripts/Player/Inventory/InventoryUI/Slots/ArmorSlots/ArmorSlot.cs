using TMPro;
using UnityEngine;

public class ArmorSlot : MonoBehaviour
{
    
    [SerializeField] Armor item;
    [SerializeField] InventorySlot inventorySlot;  // Referência direta ao slot de inventário

    private void Update()
    {
        
        UpdateWeightLimit();
    }
    private void UpdateWeightLimit()
    {
        inventorySlot = GetComponentInChildren<InventorySlot>();
        if (inventorySlot != null)
        {
            if (inventorySlot.cellItem != null && inventorySlot.cellItem.itemType == ItemType.Bag)
            {
                item = inventorySlot.cellItem as Armor;
                Inventory.Instance.limitWeight = item.capacity;
            }
            else
            {
                Inventory.Instance.limitWeight = Inventory.Instance.defaultLimitWeight;
                
            }
        }
        Inventory.Instance.UIManager.UpdateWightLabel();
    }
}
    
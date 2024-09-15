using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToolsBarManager : MonoBehaviour
{
    [System.Serializable]
    public class SLOT
    {
        public Transform inventorySlot;
        public Transform gameHUDSlot;
    }

    public List<SLOT> slots;

    public void UpdateToolsBarData()
    {
        foreach (var slot in slots)
        {
            InventorySlot i;
            slot.inventorySlot.GetChild(0).TryGetComponent<InventorySlot>(out i);
            ToolsBarSlot o;
            slot.gameHUDSlot.TryGetComponent<ToolsBarSlot>(out o);
            if (i != null && o != null)
            {
                if (i.cellItem)
                {
                    o.SetSlotItem(Inventory.Instance.SearchItem(i.cellItem));
                }
                else
                {
                 
                    o.ClearSlot();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        UpdateToolsBarData();
    }
}

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
           
            slot.inventorySlot.GetChild(0).TryGetComponent<InventorySlot>(out InventorySlot i);
     
            slot.gameHUDSlot.TryGetComponent<ToolsBarSlot>(out ToolsBarSlot o);
            if (i != null && o != null)
            {
                if (i.cellItem )
                {
                    if(o.item != i.cellItem)
                    {
                        o.SetSlotItem(Inventory.Instance.SearchItem(i.cellItem));

                    }
                    else
                    {
                        o.SetSlotItem(Inventory.Instance.SearchItem(i.cellItem));

                    }
                }
                else if(o.item != i.cellItem)
                {
                 
                    o.ClearSlot();
                }
            }
        }
    }

    private void FixedUpdate()
    {
    }
    private void Start()
    {
        foreach (var slot in slots)
        {
            slot.inventorySlot.GetChild(0).TryGetComponent<InventorySlot>(out InventorySlot i);

            slot.gameHUDSlot.TryGetComponent<ToolsBarSlot>(out ToolsBarSlot o);
            if (i != null && o != null  && i.cellItem)
            {
                o.SetSlotItem(Inventory.Instance.SearchItem(i.cellItem));
            }
            else { 

                o.ClearSlot();
            }
        }
    }
}

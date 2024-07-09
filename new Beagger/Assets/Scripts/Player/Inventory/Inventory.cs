using UnityEngine;
using System.Collections.Generic;


public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class inventoryItems
    {
        public Item item;
        public int quant;
        
        

        public  inventoryItems(Item item, int quant)
        {
            this.item = item;
            this.quant = quant;
        }
    }
    [Tooltip("Gerenciador da HUD do inventario")]
    public InventoryUIManager inventoryGUIManager;
    [Tooltip("Items no inventario")]
    [SerializeField] public List<inventoryItems> inventory = new List<inventoryItems>();
    public bool AddItem(Item item)
    {
        bool resp = true;
       
        if (inventory.Contains(SerachForItem(item)))
        {
            if (SerachForItem(item).item is Tool)
            {
                inventory.Add(new inventoryItems(item, 1));
                inventoryGUIManager.UpdateValues();
            }
            else
            {
                SerachForItem(item).quant++;
                inventoryGUIManager.UpdateValues();
            }
        }
        else
        {
            foreach (var cell in inventoryGUIManager.cells)
            {

                if (cell.GetComponent<InventorySlot>().cellItem == null)
                {
                    inventory.Add(new inventoryItems(item, 1));
                    inventoryGUIManager.UpdateValues();
                    resp = true;
                }
                else
                {
                    resp = false;
                    //todos slots ocupados
                }
            }
        }
        return resp;
    }
    public void RemoveItem(inventoryItems item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item.itemName == item.item.itemName)
            {
                inventory[i].quant--;
                if (inventory[i].quant <= 0)
                {
                   
                    inventory.RemoveAt(i);
                    inventoryGUIManager.UpdateValues();
                }
                inventoryGUIManager.UpdateValues();
                return;
            }
        }
    }
    public inventoryItems SerachForItem(Item item)
    {
        inventoryItems itemReturned = null;
        
        foreach (var item1 in inventory)
        {
            if(item != null && item1.item == item)
            {
                itemReturned = item1;
                break;
            }
        }
        return itemReturned;
    }
}
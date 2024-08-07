using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public bool isOpen = false;

    [Tooltip("Item selecionado")]
    public GameObject selectedCell;
    [HideInInspector]public Inventory inventory;
  
    [SerializeField] GameObject CellParent;
    [HideInInspector]public List<GameObject> cells = new List<GameObject>();
  
    public void SetCells()
    {
        cells.Clear();
        for (int i = 0; i < CellParent.transform.childCount; i++)
        {
            cells.Add(CellParent.transform.GetChild(i).GetChild(0).gameObject);
        }
    }
    public void UpdateValues()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if (i < inventory.inventory.Count && inventory.inventory[i].item != null)
            {
                cells[i].GetComponent<InventorySlot>().SetValues(inventory.inventory[i].item, inventory.inventory[i].quant);
            }
            else
            {
                cells[i].GetComponent<InventorySlot>().ClearValues();
            }
           }
    }

    public void OpenInventory()
    {
        gameObject.SetActive(true);

        SetCells();
        UpdateValues();
        isOpen = true;  
    }
    public void CloseInventory()
    {
        gameObject.SetActive(false);
        isOpen = false;
    }

    public void Usar()
    {
        if (selectedCell != null && selectedCell.GetComponent<InventorySlot>().cellItem is ConsumableBase item)
        {
            item.UseConsumableItem();
            if (item)
            {
                inventory.RemoveItem(inventory.SerachForItem(item));
                selectedCell = null;
            }

        }
    }
    public void Dropar()
    {
        if (selectedCell != null)
        {
            //selectedCell.GetComponent<InventoryCell>().cellItem.DropItem();
            //Instantiate(selectedCell.GetComponent<InventoryCell>().cellItem.prefab, transform.position, transform.rotation);
            inventory.RemoveItem(inventory.SerachForItem(selectedCell.GetComponent<InventorySlot>().cellItem));
            selectedCell = null;
        }
    }

}

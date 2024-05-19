using System.Collections.Generic;
using UnityEngine;

public class inventoryGUIManager : MonoBehaviour
{
    [Tooltip("Item selecionado")]
    public GameObject selectedCell;
    [HideInInspector]public Inventory inventory;
  
    [SerializeField] GameObject CellParent;
    [HideInInspector]public List<GameObject> cells = new List<GameObject>();
  
    public void SetCells()
    {
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
                cells[i].GetComponent<InventoryCell>().SetValues(inventory.inventory[i].item, inventory.inventory[i].quant);
            }
            else
            {
                cells[i].GetComponent<InventoryCell>().ClearValues();
            }
        }
    }

    private void Start()
    {
        SetCells();
        UpdateValues();
    }

    public void Usar()
    {
        if (selectedCell != null && selectedCell.GetComponent<InventoryCell>().cellItem is Consumable)
        {
         
            inventory.RemoveItem(inventory.SerachForItem(selectedCell.GetComponent<InventoryCell>().cellItem));
            selectedCell = null;
        }
    }
    public void Dropar()
    {
        if (selectedCell != null)
        {
            //selectedCell.GetComponent<InventoryCell>().cellItem.DropItem();
            //Instantiate(selectedCell.GetComponent<InventoryCell>().cellItem.prefab, transform.position, transform.rotation);
            inventory.RemoveItem(inventory.SerachForItem(selectedCell.GetComponent<InventoryCell>().cellItem));
            selectedCell = null;
        }
    }
}

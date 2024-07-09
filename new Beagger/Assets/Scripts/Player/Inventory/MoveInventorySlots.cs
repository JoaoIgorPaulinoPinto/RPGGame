using UnityEngine;

public class MoveInventorySlots : MonoBehaviour
{
    [SerializeField]InventoryUIManager selectedCell;
    Item selectedItem;

    private void FixedUpdate()
    {

    }
    public void SelectThisCell()
    {
        if (selectedCell == null)
        {
            return;
        }
        if (selectedCell.selectedCell == null)
        {

            if (this.transform.parent.TryGetComponent<ToolsBarCell>(out ToolsBarCell a))
            {
                if (a.item != null)
                {
                    selectedCell.selectedCell = gameObject;
                }
                else
                {
                    selectedCell.selectedCell = null;
                    return;
                }
            }
            else
            {
                selectedCell.selectedCell = gameObject;
            }
        }
        else 
        {
            if (selectedCell.selectedCell.TryGetComponent<InventorySlot>(out InventorySlot cell))
            {
                selectedItem = cell.cellItem;
            }
            if (transform.parent.TryGetComponent<ToolsBarCell>(out ToolsBarCell toolsCell))
            {
                if (selectedItem != null)
                {
                    if (selectedItem is Tool)
                    {

                        Transform aux = transform.parent;
                        transform.SetParent(selectedCell.selectedCell.transform.parent);
                        selectedCell.selectedCell.transform.SetParent(aux);


                        selectedCell.selectedCell = null;
                        selectedItem = null;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }


                selectedCell.selectedCell = null;
                selectedItem = null;
            }
            else
            {
                Transform aux = transform.parent;
                transform.SetParent(selectedCell.selectedCell.transform.parent);
                selectedCell.selectedCell.transform.SetParent(aux);
                selectedCell.selectedCell = null;
                selectedItem = null;
            }
        }
    }

}

using UnityEngine;

public class ToolsBarCell : MonoBehaviour
{
    public Item item;
    [SerializeField]ToolsBarSlot Slot;

    private void Update()
    {
        if(GetComponentInChildren<InventoryCell>().cellItem is Tool)
        {
            item = GetComponentInChildren<InventoryCell>().cellItem;
            SetSlot(item);
        }
        else
        {
            item = null;
            UnSetSlot();
        }
    }

    void SetSlot(Item item)
    {
        Slot.SetValues(item);
    }
    void UnSetSlot()
    {
        Slot.UnSetValues();
    }
}

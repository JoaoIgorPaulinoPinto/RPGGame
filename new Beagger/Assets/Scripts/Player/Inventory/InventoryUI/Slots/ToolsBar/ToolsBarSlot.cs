using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static InventoryToolsBarManager;
using static Inventory;
using System.Runtime.Remoting.Messaging;

public class ToolsBarSlot : MonoBehaviour
{
    [Header("Managers")]
    public bool isSelected;
    public int slotindex;
    InventorySlot inventorySlot;
    public ItemData item;


        [Header("Slot Render")]
    [SerializeField] Sprite defaultSlotSprite;
    [SerializeField] Sprite selectedSlotSprite;
    [SerializeField] Image slotRender;

        [Header("Item Render")]
    [SerializeField] Image spriteRender;
    [SerializeField] Sprite defaultSprite;


    [SerializeField] TextMeshProUGUI lbl_quant;

    public void SelectSlot()
    {
        slotRender.sprite = selectedSlotSprite;
        isSelected = true;
        
    }
    public void UnSelectSlot()
    {
        slotRender.sprite = defaultSlotSprite;
        isSelected = false;
    }

    public void SetSlotItem(InventoryItems slot)
    {
        this.item = slot.item;
        spriteRender.sprite = slot.item.icon;
        lbl_quant.text = slot.quant.ToString();
        if (isSelected)
        {
            EquipedItemsManager.Instance.ChangeEquipedTool(this.gameObject);
        }
    }

    public void ClearSlot()
    {
        this.item = null;
        spriteRender.sprite = defaultSprite;
        lbl_quant.text = "";
        if (isSelected)
        {
            EquipedItemsManager.Instance.ChangeEquipedTool(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static InventoryToolsBarManager;
using static Inventory;

public class ToolsBarSlot : MonoBehaviour
{
    InventorySlot inventorySlot;

    public ItemData item;
    [SerializeField] Image spriteRender;
    [SerializeField] Sprite defaultSprite;

    [SerializeField] TextMeshProUGUI lbl_quant;

    public void SelectSlot()
    {
        print(item.itemName + " selecionado ");
    }

    public void SetSlotItem(InventoryItems  slot)
    {
        this.item = slot.item;
        spriteRender.sprite = slot.item.icon;
        lbl_quant.text = slot.quant.ToString();
    }

    public void ClearSlot()
    {
        this.item = null;
        spriteRender.sprite = defaultSprite;
        lbl_quant.text = "";
    }

    private void Start()
    {
        ClearSlot();
    }
}

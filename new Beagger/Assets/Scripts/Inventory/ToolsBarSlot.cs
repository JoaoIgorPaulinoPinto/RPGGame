using UnityEngine;
using UnityEngine.UI;

public class ToolsBarSlot : MonoBehaviour
{
    [SerializeField]EquipedToolsManager toolManager;
    [SerializeField] Sprite empySlotSprite;
    public Image spriteRender;
    public Item item;

    public void SetValues(Item item)
    {
        spriteRender.sprite = item.icon;
        this.item = item;
    }
    public void UnSetValues()
    {
        spriteRender.sprite = empySlotSprite;
        this.item = null;
    }

    public void EquipTool()
    {
        Tool newTool = null;
        if(item is Tool)
        {
            newTool = (Tool)item;
        }
        toolManager.ChangeEquipedTool(newTool);
    }
}
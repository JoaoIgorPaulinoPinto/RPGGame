using UnityEngine;
using UnityEngine.UI;

public class ToolsBarSlot : MonoBehaviour
{
    public Image spriteRender;
    Item item;

    public void SetValues(Item item)
    {
        spriteRender.sprite = item.icon;
        this.item = item;
    }
    public void UnSetValues()
    {
        spriteRender.sprite = default;
        this.item = null;
    }
}
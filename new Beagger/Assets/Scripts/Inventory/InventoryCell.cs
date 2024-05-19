using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    public Item cellItem;
    [SerializeField]Image spriteRender;
    [SerializeField]TextMeshProUGUI? labelName;
    [SerializeField]TextMeshProUGUI? labelQuant;

    public int quant = 0;

    public void SetValues(Item item, int qnt)
    {
        spriteRender.sprite = item.icon;
        labelName.text = item.itemName;
        labelQuant.text = qnt.ToString();

      

        cellItem = item;
        quant =  qnt;
    }
    private void Awake()
    {
        if(cellItem == null) { 
            ClearValues();
        }
    }
    public void ClearValues()
    {
        this.cellItem = null;
        this.labelName.text = "";
        this.labelQuant.text = "";
        this.spriteRender.sprite = default;
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, transform.parent.position, 0.3f);
    }
}
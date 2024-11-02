using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemExpUIManager : MonoBehaviour
{
    [SerializeField] InventoryUIManager inventoryUIManager;

    [SerializeField] GameObject container;
    

    [SerializeField] TextMeshProUGUI lbl_name;
    [SerializeField] TextMeshProUGUI lbl_type;
    [SerializeField] TextMeshProUGUI lbl_weight;
    [SerializeField] TextMeshProUGUI lbl_rarity;
    [SerializeField] TextMeshProUGUI lbl_quantity;
    [SerializeField] TextMeshProUGUI lbl_origin;
    [SerializeField] TextMeshProUGUI lbl_condition;
    [SerializeField] TextMeshProUGUI lbl_description;

    [SerializeField] Image iconRender;


    [SerializeField] ItemData item;

    public void Show(GameObject button)
    {
        button.TryGetComponent(out InventorySlot a);
        if (a != null)
        {
            ItemData item = a.cellItem;
            this.item = item;
            if (item != null)
            {
                container.SetActive(true);

                iconRender.sprite = item.icon;
                lbl_name.text = item.itemName;
                lbl_type.text = item.itemType.ToString();
                lbl_weight.text = $"{item.weight.ToString("00.00")} Kg";
                lbl_rarity.text = item.rarityLevel.ToString();
                lbl_origin.text = item.origin;
                lbl_condition.text = $"{item.condition.ToString()}%";
                lbl_description.text = item.description;

                lbl_quantity.text = button.GetComponent<InventorySlot>().quant.ToString("00.00");
            }
        }
       
        else
        {
            foreach (var slot in inventoryUIManager.slots)
            {
                slot.transform.GetChild(0).TryGetComponent<InventorySlot>(out InventorySlot i);
                if (i.isSelected)
                {
                    item = i.cellItem;

                    container.SetActive(true);

                    iconRender.sprite = item.icon;
                    lbl_name.text = item.itemName;
                    lbl_type.text = item.itemType.ToString();
                    lbl_weight.text = $"{item.weight.ToString("00.00")} Kg";
                    lbl_rarity.text = item.rarityLevel.ToString();
                    lbl_origin.text = item.origin;
                    lbl_condition.text = $"{item.condition.ToString()}%";
                    lbl_description.text = item.description;

                    lbl_quantity.text = button.GetComponent<InventorySlot>().quant.ToString();
                    break;
                }
                else
                {
                    container.SetActive(false);
                }
            }
            
        }

    }
    private void Update()
    {/*
        if (!s)
        {
            container.SetActive(true);
        }
        else
        {
            container.SetActive(true);
        }*/
    }
}

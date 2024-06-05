using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComerceSellInventoryItemsSlot : MonoBehaviour
{
    [System.Serializable]
    public class InventoryListItem
    {
        public Item item;
        public int quantity;

        public InventoryListItem(Item item, int quantity)
        {   
            this.item = item;
            this.quantity = quantity;
            
        }
    }
    public int quantity;

    public SellItemsUIManager manager;
    public Item item;
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtQuant;
    public Image icon;

    public void SetValues(Item _item,int _quantity, SellItemsUIManager _manager)
    {
        manager = _manager;
        quantity = _quantity;
        txtName.text = $"{_item.itemName} (x{_quantity})";
        icon.sprite = _item.icon;
        item = _item;
      
    }

    public void GoToMarketChart()
    {
        manager.AddToCartList(item);

    }
    public void RemoveOfList()
    {
        manager.RemoveFromCartList  (item);
    }


}

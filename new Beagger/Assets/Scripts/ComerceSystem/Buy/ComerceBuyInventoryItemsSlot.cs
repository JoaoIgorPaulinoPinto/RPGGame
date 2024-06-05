using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComerceBuyInventoryItemsSlot : MonoBehaviour
{
    [System.Serializable]
    public class ItemSlot
    {
        public Item item;
        public int quantity;

        public ItemSlot(Item item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
    }

    public BuyItemsUIManager manager;
    public Item item;
    public int quantity;
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtQuant;
    public Image icon;

    public void SetValues(Item _item, int _quantity, BuyItemsUIManager _manager)
    {
        manager = _manager;
        item = _item;
        quantity = _quantity;
        txtName.text = $"{_item.itemName} (x{_quantity})";
        icon.sprite = _item.icon;
      //  txtQuant.text = _quantity.ToString()
    }
    
    public void AddToBuyList()
    {
        manager.AddToBuyList(item);
    }

    public void RemoveOfBuyList()
    {
        manager.RemoveOfBuyList(item);
    }
}

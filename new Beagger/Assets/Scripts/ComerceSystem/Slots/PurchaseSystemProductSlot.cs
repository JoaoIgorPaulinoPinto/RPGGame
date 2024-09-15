using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseSystemProductSlot : MonoBehaviour
{
    public Product product;
    [HideInInspector]public PurchaseSystemUIManager PurchaseUIManager;



    [SerializeField] TextMeshProUGUI lblItemName;
    [SerializeField] TextMeshProUGUI lblItemPrice;
    [SerializeField] Image iconRender;


    public void SetValues(Product _product, PurchaseSystemUIManager _PurUIManager)
    {
        PurchaseUIManager = _PurUIManager;

        product = _product;

        lblItemName.text = product.item.itemName;
        lblItemPrice.text = "R$ " + product.price.ToString();
        iconRender.sprite = product.item.icon;
    }

    public void RemoveThisFromSelectedProducts()
    {

        PurchaseUIManager.RemoveSelectedProduct(product);
    }
    public void AddThisToSelectedProducts()
    {
        PurchaseUIManager.AddProduct(product);
    }
}
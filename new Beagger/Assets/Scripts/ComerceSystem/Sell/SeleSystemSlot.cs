using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaleSystemSlot : MonoBehaviour
{

    public Product product;
    [HideInInspector] public SaleSystemUIManager SaleUIManager;



    [SerializeField] TextMeshProUGUI lblItemName;
    [SerializeField] TextMeshProUGUI lblItemPrice;
    [SerializeField] Image iconRender;


    public void SetValues(Product _product, SaleSystemUIManager _UIManager)
    {
        SaleUIManager = _UIManager;

        product = _product;

        lblItemName.text = product.item.itemName;
        lblItemPrice.text = "R$ " + product.price.ToString();
        iconRender.sprite = product.item.icon;
    }

    public void RemoveThisFromSelectedProducts()
    {

        SaleUIManager.RemoveSelectedProduct(product);
    }
    public void AddThisToSelectedProducts()
    {
        SaleUIManager.AddProduct(product);
    }
}
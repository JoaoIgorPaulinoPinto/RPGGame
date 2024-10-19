using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SaleSystemSlot : MonoBehaviour
{
    AudioSource ad;
    [SerializeField] AudioClip pointerEnter;
    [SerializeField] AudioClip pointerClick;

    public Product product;
    [HideInInspector] public SaleSystemUIManager SaleUIManager;



    [SerializeField] TextMeshProUGUI lblItemName;
    [SerializeField] TextMeshProUGUI lblItemPrice;
    [SerializeField] Image iconRender;

    private void Start()
    {
        ad = GeneralReferences.Instance.UIAudioSource;

    }
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
    public void OnPointerEnter()
    {
        ad.clip = pointerEnter;
        ad.Play();
    }
    public void OnPointeClick()
    {
        ad.clip = pointerClick;
        ad.Play();
    }

}
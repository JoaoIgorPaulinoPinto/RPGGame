using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseSystemProductSlot : MonoBehaviour
{
    [SerializeField] AudioClip pointerEnter;
    [SerializeField] AudioClip pointerClick;

    public Product product;
    [HideInInspector]public PurchaseSystemUIManager PurchaseUIManager;

    AudioSource ad;

    [SerializeField] TextMeshProUGUI lblItemName;
    [SerializeField] TextMeshProUGUI lblItemPrice;
    [SerializeField] Image iconRender;
    private void Start()
    {
        ad = GeneralReferences.Instance.UIAudioSource;
    }

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
        ad.clip = pointerClick;
        ad.Play();
        PurchaseUIManager.RemoveSelectedProduct(product);
    }
    public void AddThisToSelectedProducts()
    {
        ad.clip = pointerClick;
        ad.Play();
        PurchaseUIManager.AddProduct(product);
    }
    public void OnPointerEnter()
    {
        ad.clip = pointerEnter;
        ad.Play();

    }
    public void OnPointerClick()
    {
        ad.clip = pointerClick;
        ad.Play();

    }
}
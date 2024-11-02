using TMPro;
using UnityEngine;

public class PurchaseSystemUIManager : MonoBehaviour
{
    public GameObject UI;

    [Space]

    public AudioSource audioSource;
    [SerializeField] AudioClip addToCart;
    [SerializeField] AudioClip RemoveFromCart;
    [SerializeField] AudioClip Finalize;
    [SerializeField] AudioClip Cancel;

    [Space]

    [SerializeField] TextMeshProUGUI lbl_TotalValue;
    [SerializeField] TextMeshProUGUI lbl_ClientName;
    [SerializeField] TextMeshProUGUI lbl_ComerciantName;
    [SerializeField] TextMeshProUGUI lbl_Quantity;
    [SerializeField] TextMeshProUGUI lbl_storeType;
    [SerializeField] TextMeshProUGUI lbl_playerMoney;

    [Space]
    [Space]
    [Space]
    public GameObject prefabProductSlot;
    public GameObject prefabSelectedProductSlot;

    public Transform productsListParent;
    public Transform selectedListParent;

    public PurchaseSystem purchaseSystem;
    public void SetProductsSlots()
    {
        foreach (var item in purchaseSystem.products)
        {
            PurchaseSystemProductSlot newSlot = Instantiate(prefabProductSlot, productsListParent).GetComponent<PurchaseSystemProductSlot>();
            newSlot.SetValues(item, this);
        }
    }
    public void SetSelectedProductsSlots()
    {
        foreach (var item in purchaseSystem.selectedProducts)
        {
            PurchaseSystemProductSlot newSlot = Instantiate(prefabSelectedProductSlot, selectedListParent).GetComponent<PurchaseSystemProductSlot>();
            newSlot.SetValues(item, this);
        }
    }
    public void UpdateUI()
    {
        foreach (Transform child in productsListParent.transform) { if (child != null) { Destroy(child.gameObject); } }

        foreach (Transform child in selectedListParent.transform) { if (child != null) { Destroy(child.gameObject); } }

        SetProductsSlots();

        SetSelectedProductsSlots();

        purchaseSystem.UpdateTotalValue();
        purchaseSystem.PricesCorrection();

        UpdateDetailsScreen();
    }
    public void RemoveSelectedProduct(Product product)
    {
        purchaseSystem.selectedProducts.Remove(product);
        UpdateUI();
        PlaySound(RemoveFromCart);

    }
    public void AddProduct(Product product)
    {
        purchaseSystem.selectedProducts.Add(product);
        UpdateUI();
        print(product.item.itemName + " ADICIONADO AO CARRINHO");
        PlaySound(addToCart);

    }
    public void PurchaseComplete()
    {
        purchaseSystem.PurchaseCompleted();
        UpdateUI();
        PlaySound(Finalize);

    }
    public void PurchaseCanceled()
    {
        purchaseSystem.PurchaseCanceled();
        UpdateUI();
        PlaySound(Cancel);
    }
    public void UpdateDetailsScreen()
    {
        if (purchaseSystem.totalValue > PlayerStts.Instance.money)
        {
            lbl_TotalValue.color = Color.red;
        }
        else
        {
            lbl_TotalValue.color = Color.green;

        }
        lbl_playerMoney.text = "R$ " + PlayerStts.Instance.money.ToString("00.00");
        lbl_TotalValue.text = "R$ " + purchaseSystem.totalValue.ToString("00.00");
        lbl_ClientName.text = PlayerStts.Instance.playerName;
        lbl_ComerciantName.text = purchaseSystem.Store.SellerName;
        lbl_storeType.text = purchaseSystem.Store.StoreType.ToString();
        lbl_Quantity.text = purchaseSystem.selectedProducts.Count.ToString("00.00");
    }
    void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
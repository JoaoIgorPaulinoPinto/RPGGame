using TMPro;
using UnityEngine;

public class SaleSystemUIManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI lbl_TotalValue;
    [SerializeField] TextMeshProUGUI lbl_ClientName;
    [SerializeField] TextMeshProUGUI lbl_ComerciantName;
    [SerializeField] TextMeshProUGUI lbl_Quantity;
    [SerializeField] TextMeshProUGUI lbl_storeType;
    [SerializeField] TextMeshProUGUI lbl_playerMoney;
    [Space]
    [Space]
    [Space]
    public GameObject prefabInventoryProductSlot;
    public GameObject prefabSelectedProductSlot;

    public Transform inventoryProductsListParent;
    public Transform selectedListParent;

    public SaleSystem saleSystem;
    public void SetInventoryProductsSlots()
    {

        foreach (var item in saleSystem.inventoryProducts)
        {
            SaleSystemSlot newSlot = Instantiate(prefabInventoryProductSlot, inventoryProductsListParent).GetComponent<SaleSystemSlot>();
            newSlot.SetValues(item, this);
        }
    }
    public void SetSelectedProductsSlots()
    {
        foreach (var item in saleSystem.selectedProducts)
        {
            SaleSystemSlot newSlot = Instantiate(prefabSelectedProductSlot, selectedListParent).GetComponent<SaleSystemSlot>();
            newSlot.SetValues(item, this);
        }
    }
    public void UpdateUI()
    {
        foreach (Transform child in inventoryProductsListParent.transform) { if (child != null) { Destroy(child.gameObject); } }

        foreach (Transform child in selectedListParent.transform) { if (child != null) { Destroy(child.gameObject); } }

        SetInventoryProductsSlots();

        SetSelectedProductsSlots();

        saleSystem.UpdateTotalValue();

        UpdateDetailsScreen();
    }
    public void RemoveSelectedProduct(Product product)
    {
        saleSystem.selectedProducts.Remove(product);
        saleSystem.inventoryProducts.Add(product);  
        UpdateUI();
    }
    public void AddProduct(Product product)
    {
        saleSystem.selectedProducts.Add(product);
        saleSystem.inventoryProducts.Remove(product);
        UpdateUI();
        print(product.item.itemName + " SENDO VENDIDO(A)");
    }
    public void PurchaseComplete()
    {
        saleSystem.SaleCompleted();
        UpdateUI();
    }
    public void PurchaseCanceled()
    {
        saleSystem.SaleCanceled();
        UpdateUI();
    }
    public void UpdateDetailsScreen()
    {
        lbl_playerMoney.text = "R$ " + saleSystem.playerStts.money.ToString();
        lbl_TotalValue.text = "R$ " + saleSystem.totalValue.ToString();
        lbl_ClientName.text = saleSystem.playerStts.playerName;
        lbl_ComerciantName.text = saleSystem.Store.SellerName;
        lbl_storeType.text = saleSystem.Store.StoreType.ToString();
        lbl_Quantity.text = saleSystem.selectedProducts.Count.ToString();
    }
}